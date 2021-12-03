using BenchmarkDotNet.Attributes;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static RustDotnetBindgenDemo.RustDotnetBindgenDemo;

public class Benchmark
{
    private int[] numbers;
    private IOpaqueHandle state;

    [Params(1_000, 10_000, 100_000)] public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var halfMax = int.MaxValue / 2;
        numbers = Enumerable.Range(0, Count).Select(_ => Random.Shared.Next() - halfMax).ToArray();
        state = InitState(numbers);
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
    public unsafe int ManagedSIMD()
    {
        if (!Avx2.IsSupported)
        {
            return ManagedLoop();
        }

        var result = new int[8];
        fixed (int* input = numbers, output = result)
        {
            var accum = new Vector256<int>();
            for (var i = 0; i < Count; i += 8)
            {
                var block = Avx2.LoadVector256(input+i); // _mm256_loadu_si256
                accum = Avx2.Add(accum, block); // _mm256_add_epi32
            }
            Avx2.Store(output, accum);
        }

        var finalResult = 0;
        for (var i = 0; i < 8; i++)
        {
            finalResult = finalResult + result[i];
        }
        return finalResult;
    }

    [Benchmark]
    public int UnmanagedLoop()
    {
        return AddLoop(state);
    }

    [Benchmark]
    public int UnmanagedFold()
    {
        return AddFold(state);
    }
}
