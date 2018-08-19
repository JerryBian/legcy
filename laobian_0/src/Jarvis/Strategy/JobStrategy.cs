using System;

namespace Jarvis.Strategy
{
    public abstract class JobStrategy
    {
        public DateTime LastRunAt { get; set; }

        public virtual bool RunOnlyOnce => false;

        public abstract bool RunNow();
    }
}