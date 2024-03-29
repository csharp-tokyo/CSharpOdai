# お題

- C# で書いてください。
- 一人用ポーカーを作ってください。

## ポーカーのルール
- ジョーカーを除くトランプ 52 枚からランダムに 5 枚のカードが配られる
- その中で成立している最も強い役を表示する（成立していなければブタ）
- 役のリストは次を参考にしてください

1. ロイヤルストレートフラッシュ
1. ストレートフラッシュ
1. フォーカード
1. フルハウス
1. フラッシュ
1. ストレート
1. スリーカード
1. ツーペア
1. ワンペア
1. ブタ

# 回答の解説
いつもの簡単にシュッと手早く 1 行で書いてしまう回答です。  
今回は、出力を UTF8 にするために、1 行でなく 2 行のコードになっています。スートに記号を使いたくて。

# 実行結果
```ps1
♢ 3, ♡ 4, ♢ 4, ♤ 5, ♧ 12
ワンペア
```

# C# コードの解説
少しだけテクニックを使ったのでそこだけ補足しておきます。

### 52 毎のデッキを作るところ
4 つのスート × 1～13 の数 の組で 52 のカードを作るところですが、**Enumerable.Join** ですべての要素がマッチするように書いて 4 * 13 の結果が作られるようにしてみました。  

### デッキをランダムに並び替えるところ
```.OrderBy(x => Guid.NewGuid())``` としてランダムな値で並び替えることでシャッフルを実現してみました。
