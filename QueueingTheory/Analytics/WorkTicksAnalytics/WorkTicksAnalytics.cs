using QueueingTheory.blocks;

namespace QueueingTheory.Analytics.WorkTicksAnalytics;

public class WorkTicksAnalytics : Analytics
{
    private readonly Dictionary<ITickable, int> _tickableWorkTicks = new();
    
    public override void AnalyzeBeforeTick(ITickable tickable, int tickNumber)
    {
        if (tickable is DiscardBlock discardBlock)
        {
            
        }
    }
}