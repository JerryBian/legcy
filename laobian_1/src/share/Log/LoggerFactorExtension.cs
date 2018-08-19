using Laobian.Share.Log.Logger;
using Laobian.Share.Log.Provider;
using Laobian.Share.Model.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Laobian.Share.Log
{
    public static class LoggerFactorExtension
    {
        public static ILoggingBuilder AddDb<TLogItem, TLogger>(this ILoggingBuilder builder)
            where TLogItem : LogItem
            where TLogger : LoggerBase<TLogItem>
        {
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider<TLogItem, TLogger>>();
            return builder;
        }
    }
}