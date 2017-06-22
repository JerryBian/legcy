using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary4Net
{
    public class CompressHelper
    {
        public static async Task<string> CompressAsync(string input)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var stream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    var bytes = Encoding.UTF8.GetBytes(input);
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }

                var output = outputStream.ToArray();
                return Convert.ToBase64String(output);
            }
        }

        public static string Compress(string input)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var stream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    var bytes = Encoding.UTF8.GetBytes(input);
                    stream.Write(bytes, 0, bytes.Length);
                }

                var output = outputStream.ToArray();
                return Convert.ToBase64String(output);
            }
        }

        public static async Task<string> DecompressAsync(string input)
        {
            using (var inputStream = new MemoryStream(Convert.FromBase64String(input)))
            {
                using (var stream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(outputStream);
                        var output = outputStream.ToArray();
                        return Encoding.UTF8.GetString(output, 0, output.Length);
                    }
                }
            }
        }

        public static string Decompress(string input)
        {
            using (var inputStream = new MemoryStream(Convert.FromBase64String(input)))
            {
                using (var stream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        stream.CopyTo(outputStream);
                        var output = outputStream.ToArray();
                        return Encoding.UTF8.GetString(output, 0, output.Length);
                    }
                }
            }
        }
    }
}
