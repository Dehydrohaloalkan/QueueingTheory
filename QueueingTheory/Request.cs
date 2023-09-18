namespace QueueingTheory;

public class Request
{
    public int id;

    public Request()
    {
        id = new Random().Next();
    }
}