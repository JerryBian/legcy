using Laobian.Infrastuture.Interface.Service;
using Laobian.Service.Lib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laobian.Service.Tasks
{
    public abstract class TaskBase<T> : ITaskBase<T>
    {
        private bool _started;
        private readonly ConcurrentQueue<T> _queueStore;

        protected TaskBase()
        {
            _started = false;
            _queueStore = new ConcurrentQueue<T>();
            DoWork();
        }

        public virtual void Add(T element)
        {
            _queueStore.Enqueue(element);
        }

        public virtual bool TryDequeue(out T element)
        {
            return _queueStore.TryDequeue(out element);
        }

        public virtual bool TryDequeueAll(out List<T> elements)
        {
            elements = new List<T>();
            T element;
            while (TryDequeue(out element))
            {
                elements.Add(element);
            }

            return elements.Any();
        }

        public virtual void DoWork()
        {
        }

        protected void DoWork(Func<bool> func)
        {
            if (_started)
            {
                return;
            }

            _started = true;
            var foreverRetry = new ForeverRetry(
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromHours(0.5));
            Task.Factory.StartNew(() => foreverRetry.Execute(func), TaskCreationOptions.LongRunning);
        }

        protected async Task DoWorkAsync(Func<Task<bool>> func)
        {
            if (_started)
            {
                return;
            }

            _started = true;
            var foreverRetry = new ForeverRetry(
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromHours(0.5));
            await Task.Factory.StartNew(async () => await foreverRetry.ExecuteAsync(func), TaskCreationOptions.LongRunning);
        }
    }
}
