using BenchmarkDotNet.Attributes;
using static RustDotnetBindgenDemo.RustDotnetBindgenDemo;

public class Benchmark
{
    private int[] numbers;

    [Params(1_000, 10_000, 100_000)] public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var halfMax = int.MaxValue / 2;
        numbers = Enumerable.Range(0, Count).Select(_ => Random.Shared.Next() - halfMax).ToArray();
        SetNumbers(numbers);
    }

    [Benchmark(Baseline = true)]
    public int ManagedLoop()
    {
        var accum = 0;
        foreach (var number in numbers)
        {
            accum += number;
        }
        return accum;
    }

    [Benchmark]
    public int ManagedFold()
    {
        return numbers.Aggregate(0, (x, y) => x + y);
    }

    [Benchmark]
    public int UnmanagedLoop()
    {
        return AddLoop();
    }

    [Benchmark]
    public int UnmanagedFold()
    {
        return AddFold();
    }
}
