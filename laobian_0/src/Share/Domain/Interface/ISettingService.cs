using System.Collections.Generic;
using System.Threading.Tasks;
using Laobian.Share.Domain.Setting;

namespace Laobian.Share.Domain.Interface
{
    public interface ISettingService
    {
        Task<T> FindAsync<T>(SettingKey key);

        Task<IEnumerable<Model.Setting>> FindAllAsync();

        Task UpdateAsync(Model.Setting setting);

        Task<object> FindAsync(string key);
    }
}