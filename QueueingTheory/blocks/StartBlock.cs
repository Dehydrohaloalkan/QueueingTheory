namespace QueueingTheory.blocks;

public class StartBlock : TickableBase
{
    // public int WorkTicks { get; set; } = -1;

    public override void NextTick(Func<Request, bool> tryToSendRequest)
    {
        tryToSendRequest(new Request());
    }
}