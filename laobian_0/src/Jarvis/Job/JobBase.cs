using System;
using System.Threading.Tasks;
using Jarvis.Strategy;
using Microsoft.Extensions.DependencyInjection;

namespace Jarvis.Job
{
    public abstract class JobBase
    {
        protected ServiceProvider ServiceProvider;

        protected JobBase(ServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public JobStrategy Strategy { get; set; }

        public Action JobAction { get; set; }

        public virtual void Execute()
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await ExecuteInternalAsync();
                }
                catch (Exception ex)
                {
                    // log the exception and pass through
                }
            }, TaskCreationOptions.LongRunning);
        }

        private async Task ExecuteInternalAsync()
        {
            if (Strategy.RunOnlyOnce)
            {
                if (Strategy.RunNow()) JobAction();

                return;
            }

            while (true)
            {
                if (Strategy.RunNow())
                {
                    JobAction();
                    Strategy.LastRunAt = DateTime.UtcNow;
                }

                // try to make CPU not that busy
                await Task.Delay(500);
            }
        }
    }
}