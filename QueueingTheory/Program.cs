using QueueingTheory;

var blockQueue = new BlockQueue();

blockQueue.Simulate(50000);

var values = blockQueue.GetValues();


Console.WriteLine($"A : {values.A}");
Console.WriteLine($"Q : {values.Q}");
Console.WriteLine($"P отк : {values.P1}");
Console.WriteLine($"P бл : {values.P2}");
Console.WriteLine($"L оч : {values.L1}");
Console.WriteLine($"L с : {values.L2}");
Console.WriteLine($"W оч : {values.W1}");
Console.WriteLine($"W с : {values.W2}");
Console.WriteLine(values.K
    .Select(k => k.ToString("0.00000"))
    .Aggregate("K кан : ", (first, next) => $"{first}, {next}"));