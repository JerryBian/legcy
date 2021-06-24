using System.IO;
using Laobian.Common.Setting;

namespace Laobian.Common.Azure
{
    /// <summary>
    /// Centralized place for managing Azure Blob namings
    /// </summary>
    public class PrivateBlobResolver
    {
        /// <summary>
        /// Get the Blog BlogPost blob name
        /// </summary>
        /// <returns>Normalized string represents Blog BlogPost blob name</returns>
        public static string PostBlob()
        {
            return ComposeBlobName("blog", "post");
        }

        /// <summary>
        /// Normalize given name to accepted one
        /// </summary>
        /// <param name="name">The given name</param>
        /// <returns>Normalized string can accepted by Microsoft Azure Blob</returns>
        public static string Normalize(string name)
        {
            return name.ToLowerInvariant();
        }

        /// <summary>
        /// Normalize given container name to accepted one
        /// </summary>
        /// <para name="containerName">
        /// The given container
        /// </para>
        /// <returns>Normalized string can accepted by Microsoft Azure Blob</returns>
        public static string Normalize(BlobContainer containerName)
        {
            return Normalize(containerName.ToString());
        }

        /// <summary>
        /// Get parent of specified blob name
        /// </summary>
        /// <param name="name">The blob name</param>
        /// <returns>Parent path</returns>
        public static string GetParent(string name)
        {
            var index = name.LastIndexOf('/');
            if (index < 0)
            {
                return string.Empty;
            }

            return name.Substring(0, index);
        }

        /// <summary>
        /// Get blob name
        /// </summary>
        /// <param name="name">The specified blob full path</param>
        /// <param name="trimExtension">Whether exclude extension or not</param>
        /// <returns>The blob name</returns>
        public static string GetName(string name, bool trimExtension = true)
        {
            var index = name.LastIndexOf('/');
            if (index < 0)
            {
                return name;
            }

            var fileName = name.Substring(index + 1);
            return trimExtension ? Path.GetFileNameWithoutExtension(fileName) : fileName;
        }

        /// <summary>
        /// Compose blob name according to provided parts
        /// </summary>
        /// <param name="baseName">Base name of blob</param>
        /// <param name="blobName">The blob name</param>
        /// <param name="includeContainer"></param>
        /// <param name="subFolders">The sub folders between base name and blob name, they will be composed in provided order</param>
        /// <returns>The composed blob name</returns>
        public static string ComposeBlobName(
            string baseName,
            string blobName = "",
            bool includeContainer = false,
            params string[] subFolders)
        {
            // base name may or not contains container name
            if (baseName.StartsWith(Normalize(BlobContainer.Private)))
            {
                baseName = baseName.Remove(0, $"{BlobContainer.Private}/".Length);
            }

            var name = includeContainer ? $"{BlobContainer.Private}/{baseName}" : baseName;
            foreach (var subFolder in subFolders)
            {
                name = $"{name}/{subFolder}";
            }

            if (!string.IsNullOrEmpty(blobName) && !blobName.EndsWith(AppSetting.Default.ProtoBufExtension))
            {
                blobName = $"{blobName}{AppSetting.Default.ProtoBufExtension}";
                name = $"{name}/{blobName}";
            }

            return Normalize(name);
        }
    }
}
