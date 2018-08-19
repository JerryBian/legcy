using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laobian.Share.Model;
using Laobian.Share.Model.Log;

namespace Laobian.Share.Data.Repository
{
    public class JarvisLogRepository : LogRepository<JarvisLogItem>
    {
        public JarvisLogRepository(Config config) : base(config)
        {
        }

        public override async Task AddAsync(List<JarvisLogItem> logs)
        {
            if (logs.Any())
            {
                var columnItems = logs.Select(l => l.GetColumnItems()).ToList();
                await AddAsync("jarvis_log", columnItems);
            }
        }
    }
}
