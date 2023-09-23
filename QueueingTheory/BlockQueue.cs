using QueueingTheory.blocks;

namespace QueueingTheory;

public class BlockQueue
{
    private readonly List<ITickable> _blocks = new();
    private List<ITickable> _reverseBlocks;
    private int _tickCount;
    private readonly List<int> _snapshots = new();

    public BlockQueue() => Add(new StartBlock());

    public void Compile()
    {
        Add(new EndBlock());
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
            foreach (var block in _reverseBlocks)
            {
                block.NextTick();
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
        
        var accumulatorSnapshots = new List<List<int>>();
        foreach (var block in _blocks)
        {
            if (block is Accumulator accumulator)
            {
                accumulatorSnapshots.Add(accumulator.Snapshots);
            }
        }

        double L1 = 0; 
        if (accumulatorSnapshots.Count > 0) L1 = accumulatorSnapshots.ToSumList().Average();

        var L2 = _snapshots.Average();

        double W1 = 0;
        double W2 = 0;
        
        if ((_blocks.Last() as EndBlock)!.QueueSnapshots.Count > 0)
        {
            W1 = (_blocks.Last() as EndBlock)!.QueueSnapshots.Average();    
        }

        if ((_blocks.Last() as EndBlock)!.SystemSnapshots.Count > 0)
        {
            W2 = (_blocks.Last() as EndBlock)!.SystemSnapshots.Average();    
        }
        
        var KList = _blocks
            .Select((block) => block.WorkTicks / (double)_tickCount)
            .Where(x => x >= 0)
            .Skip(1)
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