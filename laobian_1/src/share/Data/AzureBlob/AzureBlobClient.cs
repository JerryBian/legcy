using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Share.Model;
using Laobian.Share.Model.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Laobian.Share.Data.AzureBlob
{
    public interface IAzureBlobClient
    {
        Task<List<BlobAttribute>> ListBlobsAsync(ContainerAttribute containerAttr);

        Task<string> UploadBlobAsync(ContainerAttribute containerAttr, string baseType, string fileName, byte[] buffer);
    }

    public class AzureBlobClient : IAzureBlobClient
    {
        private readonly CloudStorageAccount _storageAccount;

        public AzureBlobClient(Config config)
        {
            _storageAccount = CloudStorageAccount.Parse(config.AzureStorageConnection);
        }

        public async Task<List<BlobAttribute>> ListBlobsAsync(ContainerAttribute containerAttr)
        {
            var client = _storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerAttr.Name);

            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();
                await container.SetPermissionsAsync(containerAttr.IsPublic
                    ? new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container }
                    : new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
            }

            return await ListBlobsAsync(null, container);
        }

        public async Task<string> UploadBlobAsync(ContainerAttribute containerAttr, string baseType, string fileName, byte[] buffer)
        {
            var client = _storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerAttr.Name);

            if (!await container.ExistsAsync())
            {
                await container.CreateAsync();
                await container.SetPermissionsAsync(containerAttr.IsPublic
                    ? new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container }
                    : new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
            }

            var blob = container.GetBlockBlobReference($"{baseType}/{fileName}");
            var i = 0;
            while (await blob.ExistsAsync())
            {
                blob = container.GetBlockBlobReference(
                    $"{baseType}/{Path.GetFileNameWithoutExtension(fileName)}_{i}{Path.GetExtension(fileName)}");
                i++;
            }

            await blob.UploadFromByteArrayAsync(buffer, 0, buffer.Length);
            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        private async Task<List<BlobAttribute>> ListBlobsAsync(string prefix, CloudBlobContainer container)
        {
            var blobs = new List<BlobAttribute>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.All, new int?(), blobContinuationToken, null, null);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (var item in results.Results.Cast<CloudBlockBlob>().OrderByDescending(b => b.Properties.Created))
                {
                    blobs.Add(new BlobAttribute
                    {
                        Create = item.Properties.Created,
                        Uri = item.StorageUri.PrimaryUri.AbsoluteUri,
                        Size = item.Properties.Length,
                        Name = item.Name.Substring(item.Name.IndexOf('/') + 1),
                        BaseType = item.Name.Substring(0, item.Name.IndexOf('/'))
                    });
                }

            } while (blobContinuationToken != null); // Loop while the continuation token is not null. 

            return blobs;
        }
    }
}
