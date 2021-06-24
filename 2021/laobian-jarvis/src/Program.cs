using Laobian.Jarvis.Model;
using System.Threading.Tasks;
using Laobian.Jarvis.Log;
using Laobian.Jarvis.Option;

namespace Laobian.Jarvis
{
    /// <summary>
    /// Entry point of application
    /// </summary>
    public class Program
    {
        private static async Task Main(string[] args)
        {
            await FileLogger.Default.StartAsync();

            await JarvisOut.VerbAsync($"App started with arguments: {string.Join(" ", args)}");

            await OptionDispatcher.ParseAsync(args);

            await JarvisOut.VerbAsync("App executed completed successfully");
            await FileLogger.Default.StopAsync();
        }
    }
}
