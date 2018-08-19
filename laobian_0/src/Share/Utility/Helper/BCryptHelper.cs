namespace Laobian.Share.Utility.Helper
{
    public class BCryptHelper
    {
        public static string HashString(string str)
        {
            return BCrypt.Net.BCrypt.HashString(str);
        }

        public static bool VerifyHash(string str, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(str, hash);
        }
    }
}