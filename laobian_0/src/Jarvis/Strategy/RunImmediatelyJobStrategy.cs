namespace Jarvis.Strategy
{
    public class RunImmediatelyJobStrategy : JobStrategy
    {
        public override bool RunOnlyOnce => true;

        public override bool RunNow()
        {
            return true;
        }
    }
}