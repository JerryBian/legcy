using System;
using Func.Cache.Interfaces;
using Newtonsoft.Json;

namespace Func.Cache.Defaults
{
    internal static class FuncCacheProvider
    {
        private const string DefaultCacheKeyPrefix = "FuncCache::Default-";
        public static TimeSpan DefaultExpireDuration = TimeSpan.FromMinutes(1);

        private static readonly Lazy<IUnderlyingCache> LazyDefault =
            new Lazy<IUnderlyingCache>(() => new FuncMemoryCache(), true);

        public static IUnderlyingCache Default => LazyDefault.Value;

        public static string GenerateDefaultCacheKey<TResult>(Func<TResult> func)
        {
            var obj = new { func.Target, func.Method };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, TResult>(Func<T1, TResult> func, T1 arg1)
        {
            var obj = new { func.Target, func.Method, arg1 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            var obj = new { func.Target, func.Method, arg1, arg2 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 arg1,
            T2 arg2, T3 arg3)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5, arg6 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            T7 arg7)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5, arg6, arg7 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            T7 arg7, T8 arg8)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11)
        {
            var obj = new { func.Target, func.Method, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12)
        {
            var obj =
                new
                {
                    func.Target,
                    func.Method,
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13)
        {
            var obj =
                new
                {
                    func.Target,
                    func.Method,
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14)
        {
            var obj =
                new
                {
                    func.Target,
                    func.Method,
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15)
        {
            var obj =
                new
                {
                    func.Target,
                    func.Method,
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }

        public static string GenerateDefaultCacheKey
            <T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15,
            T16 arg16)
        {
            var obj =
                new
                {
                    func.Target,
                    func.Method,
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                };
            return DefaultCacheKeyPrefix + JsonConvert.SerializeObject(obj);
        }
    }
}