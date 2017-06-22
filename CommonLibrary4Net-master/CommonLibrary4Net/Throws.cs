using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary4Net
{
    public sealed class Throws
    {
        public static void IfNull(object obj, string message = "")
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), message);
            }
        }

        public static void IfNullOrEmpty(string str, string message = "")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message, nameof(str));
            }
        }

        public static void IfNullOrEmpty<T>(IEnumerable<T> elements, string message = "")
        {
            if (elements == null || !elements.Any())
            {
                throw new ArgumentException(message, nameof(elements));
            }
        }

        public static void IfNullOrEmptyOrWhitespace(string str, string message = "")
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(message, nameof(str));
            }
        }

        public static void IfContainsNull<T>(IEnumerable<T> source, string message = "") where T : class
        {
            if (source.Any(_ => _ == null))
            {
                throw new ArgumentException(message, nameof(source));
            }
        }
    }
}
