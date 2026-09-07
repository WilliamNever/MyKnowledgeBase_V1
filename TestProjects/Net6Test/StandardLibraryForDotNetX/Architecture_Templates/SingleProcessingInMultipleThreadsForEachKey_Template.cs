using StandardLibrary.Exceptions;
using StandardLibrary.IServices;
using StandardLibraryForDotNetX.FunctionalEntryModels;
using System.Collections.Concurrent;

namespace StandardLibraryForDotNetX.Architecture_Templates
{
    /// <summary>
    /// A template to allow only one in ProcessingToGetNewTokenAsync within multiple task threads.
    /// 
    /// ************************************************************************************
    /// T : To be as the Key type of the static ConcurrentDictionary. 
    ///     Neither notnull nor new() is required, just easy to write the demo.
    /// ************************************************************************************
    /// </summary>
    public abstract class SingleProcessingInMultipleThreadsForEachKey_Template<T> where T : notnull, new()
    {
        private const int EacTkeyLimit = 3;
        private readonly static object _lock = new();
        private readonly static ConcurrentDictionary<T, SemaphoreLockEntry> _sslims = new();

        private readonly ICacheManage _cacheManage;
        public SingleProcessingInMultipleThreadsForEachKey_Template(ICacheManage cache)
        {
            _cacheManage = cache;
        }

        public async Task<KeyValuePair<string, string>> ProcessingToGetTokenAsync(CancellationToken token = default)
        {
            T Tkey = new();
            var _Token = _cacheManage.GetAPIToken("CacheKey");
            if (!IsValid(_Token))
            {
                SemaphoreLockEntry slim;
                lock (_lock)
                {
                    slim = _sslims.GetOrAdd(Tkey, _ => new SemaphoreLockEntry(EacTkeyLimit));
                    slim.AddReferenceCount();
                }
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
                    lock (_lock)
                    {
                        slim.Release(sAccess);
                        if (slim.TryDispose())
                        {
                            _sslims.TryRemove(Tkey, out _);
                        }
                    }
                }
            }
            return new KeyValuePair<string, string>("CacheKey", _Token);
        }

        private async Task<string> StartToProcessingAsync(CancellationToken token)
        {
            var _Token = _cacheManage.GetAPIToken("CacheKey");
            if (!IsValid(_Token))
            {
                try
                {
                    _Token = await ProcessingAsync(token);
                    if (!string.IsNullOrEmpty(_Token?.Trim()))
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
