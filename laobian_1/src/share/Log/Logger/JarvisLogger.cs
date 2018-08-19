using System;
using Laobian.Share.Model.Log;
using Microsoft.Extensions.Logging;

namespace Laobian.Share.Log.Logger
{
    public class JarvisLogger : LoggerBase<JarvisLogItem>
    {
        public JarvisLogger(string category, Action<JarvisLogItem> addAction) : base(category, addAction)
        {
        }

        public override void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logItem = new JarvisLogItem
            {
                Category = CategoryName,
                Exception = exception,
                Message = formatter(state, exception),
                Level = logLevel.ToString()
            };

            AddAction(logItem);
        }
    }
}
