using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Share.Domain.Interface;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Infrastructure.Repository;
using Laobian.Share.Utility.Extension;

namespace Laobian.Share.Domain.Setting
{
    public class SettingService : ISettingService
    {
        private readonly IBlobDataRepository<Model.Setting> _settingRepository;

        public SettingService(IBlobDataRepository<Model.Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task<IEnumerable<Model.Setting>> FindAllAsync()
        {
            var results = new List<Model.Setting>();
            var settings = await _settingRepository.FindAllAsync();
            foreach (var name in Enum.GetNames(typeof(SettingKey)))
            {
                var setting = settings.FirstOrDefault(s => s.Key.ToString().EqualIgnoreCase(name));
                var value = setting?.Value;
                results.Add(new Model.Setting {Key = name, Value = value});
            }

            return results;
        }

        public async Task<object> FindAsync(string key)
        {
            var results = await _settingRepository.FindAllAsync();
            var setting = results.FirstOrDefault(r => r.Key.EqualIgnoreCase(key));
            return setting;
        }

        public async Task<T> FindAsync<T>(SettingKey key)
        {
            var results = await _settingRepository.FindAllAsync();
            var setting = results.FirstOrDefault(r => r.Key == key.ToString());
            if (setting == null) return default;

            return (T) Convert.ChangeType(setting.Value, typeof(T));
        }

        public async Task UpdateAsync(Model.Setting setting)
        {
            var settings = await _settingRepository.FindAllAsync();
            var existing = settings.FirstOrDefault(s => s.Key.ToString().EqualIgnoreCase(setting.Key));
            if (existing != null)
            {
                setting.Id = existing.Id;
                await _settingRepository.UpdateAsync(setting);
            }
            else
            {
                await _settingRepository.AddAsync(setting);
            }
        }
    }
}