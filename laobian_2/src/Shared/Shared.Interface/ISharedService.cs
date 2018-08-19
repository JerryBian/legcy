using System.Threading.Tasks;

namespace Laobian.Shared.Interface
{
    public interface ISharedService
    {
        Task<string> GetSettingAsync(string key);
    }
}
