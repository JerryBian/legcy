using System;
using System.Threading.Tasks;
using Laobian.Common.Config;
using Laobian.Jarvis.Model;

namespace Laobian.Jarvis.Option
{
    /// <summary>
    /// Option base
    /// </summary>
    public abstract class OptionBase
    {
        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Task</returns>
        public async Task ExecuteAsync()
        {
            try
            {
                if (!IsAzureStorageConnectionValid())
                {
                    await JarvisOut.ErrorAsync("No azure storage connection was configured, please use 'config -n AzureStorageConnectionString -v XXX' to get it fixed.");
                    return;
                }

                await ExecuteInternalAsync();
                await JarvisOut.VerbAsync("Command executed completed.");
            }
            catch (Exception ex)
            {
                await JarvisOut.ErrorAsync($"Fail - {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Execute logic internally, this should be implemented by each option
        /// </summary>
        /// <returns>Task</returns>
        protected abstract Task ExecuteInternalAsync();

        /// <summary>
        /// Verify whether azure storage connection set or not
        /// </summary>
        /// <returns>True if azure storage connection set, otherwise not</returns>
        protected virtual bool IsAzureStorageConnectionValid()
        {
            return !string.IsNullOrEmpty(AppConfig.Default.AzureStorageConnection);
        }
    }
}
