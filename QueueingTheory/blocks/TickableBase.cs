namespace QueueingTheory.blocks;

public abstract class TickableBase : ITickable
{
    public virtual void NextTick(Func<Request, bool> tryToSendRequest)
    {
    }

    public virtual void Accept(Request req)
    {
    }

    public virtual bool CanAccept => true;

    public virtual bool HandlingRequest => false;
}