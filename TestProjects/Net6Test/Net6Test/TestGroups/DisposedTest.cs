namespace Net6Test.TestGroups
{
    public static class DisposedTest
    {
        public static async Task DisposedInterfacesTest()
        {
            using (var dis = new DisTest_IDisposable())
            {
                Console.WriteLine($"In {typeof(DisTest_IDisposable).Name} working area.");
            }

            await using (var dis = new DisTest_IAsyncDisposable())
            {
                Console.WriteLine($"In {typeof(DisTest_IAsyncDisposable).Name} working area.");
            }

            Console.WriteLine($"Exit all working areas.");
        }
    }

    public class DisTest_IDisposable : IDisposable
    {
        private bool _disposed;

        //~DisTest_IDisposable()
        //{
        //    Console.WriteLine($"++{GetType().Name}");
        //}
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Console.WriteLine($"__{GetType().Name}");
            _disposed = true;
            //GC.SuppressFinalize(this);    // this command works for destructure. if there is no destructure, it can be removed.
        }
    }

    public class DisTest_IAsyncDisposable : IAsyncDisposable
    {
        private bool _disposed;

        //~DisTest_IAsyncDisposable()
        //{
        //    Console.WriteLine($"++{GetType().Name}");
        //}

        /// <summary>
        /// do not await, just return ValueTask.
        /// If unmanaged cleanup is required, combine a normal finalizer with synchronous unmanaged release logic, not await.
        /// no finalizer(destructure), return ValueTask.CompletedTask unless real async cleanup is needed.
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            if (_disposed)
            {
                return ValueTask.CompletedTask;
            }

            Console.WriteLine($"__{GetType().Name}");
            _disposed = true;
            //GC.SuppressFinalize(this); // this command works for destructure. if there is no destructure, it can be removed.
            //no finalizer, return ValueTask.CompletedTask unless real async cleanup is needed
            return ValueTask.CompletedTask;
            //return new ValueTask(Task.Run(() => { Console.WriteLine($"__{GetType().Name}"); }));
        }
    }

    /// <summary>
    /// The sample for IDisposable interface
    /// </summary>
    public class MyResource : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Finalizer fallback. Releases unmanaged resources if Dispose() was not called.
        /// </summary>
        ~MyResource()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // dispose managed resources here
            }

            // free unmanaged resources here

            _disposed = true;
        }
    }
}
