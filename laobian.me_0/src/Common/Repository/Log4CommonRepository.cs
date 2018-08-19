using Laobian.Infrastuture.Entity.Log;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;

namespace Laobian.Repository
{
    public class Log4CommonRepository : RepositoryBase<Log4Common>, ILog4CommonRepository
    {
        public Log4CommonRepository(IOptions<AppSettings> setting) : base(setting)
        {
        }
    }
}
