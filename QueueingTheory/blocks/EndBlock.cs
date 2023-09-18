namespace QueueingTheory.blocks;

public class EndBlock : ITickable
{
    public ITickable NextBlock { get; set; }
    public int WorkTicks { get; set; } = -1;
    public int RequestCount;
    public readonly List<int> SystemSnapshots;
    public readonly List<int> QueueSnapshots;
    
    public EndBlock()
    {
        SystemSnapshots = new List<int>();
        QueueSnapshots = new List<int>();
    }


    public void NextTick() {}

    public void Accept(Request req)
    {
        SystemSnapshots.Add(req.TimeInSystem);
        QueueSnapshots.Add(req.TimeIQueue);
        RequestCount++;
    }

    public bool CanAccept()
    {
        return true;
    }

    public int GetRequestCount() => 0;
}