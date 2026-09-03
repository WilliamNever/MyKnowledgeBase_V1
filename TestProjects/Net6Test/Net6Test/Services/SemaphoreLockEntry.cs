namespace Net6Test.Services
{
    public sealed class SemaphoreLockEntry : IDisposable
    {
        public bool HasDisposed { get; private set; } = false;
        public SemaphoreSlim Semaphore { get; }
        private int _referenceCount = 0;

        private object _lock = new object();

        public SemaphoreLockEntry(int maxConcurrentRequests)
        {
            Semaphore = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);
        }

        public void Release()
        {
            lock (_lock)
            {
                if (HasDisposed)
                    return;

                if (ReleaseReference() <= 0)
                    HasDisposed = true;
                Semaphore.Release();
            }
        }

        public bool Wait(TimeSpan timeout, CancellationToken token = default)
        {
            AddReference();
            return Semaphore.Wait(timeout, token);
        }

        public int AddReference()
        {
            lock (_lock)
            {
                if (HasDisposed) throw new ObjectDisposedException(nameof(SemaphoreLockEntry));
                return Interlocked.Increment(ref _referenceCount);
            }
        }

        private int ReleaseReference()
        {
            return Interlocked.Decrement(ref _referenceCount);
        }

        public void Dispose()
        {
            HasDisposed = true;
            Semaphore.Dispose();
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
