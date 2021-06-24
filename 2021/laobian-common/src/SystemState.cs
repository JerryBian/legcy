using System;
using System.Collections.Concurrent;
using Laobian.Common.Base;
using Laobian.Common.Setting;

namespace Laobian.Common
{
    /// <summary>
    /// Global system shared states
    /// </summary>
    public static class SystemState
    {
        /// <summary>
        /// System startup time
        /// </summary>
        public static DateTime StartupTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last time posts reloaded from store
        /// </summary>
        public static DateTime LastTimePostReloaded { get; set; }

        /// <summary>
        /// Count of published posts
        /// </summary>
        public static int PublishedPostsCount { get; set; }

        /// <summary>
        /// Count of visit logs
        /// </summary>
        public static int VisitLogsCount { get; set; }

        /// <summary>
        /// Count of status logs
        /// </summary>
        public static int StatusLogsCount { get; set; }

        /// <summary>
        /// Count of total logs, including visit logs and status logs
        /// </summary>
        public static int TotalLogs => VisitLogsCount + StatusLogsCount;

        /// <summary>
        /// Local cache of posts visit count, this is updated by log hosted service and consumed by each post reloading
        /// </summary>
        public static ConcurrentDictionary<Guid, int> PostsVisitCount { get; } = new ConcurrentDictionary<Guid, int>();

        /// <summary>
        /// Friendly display for startup statistic
        /// </summary>
        /// <returns></returns>
        public static string GetStartupStatistic()
        {
            return $"Startup at {StartupTime.ToChinaTime().ToIso8601()}, it has been up for {DateTime.UtcNow - StartupTime}.";
        }

        /// <summary>
        /// Friendly display for post reloading statistic
        /// </summary>
        /// <returns></returns>
        public static string GetPostStatistic()
        {
            return $"BlogPost loaded at {LastTimePostReloaded.ToChinaTime().ToIso8601()}, next refresh will happen after {LastTimePostReloaded + AppSetting.Default.BlogPostReloadInterval - DateTime.UtcNow}.";
        }
    }
}
