using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Config;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Model;
using Laobian.Share.Utility.Helper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Laobian.Share.Infrastructure.Blob
{
    public class BlobClient : IBlobClient
    {
        private readonly CloudBlobClient _cloudBlobClient;

        public BlobClient(ConfigSetting config)
        {
            var storageAccount = CloudStorageAccount.Parse(config.StorageConnectionString);
            _cloudBlobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task UploadDataAsync<T>(List<T> obj, bool createSnapshot = false) where T : BlobDataBase
        {
            var container = await GetBlobContainerAsync("data", false);
            var blob = container.GetBlockBlobReference(typeof(T).Name.ToLowerInvariant());

            var json = SerializeHelper.SerializeToJson(obj, Formatting.Indented);
            await blob.UploadTextAsync(json);
            if (createSnapshot) await blob.CreateSnapshotAsync();
        }

        public async Task<List<T>> DownloadDataAsync<T>() where T : BlobDataBase
        {
            var container = await GetBlobContainerAsync("data", false);
            var blob = container.GetBlockBlobReference(typeof(T).Name.ToLowerInvariant());
            if (!await blob.ExistsAsync()) return null;

            var json = await blob.DownloadTextAsync();
            return SerializeHelper.DeserializeFromJson<List<T>>(json);
        }

        public async Task<string> UploadFileAsync(byte[] content, string fileName)
        {
            var container = await GetBlobContainerAsync("share");
            // wierd wierd!!! It seems there is no simple API to get list of blobs
            var blob = container.GetBlockBlobReference(
                $"{Path.GetRandomFileName().Replace(".", string.Empty)}/{fileName.ToLowerInvariant()}");

            await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<CloudBlobContainer> GetBlobContainerAsync(string area, bool share = true)
        {
            var container = _cloudBlobClient.GetContainerReference(area.ToLowerInvariant());
            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = share ? BlobContainerPublicAccessType.Container : BlobContainerPublicAccessType.Off
                });
            }

            return container;
        }
    }
}