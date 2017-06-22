using System;
using System.Runtime.Caching;
using Func.Cache.Interfaces;

namespace Func.Cache.Defaults
{
    public class FuncMemoryCache : IUnderlyingCache
    {
        private static readonly object LockObject = new object();
        private readonly MemoryCache _memoryCache = MemoryCache.Default;

        public bool Contains(string cacheKey)
        {
            return _memoryCache.Contains(cacheKey);
        }

        public void Add(string cacheKey, object value, DateTimeOffset absoluteExpire)
        {
            lock (LockObject)
            {
                _memoryCache.Add(cacheKey, value, absoluteExpire);
            }
        }

        public object Get(string cacheKey)
        {
            return _memoryCache.Get(cacheKey);
        }

        public object this[string cacheKey] => Get(cacheKey);
    }
}
