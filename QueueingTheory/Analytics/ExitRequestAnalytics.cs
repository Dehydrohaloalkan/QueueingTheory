using QueueingTheory.blocks;

namespace QueueingTheory.Analytics;

public class ExitRequestAnalytics : Analytics
{
    public int ExitedRequestCount { get; private set; }

    public override void AnalyzeAfterTick(ITickable tickable, ITickable? nextTickable, Request? request, int tickNumber)
    {
        if (tickable is EndBlock && request is not null)
        {
            ExitedRequestCount++;
        }
    }
}