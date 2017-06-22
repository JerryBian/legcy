using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Laobian.Infrastuture.Extension
{
    public static class StringExtension
    {
        public static string Md5(this string input)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(bytes);
                var sb = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string AesEncrypt(this string input, string salt)
        {
            var clearBytes = Encoding.Unicode.GetBytes(input);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(salt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    input = Convert.ToBase64String(ms.ToArray());
                }
            }

            return input;
        }

        public static string AesDecrypt(this string input, string salt)
        {
            input = input.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(input);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(salt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    input = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return input;
        }
    }
}
