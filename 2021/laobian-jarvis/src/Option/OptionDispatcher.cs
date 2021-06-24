using System.Threading.Tasks;
using CommandLine;

namespace Laobian.Jarvis.Option
{
    /// <summary>
    /// Dispatcher for options
    /// </summary>
    public class OptionDispatcher
    {
        /// <summary>
        /// Parse arguments, and map to options
        /// </summary>
        /// <param name="args">The given arguments</param>
        /// <returns>Task</returns>
        public static async Task ParseAsync(string[] args)
        {
            await Parser.Default
                .ParseArguments<PostOption, FileOption, ConfigOption>(args)
                .MapResult(
                    (PostOption opts) => ExecuteOptions(opts),
                    (FileOption opts) => ExecuteOptions(opts),
                    (ConfigOption opts) => ExecuteOptions(opts),
                    errs => Task.FromResult(1));
        }

        private static async Task<int> ExecuteOptions(OptionBase opts)
        {
            await opts.ExecuteAsync();
            return 0;
        }
    }
}
