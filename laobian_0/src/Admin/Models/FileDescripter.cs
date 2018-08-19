using System.Collections.Generic;

namespace Laobian.Admin.Models
{
    public class FileDescripter
    {
        public static int MaxSizeInKb => 1024 * 100; // allow 100MB

        public static string[] AllowedExtension
        {
            get
            {
                var result = new List<string>();
                result.AddRange(AllowedImageExtension);
                return result.ToArray();
            }
        }

        public static string[] AllowedImageExtension => new[]
        {
            "jpg",
            "jpeg",
            "png",
            "bmp",
            "gif"
        };
    }
}