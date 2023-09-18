namespace QueueingTheory;

public class Request
{
    public int id;
    public int TimeIQueue = 0;
    public int TimeInSystem = 0;

    public Request()
    {
        id = new Random().Next();
    }
}