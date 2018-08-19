using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Laobian.Share.Infrastructure.Interface
{
    public interface ICacheClient
    {
        Task<T> StringGetAsync<T>(string key);

        Task<int> StringGetSetAsync(string key, int obj);

        Task StringSetAsync<T>(string key, T obj, TimeSpan expire);

        Task StringIncrementAsync(string key);

        Task SetAddAsync(string key, string value);

        Task<IEnumerable<string>> SetMembersAsync(string key);

        Task KeyDeleteAsync(string key);
    }
}