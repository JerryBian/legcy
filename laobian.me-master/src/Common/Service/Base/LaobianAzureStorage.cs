using Laobian.Infrastuture.Interface.Service;
using Laobian.Infrastuture.Model;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace Laobian.Service.Base
{
    public class LaobianAzureStorage : ILaobianAzureStorage
    {
        private readonly CloudStorageAccount _storageAccount;
        private const string AccessUrlBase = "https://laobian.blob.core.windows.net";

        public LaobianAzureStorage(
            IOptions<AppSettings> setting)
        {
            _storageAccount = CloudStorageAccount.Parse(setting.Value.StorageConnectionString);
        }

        public async Task<string> UploadFileAsync(string fileName, byte[] content)
        {
            fileName = fileName.ToLowerInvariant();
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("upload");
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            var blob = container.GetBlockBlobReference(fileName);
            var index = 0;
            while(await blob.ExistsAsync())
            {
                fileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{index}{Path.GetExtension(fileName)}";
                blob = container.GetBlockBlobReference(fileName);
                index++;
            }

            await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            return $"{AccessUrlBase}/upload/{fileName}";
        }
    }
}
