# お題の実装

ExpressionTree、ILEmit、Roslyn はいずれも、DateTime.ParseExact / TryFormat を大袈裟にやっているだけなので、パフォーマンスでは大きな差は出ない。

クソコードだが、敢えてクソく書くチャレンジをすることで見えてくるものもあったりなかったりする。

CollectionExpression や NoAllocation は文字列操作で結果を組み立てており、DateTime のメソッドを使っていないので、速いのは当たり前。

# ベンチマーク
```
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.2861/23H2/2023Update/SunValley3)
Unknown processor
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
```
| Method                                 | Iteration | Mean        | Error     | StdDev    | Gen0   | Allocated |
|--------------------------------------- |---------- |------------:|----------:|----------:|-------:|----------:|
| DateTimeParseExactAndToStringBenchmark | 10        | 1,826.88 ns | 11.324 ns |  9.456 ns |      - |         - |
| DateOnlyParseExactAndToStringBenchmark | 10        | 2,027.01 ns |  2.800 ns |  2.338 ns |      - |         - |
| ExpressionTreeBenchmark                | 10        | 1,835.83 ns | 10.721 ns | 10.029 ns |      - |         - |
| ILEmitBenchmark                        | 10        | 1,854.80 ns |  8.370 ns |  7.420 ns |      - |         - |
| RoslynBenchmark                        | 10        | 1,894.37 ns | 36.743 ns | 46.468 ns |      - |         - |
| CollectionExpressionBenchmark          | 10        |   591.12 ns | 10.737 ns |  9.518 ns | 0.1984 |    2080 B |
| NoAllocationBenchmark                  | 10        |    10.19 ns |  0.014 ns |  0.011 ns |      - |         - |
