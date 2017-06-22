using Laobian.Infrastuture.Entity.Log;
using Microsoft.Extensions.Options;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Model;

namespace Laobian.Repository
{
    public class Log4BlogVisitRepository : RepositoryBase<Log4BlogVisit>, ILog4BlogVisitRepository
    {
        public Log4BlogVisitRepository(IOptions<AppSettings> setting) : base(setting)
        {
        }
    }
}
