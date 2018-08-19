using System.Threading.Tasks;
using Laobian.Share.Data.Repository;
using Laobian.Share.Model.Job;

namespace Laobian.Jarvis.Job.Definition
{
    public interface IJob
    {
        JobTask Context { get; set; }

        IJobTaskLogRepository JobTaskLogRepository { get; set; }

        void SetContext(JobTask context);

        void SetJobTaskLogRepository(IJobTaskLogRepository jobTaskLogRepository);

        Task ExecuteAsync();
    }
}
