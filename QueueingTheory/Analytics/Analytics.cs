namespace QueueingTheory.Analytics;

public abstract class Analytics : IAnalytics
{
    public virtual void AnalyzeBeforeTick(
        ITickable tickable,
        int tickNumber)
    {
    }

    public virtual void AnalyzeAfterTick(
        ITickable tickable,
        ITickable? nextTickable,
        Request? request,
        int tickNumber)
    {
    }
}