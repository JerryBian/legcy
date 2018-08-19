using System;

namespace Laobian.Shared.Interface.Blog
{
    public class BlogLang
    {
        public const string English = "en";

        public const string Chinese = "zh";

        public static string GetNormalizedLang(string lang)
        {
            if (string.IsNullOrEmpty(lang))
            {
                return English;
            }

            return lang.Equals(Chinese, StringComparison.InvariantCultureIgnoreCase) ? Chinese : English;
        }

        public static bool IsEnglish(string lang)
        {
            return GetNormalizedLang(lang) == English;
        }
    }
}
