using System.Collections.Generic;
using System.Linq;

namespace Laobian.Share.Utility.Extension
{
    public static class ListExtension
    {
        public static List<T> ToPagedObjects<T>(this List<T> source, int chunkSize, int page)
            where T : class
        {
            if (page <= 0 || page >= source.Count()) return source;

            return source.Skip(chunkSize * (page - 1)).Take(chunkSize).ToList();
        }
    }
}