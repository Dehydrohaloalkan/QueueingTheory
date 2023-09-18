namespace QueueingTheory.blocks;

public class EndBlock : TickableBase
{
    public int WorkTicks { get; set; } = -1;
    public readonly List<int> SystemSnapshots = new();
    public readonly List<int> QueueSnapshots = new();
    
    public void Accept(Request req)
    {
        SystemSnapshots.Add(req.TimeInSystem);
        QueueSnapshots.Add(req.TimeIQueue);
        RequestCount++;
    }
}