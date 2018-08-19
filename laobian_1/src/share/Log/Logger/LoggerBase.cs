using System;
using Laobian.Share.Model.Log;
using Microsoft.Extensions.Logging;

namespace Laobian.Share.Log.Logger
{
    public abstract class LoggerBase<TLogItem> : ILogger 
        where TLogItem : LogItem
    {
        protected LoggerBase(string category, Action<TLogItem> addAction)
        {
            CategoryName = category;
            AddAction = addAction;
        }

        protected string CategoryName { get; set; }

        protected Action<TLogItem> AddAction { get; }

        public abstract void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter);

        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public virtual IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
