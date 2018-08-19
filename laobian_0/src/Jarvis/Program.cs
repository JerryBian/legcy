using System;
using System.Threading.Tasks;
using Jarvis.Job;
using Laobian.Share.Domain;
using Laobian.Share.Infrastructure;
using Laobian.Share.Infrastructure.Email;
using Laobian.Share.Infrastructure.Interface;
using Laobian.Share.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jarvis
{
    internal class Program
    {
        private static IEmailService _emailService;
        private static IServiceProvider _serviceProvider;

        private static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            // https://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _emailService = _serviceProvider.GetService<IEmailService>();

            var jobManager = new JobManager(_serviceProvider);
            jobManager.Start();

            await _emailService.SendAsync(new EmailMessage
            {
                EmailDomain = DomainName.Jarvis,
                Message = $"Jarvis is online at [{DateTime.UtcNow}]"
            });
            await Task.Delay(-1); // make program never exists
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            if (string.Equals("development", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                StringComparison.InvariantCultureIgnoreCase)) configurationBuilder.AddUserSecrets<Program>();

            StartupHelper.ConfigureServices(services, configurationBuilder.Build());
        }

        private static async void OnProcessExit(object sender, EventArgs e)
        {
            await _emailService.SendAsync(new EmailMessage
            {
                EmailDomain = DomainName.Jarvis,
                Message = $"Jarvis is offline at [{DateTime.UtcNow}]"
            });
        }
    }
}