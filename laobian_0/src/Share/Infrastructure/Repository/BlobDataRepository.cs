using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Model;

namespace Laobian.Share.Infrastructure.Repository
{
    public class BlobDataRepository<T> : IBlobDataRepository<T> where T : BlobDataBase
    {
        private readonly IBlobClient _blobClient;
        private readonly ICacheClient _cacheClient;
        private readonly string _cacheKey;

        public BlobDataRepository(IBlobClient blobClient, ICacheClient cacheClient)
        {
            _blobClient = blobClient;
            _cacheClient = cacheClient;
            _cacheKey = $"__blob_data_{typeof(T).Name}";
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await FindFirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<T> FindFirstOrDefaultAsync(Func<T, bool> predicate)
        {
            var data = await FindAllAsync();
            return data.FirstOrDefault(predicate);
        }

        public async Task<List<T>> FindAsync(Predicate<T> match)
        {
            var data = await FindAllAsync();
            return data.FindAll(match);
        }

        public async Task<List<T>> FindAllAsync()
        {
            var result = await _cacheClient.StringGetAsync<List<T>>(_cacheKey);
            if (result == null)
            {
                await RefreshCacheAsync();
                result = await _cacheClient.StringGetAsync<List<T>>(_cacheKey);
            }

            return result;
        }

        public async Task AddAsync(T blobData, bool createSnapshot = false)
        {
            try
            {
                var data = await FindAllAsync();
                if (data.Exists(d => d.Id == blobData.Id)) throw new InvalidOperationException();

                data.Add(blobData);
                await _blobClient.UploadDataAsync(data, createSnapshot);
                await RefreshCacheAsync();
            }
            finally
            {
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> blobDatas, bool createSnapshot = false)
        {

            try
            {
                var data = await FindAllAsync();

                data.AddRange(blobDatas);
                await _blobClient.UploadDataAsync(data, createSnapshot);
                await RefreshCacheAsync();
            }
            finally
            {
            }
        }

        public async Task UpdateAsync(T blobData, bool createSnapshot = false)
        {

            try
            {
                var data = await FindAllAsync();
                var existing = data.FirstOrDefault(d => d.Id == blobData.Id);

                if (existing == null) throw new InvalidOperationException();

                blobData.CreateAt = existing.CreateAt;

                data.Remove(existing);
                data.Add(blobData);

                await _blobClient.UploadDataAsync(data, createSnapshot);
                await RefreshCacheAsync();
            }
            finally
            {
            }
        }

        public async Task RemoveAsync(IEnumerable<T> blobDatas)
        {

            try
            {
                var data = await FindAllAsync();
                data.RemoveAll(d => blobDatas.Select(b => b.Id).Contains(d.Id));

                await _blobClient.UploadDataAsync(data);
                await RefreshCacheAsync();
            }
            finally
            {
            }
        }

        private async Task RefreshCacheAsync()
        {
            var data = await _blobClient.DownloadDataAsync<T>();
            await _cacheClient.StringSetAsync(_cacheKey, data, TimeSpan.FromDays(1));
        }
    }
}