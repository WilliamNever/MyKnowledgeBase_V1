using Microsoft.Extensions.Caching.Memory;
using StandardLibrary.IServices;
using System;

namespace StandardLibrary.Services
{
    public class CacheManageService: ICacheManage
    {
        private readonly IMemoryCache _cache;
        public CacheManageService(IMemoryCache cache)
        {
            _cache = cache;
        }
        #region Private methods
        private MemoryCacheEntryOptions CreateSlidingExpirationEntryOption()
        {
            var copt = new MemoryCacheEntryOptions().SetSize(1);
            copt.SetSlidingExpiration(TimeSpan.FromMinutes(20));
            return copt;
        }
        private MemoryCacheEntryOptions CreateAbsoluteExpirationEntryOption(DateTime dt)
        {
            var copt = new MemoryCacheEntryOptions().SetSize(1);
            copt.SetAbsoluteExpiration(new DateTimeOffset(dt));
            return copt;
        }
        private string CreateCacheKey<TModel>(string key) => $"{typeof(TModel).Name}_{key}";

        #endregion

        #region Common read and write
        public void SetSlidingExpirationCache<T>(string key, T value)
        {
            _cache.Set(CreateCacheKey<T>(key)
                , value, CreateSlidingExpirationEntryOption());
        }
        public void SetAbsoluteExpirationCache<T>(string key, T value, DateTime ExpDateTime)
        {
            _cache.Set(CreateCacheKey<T>(key)
                , value, CreateAbsoluteExpirationEntryOption(ExpDateTime));
        }
        public bool GetFromCache<T>(string key, out T model)
        {
            var cachKey = CreateCacheKey<T>(key);
            return _cache.TryGetValue(cachKey, out model);
        }
        #endregion

        #region Examples
        public string GetAPIToken(string key)
        {
            if (!GetFromCache(key, out string rsl))
                return null;
            return rsl;
        }
        public void CacheAPIToken(string key, string value)
        {
            SetAbsoluteExpirationCache(key, value, DateTime.Now.AddHours(2));
        }
        #endregion

    }
}
