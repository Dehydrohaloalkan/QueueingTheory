namespace QueueingTheory;

public interface ITickable
{
    public ITickable NextBlock { get; set; }
    public int WorkTicks { get; set; }
    public void NextTick();
    public void Accept(Request req);
    public bool CanAccept();
    public int GetRequestCount();
}