using StandardLibrary.Exceptions;
using StandardLibrary.IServices;
using System.Collections.Concurrent;

namespace StandardLibraryForDotNetX.Architecture_Templates
{
    /// <summary>
    /// A template to allow only one in ProcessingToGetNewTokenAsync within multiple task threads.
    /// 
    /// ************************************************************************************
    /// T : To be as the Key type of the static ConcurrentDictionary. 
    ///     Neither notnull nor new() is required, just easy to write the demo.
    /// 
    /// PS -
    /// 
    ///     EachTkeyLimit : 
    ///         It restricts the number of the active working threads for each Key.
    ///         
    ///     Carefully set EachTkeyLimit to 1 :
    ///         In a narrow window, which is in clearing the cached item/s frequently, 
    ///         the active working threads maybe double for each Key.
    ///         
    ///     new SemaphoreSlim(EachTkeyLimit) -
    ///         Not restrict the max concurrent working threads.
    ///         It grows the the number of concurrent working threads, if it gets more release count.
    ///     
    ///     new SemaphoreSlim(EachTkeyLimit, EachTkeyLimit) -
    ///         It is the restricted settings. Restrict the max concurrent working threads.
    ///         So it needs ReleaseLocker(SemaphoreSlim slim) method to release safely.
    ///     
    /// ************************************************************************************
    /// </summary>
    public abstract class SingleProcessingInMultipleThreadsForEachKey_Simple_Template<T> where T : notnull, new()
    {
        private const int EachTkeyLimit = 1;
        private static readonly ConcurrentDictionary<T, SemaphoreSlim> _sslims = new();

        private readonly ICacheManage _cacheManage;
        public SingleProcessingInMultipleThreadsForEachKey_Simple_Template(ICacheManage cache)
        {
            _cacheManage = cache;
        }

        public async Task<KeyValuePair<string, string>> ProcessingToGetTokenAsync(CancellationToken token = default)
        {
            T Tkey = new();
            var _Token = _cacheManage.GetAPIToken("CacheKey");
            if (!IsValid(_Token))
            {
                var slim = _sslims.GetOrAdd(Tkey, _ => new SemaphoreSlim(EachTkeyLimit, EachTkeyLimit));

                var sAccess = false;

                try
                {
                    ///according to the fact, to set the delay time.
                    sAccess = await slim.WaitAsync(TimeSpan.FromSeconds(8), token);
                    if (sAccess)
                    {
                        _Token = await StartToProcessingAsync(token);
                    }
                    else
                    {
                        _Token = _cacheManage.GetAPIToken("CacheKey");
                        if (_Token == null)
                        {
                            throw new OperationAccessException($"Failed to get the value in a timeout.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (sAccess)
                    {
                        //slim.Release();

                        ReleaseLocker(slim);

                        if (IsValid(_cacheManage.GetAPIToken("CacheKey")))
                        {
                            _sslims.TryRemove(Tkey, out _);
                        }
                    }
                }
            }
            return new KeyValuePair<string, string>("CacheKey", _Token);
        }

        private void ReleaseLocker(SemaphoreSlim slim)
        {
            try
            {
                slim.Release();
            }
            catch (SemaphoreFullException ex){
                // to log ex
            }
        }

        private async Task<string> StartToProcessingAsync(CancellationToken token)
        {
            var _Token = _cacheManage.GetAPIToken("CacheKey");
            if (!IsValid(_Token))
            {
                try
                {
                    _Token = (await ProcessingAsync(token))?.Trim();
                    if (!string.IsNullOrEmpty(_Token))
                    {
                        _cacheManage.CacheAPIToken("CacheKey", _Token);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return _Token;
        }


        protected virtual async Task<string> ProcessingAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsValid(string val)
        {
            throw new NotImplementedException();
        }
    }
}
