using System;
using System.Collections.Generic;
using System.Linq;

namespace Laobian.Shared.Interface
{
    public static class Extensions
    {
        public static string ToRelativeTime(this DateTime time)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - time.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * minute)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * minute)
                return "a minute ago";

            if (delta < 45 * minute)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * minute)
                return "an hour ago";

            if (delta < 24 * hour)
                return ts.Hours + " hours ago";

            if (delta < 48 * hour)
                return "yesterday";

            if (delta < 30 * day)
                return ts.Days + " days ago";

            if (delta < 12 * month)
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public static string ToMonthAndDay(this DateTime time)
        {
            return time.ToString("MMM dd");
        }

        public static string ToYear(this DateTime time)
        {
            return time.ToString("yyyy");
        }

        public static string ToStackOverflowStyle(this DateTime time)
        {
            return time.ToString("MMM dd\"'\"yy 'at' hh:mm");
        }

        public static IEnumerable<T> ToPagedObjects<T>(this List<T> source, int chunkSize, int page) where T : class
        {
            if (page <= 0 || page >= source.Count)
            {
                return source;
            }

            return source.Skip(chunkSize * (page - 1)).Take(chunkSize);
        }
    }
}
