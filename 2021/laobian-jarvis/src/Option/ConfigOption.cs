using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using Laobian.Common.Base;
using Laobian.Common.Config;
using Laobian.Jarvis.Model;

namespace Laobian.Jarvis.Option
{
    /// <summary>
    /// Config option
    /// </summary>
    [Verb("config", HelpText = "Manage configurations in locally.")]
    public class ConfigOption : OptionBase
    {
        /// <summary>
        /// List all configurations locally.
        /// </summary>
        [Option('l', "list", HelpText = "List all configurations locally.")]
        public bool List { get; set; }

        /// <summary>
        /// Configuration name.
        /// </summary>
        [Option('n', "name", HelpText = "Configuration name.")]
        public string Name { get; set; }

        /// <summary>
        /// Configuration value.
        /// </summary>
        [Option('v', "value", HelpText = "Configuration value.")]
        public string Value { get; set; }

        /// <inheritdoc />
        protected override async Task ExecuteInternalAsync()
        {
            if (List)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    await JarvisOut.InfoAsync("{0}\t{1}", "Local setting home", AppConfig.Default.GetLocalLocation());
                    // list all existing configurations
                    foreach (var propertyInfo in typeof(AppConfig).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        var name = propertyInfo.Name;
                        var value = propertyInfo.GetValue(AppConfig.Default);
                        await JarvisOut.InfoAsync("{0}\t{1}", name, value);
                    }
                }
                else
                {
                    foreach (var propertyInfo in typeof(AppConfig).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        var name = propertyInfo.Name;
                        if (name.EqualsIgnoreCase(Name))
                        {
                            var value = propertyInfo.GetValue(AppConfig.Default);
                            await JarvisOut.InfoAsync("{0}\t{1}", name, value);
                            return;
                        }
                    }

                    await JarvisOut.ErrorAsync("Invalid configuration name.");
                }

                return;
            }

            if (string.IsNullOrEmpty(Name))
            {
                await JarvisOut.ErrorAsync("--name must be specified for add or update.");
                return;
            }

            var prop = typeof(AppConfig).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(ps => ps.Name.EqualsIgnoreCase(Name));
            if (prop == null)
            {
                await JarvisOut.ErrorAsync($"Invalid config name {Name}");
                return;
            }

            if (string.IsNullOrEmpty(Value))
            {
                await JarvisOut.ErrorAsync("--value must be specified for add or update.");
                return;
            }

            prop.SetValue(AppConfig.Default, Value);
            AppConfig.Default.StoreToLocalConfig();
            await JarvisOut.InfoAsync("Configuration {0} set.", Name);
        }

        /// <inheritdoc />
        protected override bool IsAzureStorageConnectionValid()
        {
            return true;
        }
    }
}
