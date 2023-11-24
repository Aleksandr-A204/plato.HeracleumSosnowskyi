using HeracleumSosnowskyiService.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HeracleumSosnowskyiService.Services
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;
        public CachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T GetData<T>(string key)
        {
            try
            {
                T item = _memoryCache.Get<T>(key);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object RemoveData(string key)
        {
            var res = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Remove(key);
                else
                    res = false;

                return res;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var res = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Set(key, value, expirationTime);
                else
                    res = false;

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
