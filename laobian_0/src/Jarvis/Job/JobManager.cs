using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarvis.Job
{
    public class JobManager
    {
        private readonly List<JobBase> _jobs;

        public JobManager(IServiceProvider serviceProvider)
        {
            _jobs = new List<JobBase>();
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => typeof(JobBase).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass);
            foreach (var type in types)
            {
                var job = (JobBase) Activator.CreateInstance(type, serviceProvider);
                _jobs.Add(job);
            }
        }

        public void Start()
        {
            Parallel.ForEach(_jobs, job =>
            {
                try
                {
                    job.Execute();
                }
                catch (Exception ex)
                {
                    // log the exception, and ignore it.
                }
            });
        }
    }
}