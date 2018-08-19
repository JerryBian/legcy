using System;
using System.Threading.Tasks;
using Laobian.Share.Model.Job;

namespace Laobian.Jarvis.Job.Definition
{
    public class TestJob2 : JobBase
    {
        public override async Task ExecuteAsync()
        {
            await JobTaskLogRepository.AddAsync(new JobTaskLog
            {
                JobId = Context.JobId,
                TaskId = Context.TaskId,
                Message = $"@{DateTime.UtcNow} TestJob2"
            });
            await Task.CompletedTask;
        }
    }
}
