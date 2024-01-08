# お題の実装

ExpressionTree、ILEmit、Roslyn はいずれも、DateTime.ParseExact / TryFormat を大袈裟にやっているだけなので、パフォーマンスでは大きな差は出ない。

クソコードだが、敢えてクソく書くチャレンジをすることで見えてくるものもあったりなかったりする。

CollectionExpression や NoAllocation は文字列操作で結果を組み立てており、DateTime のメソッドを使っていないので、速いのは当たり前。