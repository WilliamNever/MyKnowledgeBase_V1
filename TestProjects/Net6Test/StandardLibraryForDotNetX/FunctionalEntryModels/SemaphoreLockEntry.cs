namespace StandardLibraryForDotNetX.FunctionalEntryModels
{
    /*
     * Usages - 
    
            var sle = new SemaphoreLockEntry(1);
            bool sEnter = false;
            try
            {
                Console.WriteLine($"Begin - {DateTime.Now}");
                var tsrc = new CancellationTokenSource(TimeSpan.FromMinutes(2));
                sEnter = sle.Wait(TimeSpan.FromSeconds(5), tsrc.Token);
                Console.WriteLine($"{sEnter} - {DateTime.Now}");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sle.Release(sEnter);
                var tdps = sle.TryDispose();
                sle.Dispose();
            }

     */

    /// <summary>
    /// References SemaphoreSlim - 
    /// ************************************************************************************
    ///     SemaphoreLockEntry.Wait and SemaphoreLockEntry.Release must appear in pairs.
    /// ************************************************************************************
    /// </summary>
    public sealed class SemaphoreLockEntry : IDisposable
    {
        public bool HasDisposed { get; private set; } = false;
        private SemaphoreSlim Semaphore { get; }
        private int _referenceCount = 0;

        private readonly object _lock = new object();

        public SemaphoreLockEntry(int maxConcurrentRequests)
        {
            Semaphore = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);
        }

        public void Release(bool canRestoreEnterCount)
        {
            lock (_lock)
            {
                if (HasDisposed) return;
                _referenceCount--;
            }
            if (canRestoreEnterCount)
                Semaphore.Release();

        }

        public bool Wait(TimeSpan timeout, CancellationToken token = default)
        {
            lock (_lock)
            {
                if (HasDisposed) return false;
                _referenceCount++;
            }
            return Semaphore.Wait(timeout, token);
        }

        public async Task<bool> WaitAsync(TimeSpan timeout, CancellationToken token = default)
        {
            lock (_lock)
            {
                if (HasDisposed) return false;
                _referenceCount++;
            }
            return await Semaphore.WaitAsync(timeout, token);
        }

        public bool TryDispose()
        {
            lock (_lock)
            {
                if (HasDisposed) return true;

                var rsl = _referenceCount < 1;
                if (rsl) ToDispose();
                return rsl;
            }
        }
        public void Dispose()
        {
            lock (_lock)
            {
                ToDispose();
            }
        }

        private void ToDispose()
        {
            if (!HasDisposed)
            {
                HasDisposed = true;
                Semaphore.Dispose();
            }
        }

        //public int TestAddReference(int num)
        //{
        //    var ex = Interlocked.Exchange(ref _referenceCount, num);
        //    //var ends = Interlocked.Or(ref _referenceCount, 2);
        //    //ends = Interlocked.Or(ref _referenceCount, 2);
        //    return Interlocked.Add(ref _referenceCount, num);
        //    Interlocked.Decrement(ref _referenceCount);
        //}

    }
}
