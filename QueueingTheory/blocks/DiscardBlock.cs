using System.Diagnostics;

namespace QueueingTheory.blocks;

public class DiscardBlock : ITickable
{
    public ITickable NextBlock { get; set; }
    public int WorkTicks { get; set; }
    private Request? _request;
    private readonly bool _isWithTimer;
    private readonly int _timerMax;
    private int _timer;
    private readonly double _probability;
    private readonly Random _random;

    public DiscardBlock(int timerMax)
    {
        _isWithTimer = true;
        _timerMax = timerMax;
        _timer = 0;
        _request = null;
    }
    
    public DiscardBlock(double probability, Random random)
    {
        _isWithTimer = false;
        _probability = probability;
        _random = random;
        _request = null;
    }
    
    public void NextTick()
    {
        if (_request == null) return;
        
        _request.TimeInSystem++;
        WorkTicks++;
        
        if (_isWithTimer)
        {
            if (++_timer >= _timerMax) _trySend();
        }
        else
        {
            if (_random.NextDouble() > _probability) _trySend();
        }
    }
    
    private void _trySend()
    {
        if (NextBlock.CanAccept()) NextBlock.Accept(_request);
        _request = null;
    }

    public void Accept(Request req)
    {
        _request = req;
        if (_isWithTimer) _timer = 0;
    }

    public bool CanAccept()
    {
        return _request == null;
    }

    public int GetRequestCount() => _request != null ? 1 : 0;
}

