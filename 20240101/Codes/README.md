# お題の実装

ExpressionTree、ILEmit、Roslyn はいずれも、DateTime.ParseExact / TryFormat を大袈裟にやっているだけなので、パフォーマンスでは大きな差は出ない。

クソコードだが、敢えてクソく書くチャレンジをすることで見えてくるものもあったりなかったりする。

CollectionExpression や NoAllocation は文字列操作で結果を組み立てており、DateTime のメソッドを使っていないので、速いのは当たり前。書式チェックをしていないので、変な入力が与えられた場合は、相応に変な出力が出る。まぁそのへんの仕様はレギュレーションにないからいいよね。

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
| DateTimeParseExactAndToStringBenchmark | 10        | 1,824.21 ns | 17.814 ns | 16.663 ns |      - |         - |
| DateOnlyParseExactAndToStringBenchmark | 10        | 2,035.98 ns | 21.337 ns | 17.818 ns |      - |         - |
| ExpressionTreeBenchmark                | 10        | 2,063.49 ns | 10.213 ns |  8.528 ns | 0.0610 |     640 B |
| ILEmitBenchmark                        | 10        | 2,052.24 ns | 15.267 ns | 13.534 ns | 0.0610 |     640 B |
| RoslynBenchmark                        | 10        | 2,059.22 ns |  7.639 ns |  6.379 ns | 0.0610 |     640 B |
| CollectionExpressionBenchmark          | 10        |   622.06 ns |  8.712 ns |  7.723 ns | 0.1984 |    2080 B |
| NoAllocationBenchmark                  | 10        |    10.28 ns |  0.036 ns |  0.028 ns |      - |         - |
