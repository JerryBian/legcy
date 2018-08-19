using System;
using System.Threading.Tasks;

namespace Laobian.Infrastuture.Interface.Service
{
    public interface ILaobianCache
    {
        bool TryGetValue<T>(string key, out T value);

        void Set<T>(string key, T value, TimeSpan relativeToNow);

        void Set<T>(string key, T value);

        T GetOrSet<T>(string key, Func<T> setValue, TimeSpan relativeToNow);

        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> setValue, TimeSpan relativeToNow);
    }
}
