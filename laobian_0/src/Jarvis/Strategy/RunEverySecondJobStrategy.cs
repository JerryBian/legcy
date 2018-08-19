using System;

namespace Jarvis.Strategy
{
    public class RunEverySecondJobStrategy : JobStrategy
    {
        private readonly int _second;

        public RunEverySecondJobStrategy(int second)
        {
            _second = second;
        }

        public override bool RunNow()
        {
            return (DateTime.UtcNow - LastRunAt).TotalSeconds >= _second;
        }
    }
}