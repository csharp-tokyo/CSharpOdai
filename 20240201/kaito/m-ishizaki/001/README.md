# C# Tokyo コミュニティ企画のお題へ参加したコード

C# Tokyo コミュニティ企画のお題への回答コードです。

# 今回のお題

- C# で書いてください。
- 引数で数値を受け取って、漢数字で表現したもの出力してください。

# この回答
```cs
Console.WriteLine(args.FirstOrDefault()?.AggregateBy(x => x, new System.Text.StringBuilder(), (b, x) => { b.Append("〇一二三四五六七八九×"[int.TryParse(x.ToString(), out var i) ? i : 10]); return b; }).Select(x => x.Value).FirstOrDefault()?.ToString());
```

## 実行例
```pa1
> /\kaito.exe 123
一二三
```
```pa1
> /\kaito.exe 123jkl45
一二三×××四五
```

# 解説
.NET 9 Preview 1 で書きました。  
.NET 9 Preview 1 で追加された LINQ メソッドの ```.AggregateBy``` を間違った使い方で書いています。本来はこういうものではないです。やるにしても既存の ```.Aggregate``` でしょう。  

ただ、.NET 9 Preview 1 と言いたかっただけのコードです。

# 工夫した点
```int.TryParse``` が ```false``` の場合に、```'×'``` が出るように、漢数字の後ろに ```'×'``` を追加した点。  
**string** に添え字アクセスすると ***char*** が取れることを使った点。  

# 最後に
皆様、いいコード書いてください。
