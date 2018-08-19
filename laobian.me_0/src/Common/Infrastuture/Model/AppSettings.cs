namespace Laobian.Infrastuture.Model
{
    public class AppSettings
    {
        public string MySqlConnectionString { get; set; }

        public string SendGridApiKey { get; set; }

        public string StorageConnectionString { get; set; }

        public string BlogHost { get; set; }

        public string AdminHost { get; set; }

        public string HomeHost { get; set; }

        public string ApiHost { get; set; }

        public string Salt { get; set; }

        public string AdminUserName { get; set; }

        public string AdminPassword { get; set; }

        public string AdminEmail { get; set; }

        public string AdminFullName { get; set; }
    }
}
