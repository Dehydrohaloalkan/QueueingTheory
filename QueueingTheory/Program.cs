using System.Globalization;
using QueueingTheory;
using QueueingTheory.blocks;

var blockQueue = new BlockQueue();
var random = new Random();

blockQueue.Add(new StopBlock(0.75, random));
blockQueue.Add(new DiscardBlock(0.7, random));
blockQueue.Add(new Accumulator(2));
blockQueue.Add(new DiscardBlock(0.65, random));

// blockQueue.Add(new StopBlock(0.7, random));
// blockQueue.Add(new Accumulator(1));
// blockQueue.Add(new DiscardBlock(0.65, random));
// blockQueue.Add(new DiscardBlock(0.5, random));

// blockQueue.Add(new StopBlock(2));
// blockQueue.Add(new DiscardBlock(0.4, random));
// blockQueue.Add(new Accumulator(1));
// blockQueue.Add(new DiscardBlock(0.4, random));

blockQueue.Compile();
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
Console.WriteLine($"K : {string.Join(", ", values.K.Select(x => x.ToString(CultureInfo.CurrentCulture)))}");
    