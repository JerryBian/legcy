namespace Laobian.Shared.Interface
{
    public class Constants
    {
#if DEBUG
        public const string BlogBaseUrl = "";
#else
        public const string BlogBaseUrl = "https://blog.laobian.me";
#endif
        public const string Domain = "laobian.me";
    }
}
