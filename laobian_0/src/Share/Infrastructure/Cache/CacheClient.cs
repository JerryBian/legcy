using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Infrastructure.Config;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Utility.Helper;
using StackExchange.Redis;

namespace Laobian.Share.Infrastructure.Cache
{
    public class CacheClient : ICacheClient
    {
        private readonly ConnectionMultiplexer _connection;

        public CacheClient(ConfigSetting configSetting)
        {
            var config = new ConfigurationOptions
            {
                AllowAdmin = true,
                ClientName = "laobian"
            };
            config.EndPoints.Add(configSetting.RedisConnectionString);
            _connection = ConnectionMultiplexer.Connect(config);
            _connection.PreserveAsyncOrder = false; // if set true, we usually see dead lock
        }

        public async Task<T> StringGetAsync<T>(string key)
        {
            var db = _connection.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (value == RedisValue.Null) return default;

            return SerializeHelper.DeserializeFromJson<T>(value);
        }

        public async Task<int> StringGetSetAsync(string key, int obj)
        {
            var db = _connection.GetDatabase();
            return (int)await db.StringGetSetAsync(key, obj);
        }

        public async Task StringSetAsync<T>(string key, T obj, TimeSpan expire)
        {
            var db = _connection.GetDatabase();
            await db.StringSetAsync(key, SerializeHelper.SerializeToJson(obj), expire);
        }

        public async Task StringIncrementAsync(string key)
        {
            var db = _connection.GetDatabase();
            await db.StringIncrementAsync(key);
        }

        public async Task SetAddAsync(string key, string value)
        {
            var db = _connection.GetDatabase();
            await db.SetAddAsync(key, value);
        }

        public async Task<IEnumerable<string>> SetMembersAsync(string key)
        {
            var members = new List<string>();
            var db = _connection.GetDatabase();
            var result = await db.SetMembersAsync(key);
            foreach (string redisValue in result)
            {
                members.Add(redisValue);
            }

            return members;
        }

        public async Task KeyDeleteAsync(string key)
        {
            var db = _connection.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}