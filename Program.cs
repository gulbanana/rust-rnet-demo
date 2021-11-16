using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).RunAll();
