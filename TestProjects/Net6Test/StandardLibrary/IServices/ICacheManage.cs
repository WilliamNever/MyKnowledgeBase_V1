using System;

namespace StandardLibrary.IServices
{
    public interface ICacheManage
    {
        bool GetFromCache<T>(string key, out T model);
        void SetAbsoluteExpirationCache<T>(string key, T value, DateTime ExpDateTime);
        void SetSlidingExpirationCache<T>(string key, T value);
    }
}
