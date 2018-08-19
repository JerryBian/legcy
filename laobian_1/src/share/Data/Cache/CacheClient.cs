using System;
using Laobian.Share.Model;
using StackExchange.Redis;

namespace Laobian.Share.Data.Cache
{
    public interface ICacheClient
    {
        IDatabase CreateDatabase();
    }

    public class CacheClient : ICacheClient
    {
        private readonly ConnectionMultiplexer _connection;

        public CacheClient(Config config)
        {
            var option = new ConfigurationOptions
            {
                AllowAdmin = true,
                ClientName = Environment.MachineName
            };

            option.EndPoints.Add(config.RedisConnection);
            _connection = ConnectionMultiplexer.Connect(option);
            _connection.PreserveAsyncOrder = false;
        }

        public IDatabase CreateDatabase()
        {
            return _connection.GetDatabase();
        }
    }
}
