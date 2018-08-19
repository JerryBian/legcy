using System.Collections.Generic;
using System.Threading.Tasks;

namespace Laobian.Shared
{
    public interface ISharedRepository
    {
        Task<IDictionary<string, string>> GetSettingsAsync();
    }
}
