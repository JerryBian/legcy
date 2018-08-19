using System;
using System.Threading;
using System.Threading.Tasks;
using Laobian.Jarvis.Job;
using Laobian.Share.Data.Repository;
using Laobian.Share.Log;
using Laobian.Share.Log.Logger;
using Laobian.Share.Model;
using Laobian.Share.Model.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laobian.Jarvis.Console
{
    public class Program
    {
        public static async Task Main()
        {
            var serviceProvider = Build();
            var jobMaster = serviceProvider.GetService<JobMaster>();
            var cts = new CancellationTokenSource();

            await jobMaster.StartAsync(cts.Token);
            await jobMaster.RunAsync(cts.Token);


            System.Console.ReadKey();
        }

        private static IServiceProvider Build()
        {
            var serviceCollection = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();

            serviceCollection.AddSingleton<IJobRepository, JobRepository>();
            serviceCollection.AddSingleton<JobMaster>();

            serviceCollection.AddSingleton(configurationBuilder.Build() as IConfiguration);
            serviceCollection.AddSingleton<Config>();

            serviceCollection.AddSingleton<ILogRepository<JarvisLogItem>, JarvisLogRepository>();
            serviceCollection.AddSingleton<IJobTaskLogRepository, JobTaskLogRepository>();

            serviceCollection.AddLogging(configure => configure.AddDb<JarvisLogItem, JarvisLogger>());

            return serviceCollection.BuildServiceProvider();
        }
    }
}
