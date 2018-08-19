using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Laobian.Share.Data.Repository;
using Laobian.Share.Log.Logger;
using Laobian.Share.Model.Log;
using Microsoft.Extensions.Logging;

namespace Laobian.Share.Log.Provider
{
    public class DbLoggerProvider<TLogItem, TLogger> : ILoggerProvider
        where TLogItem : LogItem
        where TLogger: LoggerBase<TLogItem>
    {
        private readonly ILogRepository<TLogItem> _logRepository;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly List<TLogItem> _currentBatch;
        private readonly BlockingCollection<TLogItem> _messageQueue;

        public DbLoggerProvider(ILogRepository<TLogItem> logRepository)
        {
            _logRepository = logRepository;
            _currentBatch = new List<TLogItem>();
            _messageQueue = new BlockingCollection<TLogItem>(new ConcurrentQueue<TLogItem>());
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(ProcessLogAsync, null, TaskCreationOptions.LongRunning);
        }

        public ILogger CreateLogger(string categoryName)
        {
            var logger = (ILogger)Activator.CreateInstance(typeof(TLogger), categoryName, new Action<TLogItem>(AddLog));
            return logger;
        }

        private  async Task WriteLogsAysnc()
        {
            if (_currentBatch.Any())
            {
                await _logRepository.AddAsync(_currentBatch);
            }
        }

        private void AddLog(TLogItem log)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                _messageQueue.Add(log);
            }
        }

        private async Task ProcessLogAsync(object state)
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                while (_messageQueue.TryTake(out var log))
                {
                    _currentBatch.Add(log);
                }

                await WriteLogsAysnc();
                _currentBatch.Clear();

                await Task.Delay(TimeSpan.FromMilliseconds(1000 * 10));
            }
        }

        public void Dispose()
        {
        }
    }
}
