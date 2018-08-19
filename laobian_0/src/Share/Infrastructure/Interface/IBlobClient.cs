using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Blob;
using Laobian.Share.Model;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Laobian.Share.Infrastructure.Interface
{
    /// <summary>
    /// Client for operating Azure Blob
    /// </summary>
    public interface IBlobClient
    {
        /// <summary>
        /// Upload data to blob, if it's alreay exists it will be overriden.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="createSnapshot">Whether we should create snapshot for this.</param>
        /// <returns></returns>
        Task UploadDataAsync<T>(List<T> obj, bool createSnapshot = false) where T : BlobDataBase;

        /// <summary>
        /// Download data from blog. If it's not found, null will be return
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<T>> DownloadDataAsync<T>() where T : BlobDataBase;

        Task<string> UploadFileAsync(byte[] content, string fileName);

        Task<CloudBlobContainer> GetBlobContainerAsync(string area, bool share = true);
    }
}