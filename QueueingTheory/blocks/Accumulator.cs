namespace QueueingTheory.blocks;

public class Accumulator : ITickable
{
    public ITickable NextBlock { get; set; }
    public int WorkTicks { get; set; } = -1;
    private readonly Queue<Request> _queue;
    private readonly int _count;
    public readonly List<int> Snapshots;

    public Accumulator(int count)
    {
        _count = count;
        _queue = new Queue<Request>();
        
        Snapshots = new List<int>();
    }

    public void NextTick()
    {
        Snapshots.Add(_queue.Count);
        foreach (var request in _queue)
        {
            request.TimeIQueue++;
            request.TimeInSystem++;
        }
        
        if (_queue.Count == 0 || !NextBlock.CanAccept()) return;
        var req = _queue.Dequeue();
        NextBlock.Accept(req);
    }

    public void Accept(Request req)
    {
        if (NextBlock.CanAccept())
            NextBlock.Accept(req);
        else
            _queue.Enqueue(req);
    }

    public bool CanAccept() => _queue.Count < _count;
    
    public int GetRequestCount() => _queue.Count;
}