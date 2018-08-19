using Laobian.Share.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;

namespace Laobian.Share.Infrastructure.Config
{
    public class ConfigSetting
    {
        private readonly IConfiguration _config;

        public ConfigSetting(IConfiguration config)
        {
            _config = config;
        }

        public string StorageConnectionString => _config["StorageConnectionString"];

        public string AdminEmail => _config["AdminEmail"];

        public string AdminPassword => _config["AdminPassword"];

        public string AdminFullName => _config["AdminFullName"];

        public string RedisConnectionString => _config["RedisConnectionString"];

        public string SendGridApiKey => _config["SendGridApiKey"];

        public User AdminUser => new User
        {
            Email = AdminEmail,
            FullName = AdminFullName,
            Password = AdminPassword
        };
    }
}