using Laobian.Infrastuture.Entity.Log;
using Laobian.Infrastuture.Interface.Repository;
using Laobian.Infrastuture.Interface.Service;
using System.Collections.Generic;

namespace Laobian.Service.Tasks
{
    public class Log4BlogVisitTask : TaskBase<Log4BlogVisit>, ILog4BlogVisitTask
    {
        private readonly ILog4BlogVisitRepository _log4BlogVisitRepository;

        public Log4BlogVisitTask(ILog4BlogVisitRepository log4BlogVisitRepository)
        {
            _log4BlogVisitRepository = log4BlogVisitRepository;
        }

        public override void DoWork()
        {
            DoWork(() =>
            {
                if (TryDequeueAll(out List<Log4BlogVisit> elements))
                {
                    _log4BlogVisitRepository.AddAsync(elements).Wait();
                    return true;
                }

                return false;
            });
        }
    }
}
