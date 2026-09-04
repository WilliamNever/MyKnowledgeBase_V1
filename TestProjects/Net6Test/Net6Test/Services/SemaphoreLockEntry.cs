namespace Net6Test.Services
{
    /*
     * Usages - 
    
            var sle = new SemaphoreLockEntry(1);
            bool sEnter = false;
            try
            {
                Console.WriteLine($"Begin - {DateTime.Now}");
                var tsrc = new CancellationTokenSource(TimeSpan.FromMinutes(2));
                sEnter = sle.Wait(TimeSpan.FromSeconds(5), true, tsrc.Token);
                Console.WriteLine($"{sEnter} - {DateTime.Now}");
                sEnter = sle.Wait(TimeSpan.FromSeconds(5), true, tsrc.Token);
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
    /// References SemaphoreSlim
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
                Interlocked.Decrement(ref _referenceCount);
                if (canRestoreEnterCount)
                    Semaphore.Release();
            }
        }

        public bool Wait(TimeSpan timeout, bool addRef = true, CancellationToken token = default)
        {
            AddReference(addRef);
            return Semaphore.Wait(timeout, token);
        }

        public int AddReference(bool addRef = true)
        {
            lock (_lock)
            {
                if (HasDisposed) throw new ObjectDisposedException(nameof(SemaphoreLockEntry));
                
                if (addRef)
                    return Interlocked.Increment(ref _referenceCount);
                else 
                    return _referenceCount;
            }
        }

        public bool TryDispose()
        {
            lock (_lock)
            {
                if (HasDisposed) return true;
                var rsl = _referenceCount < 1;
                if (rsl) Dispose();
                return rsl;
            }
        }
        public void Dispose()
        {
            lock (_lock)
            {
                if (!HasDisposed)
                {
                    HasDisposed = true;
                    Semaphore.Dispose();
                }
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
