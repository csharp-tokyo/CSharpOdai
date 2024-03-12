using System;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using ToKanjiLib;

BenchmarkRunner.Run<ToKanjiBenchmark>();

[MemoryDiagnoser]
public class ToKanjiBenchmark
{
    [GlobalSetup]
    public static void GlobalSetup()
    {
        Console.WriteLine("GlobalSetup");
    }

    [Benchmark]
    public int ToKanji()
    {
        Span<char> buffer = stackalloc char[50];

        var filledChars = Converter.ToKanji(1234_5678_9012_3456, buffer);

        return filledChars;
    }
}
