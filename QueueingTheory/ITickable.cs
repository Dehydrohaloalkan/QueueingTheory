namespace QueueingTheory;

public interface ITickable
{
    // public ITickable NextBlock { get; set; }
    // public int WorkTicks { get; set; }
    public void NextTick(Func<Request, bool> tryToSendRequest);
    public void Accept(Request req);
    public bool CanAccept { get; }
    public bool HandlingRequest { get; }
    // public int GetRequestCount();
}