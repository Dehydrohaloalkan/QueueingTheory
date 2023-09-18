using QueueingTheory.blocks;

namespace QueueingTheory.Analytics;

public class RequestLifetimeAnalytics : Analytics
{
    private readonly Dictionary<Request, int> _aliveRequestLifetimes = new();
    private readonly Dictionary<Request, int> _requestLifetimes = new();

    public IDictionary<Request, int> RequestLifetimes => _requestLifetimes;

    public override void AnalyzeAfterTick(ITickable tickable, ITickable? nextTickable, Request? request, int tickNumber)
    {
        if (request is null)
        {
            return;
        }
        
        if (tickable is StartBlock)
        {
            _aliveRequestLifetimes[request] = tickNumber;
        }
        else if (nextTickable is EndBlock)
        {
            _requestLifetimes[request] = tickNumber - _aliveRequestLifetimes[request];
        }
    }
}