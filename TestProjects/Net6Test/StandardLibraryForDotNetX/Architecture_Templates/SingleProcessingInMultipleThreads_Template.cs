using StandardLibrary.IServices;

namespace StandardLibraryForDotNetX.Architecture_Templates
{
    /// <summary>
    /// A template to allow only one in ProcessingToGetNewTokenAsync within multiple task threads.
    /// </summary>
    public class SingleProcessingInMultipleThreads_Template
    {
        private static readonly SemaphoreSlim _semaphoreLock = new(1);
        private readonly ICacheManage _cacheManage;
        public SingleProcessingInMultipleThreads_Template(ICacheManage cache)
        {
            _cacheManage = cache;
        }

        public async Task<KeyValuePair<string, string>> ProcessingToGetTokenAsync(CancellationToken token = default)
        {
            var isOK = _cacheManage.GetFromCache("CacheKey", out string _Token);
            if (!IsTokenValid(_Token))
            {
                var lockTaken = false;
                try
                {
                    // blocking the tasks, make only one task go ahead.
                    // it can reduce the duplicated threads to get token.
                    await _semaphoreLock.WaitAsync(token);
                    lockTaken = true;   //only processing to here, the semaphore needs to be released.
                    _Token = await GetNewTokenAsync(token);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //to restrict only one task can pass by here to get token
                    if (lockTaken && _semaphoreLock.CurrentCount < 1)
                        _semaphoreLock.Release();
                }
            }
            return new KeyValuePair<string, string>("CacheKey", _Token);
        }

        private async Task<string> GetNewTokenAsync(CancellationToken token)
        {
            var isOK = _cacheManage.GetFromCache("CacheKey", out string _Token);
            if (!IsTokenValid(_Token))
            {
                try
                {
                    _Token = await ProcessingToGetNewTokenAsync(token);
                    if (_Token != null)
                    {
                        _cacheManage.SetSlidingExpirationCache("CacheKey", _Token);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return _Token;
        }

        private async Task<string> ProcessingToGetNewTokenAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private bool IsTokenValid(string val)
        {
            throw new NotImplementedException();
        }
    }
}
