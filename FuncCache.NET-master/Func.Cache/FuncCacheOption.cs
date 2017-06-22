using System;

namespace Func.Cache
{
    public class FuncCacheOption
    {
        public DateTimeOffset? AbsoluteExpire { get; set; }

        public string CacheKey { get; set; }
    }
}
