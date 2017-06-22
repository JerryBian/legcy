using System;

namespace Func.Cache.Interfaces
{
    public interface IUnderlyingCache
    {
        bool Contains(string cacheKey);

        void Add(string cacheKey, object value, DateTimeOffset absoluteExpire);

        object Get(string cacheKey);

        object this[string cacheKey] { get; }
    }
}
