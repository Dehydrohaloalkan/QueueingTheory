using QueueingTheory.blocks;

namespace QueueingTheory.Analytics;

public class EnterRequestAnalytics : Analytics
{
    public int EnteredRequestCount { get; private set; }

    public override void AnalyzeAfterTick(ITickable tickable, ITickable? nextTickable, Request? request, int tickNumber)
    {
        if (tickable is StartBlock && request is not null)
        {
            EnteredRequestCount++;
        }
    }
}