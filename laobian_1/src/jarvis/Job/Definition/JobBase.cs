using System.Threading.Tasks;
using Laobian.Share.Data.Repository;
using Laobian.Share.Model.Job;

namespace Laobian.Jarvis.Job.Definition
{
    public abstract class JobBase : IJob
    {
        public IJobTaskLogRepository JobTaskLogRepository { get; set; }

        public JobTask Context { get; set; }

        public virtual void SetContext(JobTask context)
        {
            Context = context;
        }

        public virtual void SetJobTaskLogRepository(IJobTaskLogRepository jobTaskLogRepository)
        {
            JobTaskLogRepository = jobTaskLogRepository;
        }

        public abstract Task ExecuteAsync();
    }
}