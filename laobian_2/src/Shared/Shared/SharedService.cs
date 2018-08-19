using Laobian.Shared.Interface;
using System.Threading.Tasks;

namespace Laobian.Shared
{
    public class SharedService : ISharedService
    {
        private readonly ISharedRepository _sharedRepository;

        public SharedService(ISharedRepository sharedRepository)
        {
            _sharedRepository = sharedRepository;
        }

        public async Task<string> GetSettingAsync(string key)
        {
            var settings = await _sharedRepository.GetSettingsAsync();
            return settings[key];
        }
    }
}
