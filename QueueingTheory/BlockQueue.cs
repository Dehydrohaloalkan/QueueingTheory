using QueueingTheory.Analytics;
using QueueingTheory.blocks;

namespace QueueingTheory;

public class BlockQueue
{
    private readonly List<ITickable> _blocks;
    private readonly List<ITickable> _reverseBlocks;
    private int _tickCount;
    private readonly List<int> _snapshots;

    private readonly List<IAnalytics> _analytics = new()
    {
        new EnterRequestAnalytics(),
        new ExitRequestAnalytics()
    };
    
    public BlockQueue()
    {
        var random = new Random();
        _blocks = new List<ITickable>();
        _snapshots = new List<int>();
        
        Add(new StartBlock());
        Add(new StopBlock(0.75, random));
        Add(new DiscardBlock(0.7, random));
        Add(new Accumulator(2));
        Add(new DiscardBlock(0.65, random));
        Add(new EndBlock());

        foreach (var block in _blocks)
        {
            if (block is DiscardBlock discardBlock) discardBlock.LastBlock = _blocks.Last();
        }
        
        _reverseBlocks = Enumerable.Reverse(_blocks).ToList();
    }

    public void Add(ITickable block)
    {
        if (_blocks.Count != 0) _blocks.Last().NextBlock = block; 
        _blocks.Add(block);
    }

    public void Simulate(int requestCount)
    {
        _tickCount = requestCount;
        for (var i = 0; i < requestCount; i++)
        {
            for (var blockIndex = 0; blockIndex < _reverseBlocks.Count; blockIndex++)
            {
                var block = _reverseBlocks[blockIndex];
                var nextBlock = blockIndex + 1 < _reverseBlocks.Count ? _reverseBlocks[blockIndex++] : null;
                
                var requestIsSent = false;
                
                _analytics.ForEach(a => a.AnalyzeBeforeTick(block, i));

                block.NextTick(request =>
                {
                    if (nextBlock is not null && nextBlock.CanAccept)
                    {
                        nextBlock.Accept(request);
                        return requestIsSent = true;
                    }

                    return requestIsSent = false;
                });

                _analytics.ForEach(a => a.AnalyzeAfterTick(
                    block,
                    nextBlock,
                    requestIsSent,
                    i));
            }

            _snapshots.Add(_blocks
                .Select(block => block.GetRequestCount())
                .Aggregate((sum, count) => sum + count));
        }
    }

    public Values GetValues()
    {
        var A = (_blocks.Last() as EndBlock)!.RequestCount / (double)_tickCount;
        var Q = (_blocks.Last() as EndBlock)!.RequestCount / (double)(_blocks.First() as StartBlock)!.RequestCount;
        var P1 = 1 - Q;
        
        double P2 = 0; 
        foreach (var block in _blocks)
        {
            if (block is StopBlock stopBlock) P2 += stopBlock.BlockTime;
        }
        P2 /= _tickCount;
        
        var List = new List<int>();
        foreach (var block in _blocks)
        {
            if (block is Accumulator accumulator) List.AddRange(accumulator.Snapshots);
        }
        var L1 = List.Average();
        var L2 = _snapshots.Average();

        var W1 = (_blocks.Last() as EndBlock)!.QueueSnapshots.Average();
        var W2 = (_blocks.Last() as EndBlock)!.SystemSnapshots.Average();

        var KList = _blocks
            .Select((block) => block.WorkTicks / (double)_tickCount)
            .Where(x => x >= 0)
            .ToList();
        
        return new Values
        {
            A = A,
            Q = Q,
            P1 = P1,
            P2 = P2,
            L1 = L1,
            L2 = L2,
            W1 = W1,
            W2 = W2,
            K = KList,
        };
    }
}

public struct Values
{
    public double A;
    public double Q;
    public double P1;
    public double P2;
    public double L1;
    public double L2;
    public double W1;
    public double W2;
    public List<double> K;
}