using static RustDotnetBindgenDemo.RustDotnetBindgenDemo;

SayHello("world");

var halfMax = int.MaxValue / 2;
var numbers = Enumerable.Range(0, 1000).Select(_ => Random.Shared.Next() - halfMax).ToArray();

Console.WriteLine("C#: {0}", numbers.Aggregate(0, (x, y) => x + y));
Console.WriteLine("Rust: {0}", AddNumbers(numbers));

Console.ReadKey();