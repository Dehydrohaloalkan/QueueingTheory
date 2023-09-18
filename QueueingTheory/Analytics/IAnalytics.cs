namespace QueueingTheory.Analytics;

public interface IAnalytics
{
    void AnalyzeBeforeTick(ITickable tickable, int tickNumber);
    
    void AnalyzeAfterTick(ITickable tickable, ITickable? nextTickable, Request? request, int tickNumber);
}