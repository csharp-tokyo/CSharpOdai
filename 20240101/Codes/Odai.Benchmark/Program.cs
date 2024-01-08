using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Odai;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
public class Benchmark
{
    [Params(10)]
    public int Iteration { get; set; }

    [Benchmark]
    public int DateTimeParseExactAndToStringBenchmark()
    {
        return this.Invoke(DateTimeParseExactAndToString.Instance);
    }

    [Benchmark]
    public int DateOnlyParseExactAndToStringBenchmark()
    {
        return this.Invoke(DateOnlyParseExactAndToString.Instance);
    }
    

    [Benchmark]
    public int ExpressionTreeBenchmark()
    {
        return this.Invoke(ExpressionTree.Instance);
    }

    [Benchmark]
    public int ILEmitBenchmark()
    {
        return this.Invoke(ILEmit.Instance);
    }

    [Benchmark]
    public int RoslynBenchmark()
    {
        return this.Invoke(Odai.Roslyn.Instance);
    }

    [Benchmark]
    public int CollectionExpressionBenchmark()
    {
        return this.Invoke(CollectionExpression.Instance);
    }

    [Benchmark]
    public int NoAllocationBenchmark()
    {
        return this.Invoke(NoAllocation.Instance);
    }

    private int Invoke<T>(T implementation)
        where T : IOdai
    {
        var iteration = this.Iteration;
        var result = 0;

        Span<char> output = stackalloc char[10];

        for (var i = 0; i < iteration; ++i)
        {
            implementation.Invoke("01-02-2024", output);
            result += output[0];
        }

        return result;
    }
}
