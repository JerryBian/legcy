using Laobian.Infrastuture.Interface.Service;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Laobian.Service.Base
{
    public class LaobianCache:ILaobianCache
    {
        private readonly IMemoryCache _memoryCache;

        public LaobianCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public void Set<T>(string key, T value, TimeSpan relativeToNow)
        {
            _memoryCache.Set(key, value, relativeToNow);
        }

        public void Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value, DateTimeOffset.MaxValue);
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> setValue, TimeSpan relativeToNow)
        {
            if(TryGetValue(key, out T value))
            {
                return value;
            }

            value = await setValue();
            Set(key, value, relativeToNow);
            return value;
        }

        public T GetOrSet<T>(string key, Func<T> setValue, TimeSpan relativeToNow)
        {
            if (TryGetValue(key, out T value))
            {
                return value;
            }

            value = setValue();
            Set(key, value, relativeToNow);
            return value;
        }
    }
}
