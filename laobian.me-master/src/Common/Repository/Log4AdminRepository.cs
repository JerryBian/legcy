using Laobian.Infrastuture.Entity.Log;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;

namespace Laobian.Repository
{
    public class Log4AdminRepository : RepositoryBase<Log4Admin>, ILog4AdminRepository
    {
        public Log4AdminRepository(IOptions<AppSettings> setting) : base(setting)
        {
        }
    }
}
