using System;
using System.Threading;
using System.Threading.Tasks;

namespace Laobian.Service.Lib
{
    public class ForeverRetry
    {
        private readonly TimeSpan _initRetryWait;
        private readonly TimeSpan _retryInterval;
        private readonly TimeSpan _maxRetryWait;


        public ForeverRetry(
            TimeSpan initRetryWait,
            TimeSpan retryInterval,
            TimeSpan maxRetryWait)
        {
            _initRetryWait = initRetryWait;
            _retryInterval = retryInterval;
            _maxRetryWait = maxRetryWait;
        }

        public void Execute(Func<bool> func)
        {
            var currentRetryWait = _initRetryWait;
            while (true)
            {
                var result = false;
                try
                {
                    result = func();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                if (!result)
                {
                    Thread.Sleep(currentRetryWait);
                    currentRetryWait += _retryInterval;

                    if (currentRetryWait > _maxRetryWait)
                    {
                        currentRetryWait = _initRetryWait;
                    }
                }
                else
                {
                    currentRetryWait = _initRetryWait;
                }
            }
        }

        public async Task ExecuteAsync(Func<bool> func)
        {
            var currentRetryWait = _initRetryWait;
            while (true)
            {
                var result = false;
                try
                {
                    result = func();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                if (!result)
                {
                    await Task.Delay(currentRetryWait);
                    currentRetryWait += _retryInterval;

                    if (currentRetryWait > _maxRetryWait)
                    {
                        currentRetryWait = _initRetryWait;
                    }
                }
                else
                {
                    currentRetryWait = _initRetryWait;
                }
            }
        }

        public async Task ExecuteAsync(Func<Task<bool>> func)
        {
            var currentRetryWait = _initRetryWait;
            while (true)
            {
                var result = false;
                try
                {
                    result = await func();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

                if (!result)
                {
                    await Task.Delay(currentRetryWait);
                    currentRetryWait += _retryInterval;

                    if (currentRetryWait > _maxRetryWait)
                    {
                        currentRetryWait = _initRetryWait;
                    }
                }
                else
                {
                    currentRetryWait = _initRetryWait;
                }
            }
        }
    }
}
