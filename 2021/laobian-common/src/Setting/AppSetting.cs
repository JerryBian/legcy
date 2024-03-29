﻿using System;

namespace Laobian.Common.Setting
{
    /// <summary>
    /// App Settings, they are not secrets
    /// </summary>
    public sealed class AppSetting
    {
        private static readonly Lazy<AppSetting> LazyDefault = new Lazy<AppSetting>(() => new AppSetting(), true);

        /// <summary>
        /// Singleton instance of <see cref="AppSetting"/>
        /// </summary>
        public static AppSetting Default => LazyDefault.Value;

        private AppSetting()
        {
        }

        /// <summary>
        /// Admin email address
        /// </summary>
        public string AdminEmail { get; set; } = "JerryBian@outlook.com";

        /// <summary>
        /// Admin full name
        /// </summary>
        public string AdminFullName { get; set; } = "Jerry Bian";

        /// <summary>
        /// Notification email address
        /// </summary>
        public string NotifyEmail { get; set; } = "notify@laobian.me";

        /// <summary>
        /// Notification name
        /// </summary>
        public string NotifyName { get; set; } = "laobian notification";

        /// <summary>
        /// Blog name
        /// </summary>
        public string BlogName { get; set; } = "Jerry Bian's blog";

        /// <summary>
        /// Reload interval for blog posts
        /// </summary>
        public TimeSpan BlogPostReloadInterval { get; set; } = TimeSpan.FromHours(12);

        /// <summary>
        /// Pending buffer size for visit logs
        /// </summary>
        public int VisitLogBufferSize { get; set; } = 100;

        /// <summary>
        /// Flush interval for visit logs
        /// </summary>
        public TimeSpan VisitLogFlushInterval { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// Pending buffer size for status logs
        /// </summary>
        public int StatusLogBufferSize { get; set; } = 50;

        /// <summary>
        /// Flush interval for status logs
        /// </summary>
        public TimeSpan StatusLogFlushInterval { get; set; } = TimeSpan.FromDays(7);

        /// <summary>
        /// Admin chinese name
        /// </summary>
        public string ChineseName { get; set; } = "卞良忠";

        /// <summary>
        /// Blog description
        /// </summary>
        public string BlogDescription { get; set; } = "个人博客，记录所思所得。";

        /// <summary>
        /// Extension for ProtoBuf
        /// </summary>
        public string ProtoBufExtension => ".proto";
    }
}
