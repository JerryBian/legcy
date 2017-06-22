using System;
using System.Threading.Tasks;
using CommonLibrary4Net;
using Func.Cache.Defaults;
using Func.Cache.Interfaces;

namespace Func.Cache
{
    public sealed class FuncCache
    {
        private static readonly Lazy<FuncCache> LazyDefault = new Lazy<FuncCache>(() => new FuncCache(), true);
        private readonly IUnderlyingCache _underlyingCache;

        public FuncCache()
        {
            _underlyingCache = FuncCacheProvider.Default;
        }

        public FuncCache(IUnderlyingCache underlyingCache)
        {
            Throws.IfNull(underlyingCache);
            _underlyingCache = underlyingCache;
        }

        public static FuncCache Default => LazyDefault.Value;

        public TResult Invoke<TResult>(Func<TResult> func, DateTimeOffset absoluteExpire)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> func, DateTimeOffset absoluteExpire)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<TResult>(Func<TResult> func, TimeSpan expireDuration)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> func, TimeSpan expireDuration)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<TResult>(Func<TResult> func, FuncCacheOption option)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<TResult>(Func<TResult> func, FuncCacheOption option)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, TResult>(Func<T1, TResult> func, DateTimeOffset absoluteExpire, T1 arg1)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, TResult>(Func<T1, TResult> func, DateTimeOffset absoluteExpire,
            T1 arg1)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, TResult>(Func<T1, TResult> func, TimeSpan expireDuration, T1 arg1)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, TResult>(Func<T1, TResult> func, TimeSpan expireDuration, T1 arg1)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, TResult>(Func<T1, TResult> func, FuncCacheOption option, T1 arg1)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, TResult>(Func<T1, TResult> func, FuncCacheOption option, T1 arg1)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, TResult>(Func<T1, T2, TResult> func, DateTimeOffset absoluteExpire, T1 arg1,
            T2 arg2)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, TResult>(Func<T1, T2, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1,
            T2 arg2)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, TResult>(Func<T1, T2, TResult> func, TimeSpan expireDuration, T1 arg1, T2 arg2)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, TResult>(Func<T1, T2, TResult> func, TimeSpan expireDuration,
            T1 arg1, T2 arg2)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, TResult>(Func<T1, T2, TResult> func, FuncCacheOption option, T1 arg1, T2 arg2)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, TResult>(Func<T1, T2, TResult> func, FuncCacheOption option,
            T1 arg1, T2 arg2)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, DateTimeOffset absoluteExpire,
            T1 arg1, T2 arg2, T3 arg3)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func,
            DateTimeOffset absoluteExpire,
            T1 arg1, T2 arg2, T3 arg3)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, TimeSpan expireDuration, T1 arg1,
            T2 arg2, T3 arg3)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3), DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func,
            TimeSpan expireDuration, T1 arg1,
            T2 arg2, T3 arg3)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, FuncCacheOption option, T1 arg1,
            T2 arg2, T3 arg3)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func,
            FuncCacheOption option, T1 arg1,
            T2 arg2, T3 arg3)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, DateTimeOffset absoluteExpire,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func,
            DateTimeOffset absoluteExpire,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, TimeSpan expireDuration,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func,
            TimeSpan expireDuration,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, FuncCacheOption option,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func,
            FuncCacheOption option,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey, func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            Throws.IfNull(func);

            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            Throws.IfNull(func);

            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13, arg14)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13, arg14)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14),
                    absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14,
                            arg15), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14,
                            arg15),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13, arg14, arg15)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13, arg14, arg15)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14,
                            arg15), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15, arg16), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            DateTimeOffset absoluteExpire, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14,
                            arg15, arg16), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15, arg16),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            TimeSpan expireDuration, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Throws.IfNull(func);
            var cacheKey = FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7,
                arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14,
                            arg15, arg16),
                    DateTimeOffset.UtcNow + expireDuration);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13, arg14, arg15, arg16)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15, arg16), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }

        public async Task<TResult> InvokeAsync
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            FuncCacheOption option, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            Throws.IfNull(func);
            Throws.IfNull(option);

            var cacheKey = string.IsNullOrEmpty(option.CacheKey)
                ? FuncCacheProvider.GenerateDefaultCacheKey(func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                    arg10, arg11, arg12, arg13, arg14, arg15, arg16)
                : option.CacheKey;
            var absoluteExpire = option.AbsoluteExpire ??
                                 DateTimeOffset.UtcNow + FuncCacheProvider.DefaultExpireDuration;

            if (!_underlyingCache.Contains(cacheKey))
            {
                _underlyingCache.Add(cacheKey,
                    await
                        func.AsTask(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                            arg14,
                            arg15, arg16), absoluteExpire);
            }

            return (TResult)_underlyingCache.Get(cacheKey);
        }
    }
}