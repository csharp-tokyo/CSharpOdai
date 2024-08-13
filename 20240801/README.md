新企画！ お題のコードを書いてみよう！ 第 7 回

C# Tokyo コミュニティの新企画です。管理メンバーがプログラムのお題を出しますので皆さんはそのお題のコードを書いてください。  
コードは GitHub で書いてプルリクエストと Slack でのメッセージをください。  
※企画で書かれたコードが記録として残る/後で見る方がみられるように、C# Tokyo のリポジトリに PR をもらうことにしました。  

何かあれば、ご連絡は Slack でお願いします。  

#### コードを書いていただく場所
このリポジトリ内の次の場所を回答の場所としてください。  
```/{yyyyMMXX}/kaito/{name}```  
```{yyyyMMXX｝``` 部分は毎回のお題文のあるディレクトリです。```{name}``` 部分は被らないよう各人の GitHub のユーザー名にしてください。  

# レギュレーション
ただ、お題をそのまま実現してもよいですし、高度なテクニックを駆使しても、せっかくだから普段使わない技術を使ってみたりなんでもありです。  
とにかくお題を実現してください。  
ただし、管理メンバーが全く分からないものや、実行に安全が確信できないコードは対象外となる可能性があります。  

# 今回のお題

- C# で書いてください。
- MM-dd-yyyy を引数として受け取って yyyy/MM/dd を出力するコンソールアプリを作ってください。
- [C# 12 の新機能](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-12) または [C# 13 の新機能](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-13) を使ってください。

# 例
この例では [C# 12 の新機能](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-12) である [コレクション式](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-12#collection-expressions) を使用しています。  
```cs
string[] d = args[0].Split('-');
string r = string.Join("/", (string[])[d[2], d[0], d[1]]);
Console.WriteLine(r);
```

## 実行例
PowerSell
```ps1
.\Sample.exe 08-13-2024
2024/08/13
```

[サンプルプロジェクト](./Sample)  

# 発表方法
いただいたコードは C# Tokyo イベントの配信中に発表することがあります。  
皆様の素敵なコードをお待ちしています。  

# 今回の締め切り
今回のお題の締め切りは 2024/09/30 24:00:00 とさせてください。  
