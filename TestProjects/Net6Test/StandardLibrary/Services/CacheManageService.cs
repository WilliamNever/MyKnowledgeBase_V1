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


        public string GetAPIToken(string key)
        {
            var cachKey = CreateCacheKey<string>(key);
            if (!_cache.TryGetValue(cachKey, out string value))
                value = null;
            return value;
        }
        public void CacheAPIToken(string key, string value)
        {
            _cache.Set(CreateCacheKey<string>(key), value
                , CreateAbsoluteExpirationEntryOption(DateTime.Now.AddHours(2)));
        }
    }
}
