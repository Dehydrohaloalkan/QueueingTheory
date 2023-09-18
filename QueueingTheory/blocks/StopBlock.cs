namespace QueueingTheory.blocks;

public class StopBlock : ITickable
{
    public ITickable NextBlock { get; set; }
    public int WorkTicks { get; set; }
    private Request? _request;
    private State _state;
    private readonly bool _isWithTimer;
    private readonly int _timerMax;
    private int _timer;
    private readonly double _probability;
    private readonly Random _random;
    public int BlockTime = 0;
    
    public StopBlock(int timerMax)
    {
        _isWithTimer = true;
        _timerMax = timerMax;
        _timer = 0;
        _state = State.Open;
    }
    
    public StopBlock(double probability, Random random)
    {
        _isWithTimer = false;
        _probability = probability;
        _random = random;
        _state = State.Open;
    }
    

    public void NextTick()
    {
        switch (_state)
        {
            case State.Close:
            {
                _request.TimeInSystem++;
                WorkTicks++;
                
                if (_isWithTimer)
                {
                    _timer++;
                    if (_timer >= _timerMax) _trySend();
                }
                else
                {
                    var prob = _random.NextDouble();
                    if (prob > _probability) _trySend();
                }
                break;      
            }
            case State.Stop:
            {
                _request.TimeInSystem++;
                WorkTicks++;
                BlockTime++;
                
                _trySend();  
                break;
            }
        }
    }

    private void _trySend()
    {
        if (NextBlock.CanAccept())
        {
            NextBlock.Accept(_request);
            _state = State.Open;
        }
        else
        {
            _state = State.Stop;
        }
    }
    
    public void Accept(Request req)
    {
        _request = req;
        _state = State.Close;
        if (_isWithTimer) _timer = 0;
    }

    public bool CanAccept()
    {
        return _state == State.Open;
    }

    public int GetRequestCount() => _state != State.Open ? 1 : 0;
}

public enum State
{
    Open,
    Close,
    Stop
}