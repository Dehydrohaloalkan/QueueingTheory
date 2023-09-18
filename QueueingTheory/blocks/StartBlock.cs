namespace QueueingTheory.blocks;

public class StartBlock : ITickable
{
    public ITickable NextBlock { get; set; }
    public int WorkTicks { get; set; } = -1;
    public int RequestCount;

    public void NextTick()
    {
        if (!NextBlock.CanAccept()) return;
        NextBlock.Accept(new Request());
        RequestCount++;
    }

    public void Accept(Request req)
    {
        return;
    }

    public bool CanAccept()
    {
        return false;
    }

    public int GetRequestCount() => 0;
}