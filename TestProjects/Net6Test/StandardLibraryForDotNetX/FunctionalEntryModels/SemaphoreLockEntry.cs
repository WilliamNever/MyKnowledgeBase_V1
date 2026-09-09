namespace StandardLibraryForDotNetX.FunctionalEntryModels
{
    /// <summary>
    /// References SemaphoreSlim - 
    /// **********************************************************************************************
    ///     SemaphoreLockEntry.WaitAsync and SemaphoreLockEntry.Release must appear in pairs.
    /// **********************************************************************************************
    /// </summary>
    public sealed class SemaphoreLockEntry : IDisposable
    {
        public bool HasDisposed { get; private set; } = false;
        private SemaphoreSlim Semaphore { get; }
        private int _referenceCount = 0;
        /// <summary>
        /// _lock is ensuring internally thread-safe.
        /// </summary>
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

                if (canRestoreEnterCount)
                    Semaphore.Release();
            }
        }

        public void AddReferenceCount()
        {
            lock (_lock)
            {
                _referenceCount++;
            }
        }

        public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken token = default)
        {
            lock (_lock)
            {
                if (HasDisposed)
                    return Task.FromResult(false);
                else
                    return Semaphore.WaitAsync(timeout, token);
            }
        }

        [Obsolete("Using WaitAsync method instead! This method is Only for Testing! " +
            "This method can cause deadlock!!!", true)]
        private async Task<bool> AWaitAsync(TimeSpan timeout, CancellationToken token = default)
        {
            lock (_lock)
            {
                if (HasDisposed)
                    return false;
                else
                    return Semaphore.WaitAsync(timeout, token).Result;
            }
        }

        [Obsolete("Using WaitAsync method instead! This method is Only for Testing! " +
            "This method can cause deadlock!!!", true)]
        private bool Wait(TimeSpan timeout, CancellationToken token = default)
        {
            lock (_lock)
            {
                if (HasDisposed)
                    return false;
                else
                    return Semaphore.Wait(timeout, token);
            }
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