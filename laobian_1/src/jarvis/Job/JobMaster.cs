using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Laobian.Jarvis.Job.Definition;
using Laobian.Share.Data.Repository;
using Microsoft.Extensions.Logging;

namespace Laobian.Jarvis.Job
{
    /// <summary>
    /// Manage and schedule jobs. Entry point.
    /// </summary>
    public class JobMaster
    {
        private readonly IJobTaskLogRepository _jobTaskLogRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger _logger;

        public JobMaster(
            IJobTaskLogRepository jobTaskLogRepository,
            IJobRepository jobRepository,
            ILogger<JobMaster> logger)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _jobTaskLogRepository = jobTaskLogRepository;
        }

        /// <summary>
        /// Start a background task to push new tasks to queue.
        /// As a forever running task, it will create task and push it to job_task table.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Backgroup starts");
            await Task.Factory.StartNew(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _jobRepository.UpdateJobTaskAsync();
                        await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "Backgroud loop failed.");
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Pull from task queue, and create separate tasks in memory, running to the end.
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var tasks = await _jobRepository.GetNextTasksAsync();
                        var taskBag = new List<Task>();
                        foreach (var task in tasks.OrderBy(t => t.Priority))
                        {
                            try
                            {
                                var t = (IJob)Activator.CreateInstance(Type.GetType(task.ClassName));
                                t.SetContext(task);
                                t.SetJobTaskLogRepository(_jobTaskLogRepository);
                                taskBag.Add(t.ExecuteAsync());
                                _logger.LogInformation(task.ClassName);
                            }
                            catch
                            {
                                throw;
                            }
                        }

                        if (taskBag.Any())
                        {
                            await Task.WhenAll(taskBag);
                        }
                        else
                        {
                            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        public async Task StopAsync()
        {

        }
    }
}
