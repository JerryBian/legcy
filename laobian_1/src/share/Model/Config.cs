using Microsoft.Extensions.Configuration;

namespace Laobian.Share.Model
{
    public class Config
    {
        private readonly IConfiguration _config;

        public Config(IConfiguration config)
        {
            _config = config;
        }

        public string MySqlDatabaseConnection => _config["MySqlDatabaseConnection"];

        public string AzureStorageConnection => _config["AzureStorageConnection"];

        public string RedisConnection => _config["RedisConnection"];

        public string RabbitMqHostName => _config["RabbitMqHostName"];

        public string RabbitMqUserName => _config["RabbitMqUserName"];

        public string RabbitMqPassword => _config["RabbitMqPassword"];
    }
}
