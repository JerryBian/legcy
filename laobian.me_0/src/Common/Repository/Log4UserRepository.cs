using Laobian.Infrastuture.Entity.Log;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;

namespace Laobian.Repository
{
    public class Log4UserRepository : RepositoryBase<Log4User>, ILog4UserRepository
    {
        public Log4UserRepository(IOptions<AppSettings> setting) : base(setting)
        {
        }
    }
}
