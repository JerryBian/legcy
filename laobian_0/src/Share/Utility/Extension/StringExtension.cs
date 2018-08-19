using System;

namespace Laobian.Share.Utility.Extension
{
    public static class StringExtension
    {
        public static bool EqualIgnoreCase(this string left, string right)
        {
            return string.Equals(left, right, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}