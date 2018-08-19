using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Model;

namespace Laobian.Share.Infrastructure.Interface
{
    public interface IBlobDataRepository<T> where T : BlobDataBase
    {
        Task<T> FindAsync(Guid id);

        Task<T> FindFirstOrDefaultAsync(Func<T, bool> predicate);

        Task<List<T>> FindAsync(Predicate<T> match);

        Task<List<T>> FindAllAsync();

        Task AddAsync(T blobData, bool createSnapshot = false);

        Task AddRangeAsync(IEnumerable<T> blobDatas, bool createSnapshot = false);

        Task UpdateAsync(T blobData, bool createSnapshot = false);

        Task RemoveAsync(IEnumerable<T> blobDatas);
    }
}