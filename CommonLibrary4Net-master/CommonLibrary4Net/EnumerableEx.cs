using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary4Net
{
    public class EnumerableEx
    {
        public IEnumerable<IEnumerable<T>> Chunk<T>(IEnumerable<T> source, int chunkSize)
        {
            while (source.Any())
            {
                yield return source.Take(chunkSize);
                source = source.Skip(chunkSize);
            }
        }
    }
}
