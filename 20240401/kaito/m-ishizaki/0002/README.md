# お題

- C# で書いてください。
- CSV を読み取って、引数で指示された加工をして CSV を出力してください。
- 入出力はファイルでもコンソールでも OK です。

# 回答の解説
いつもの一行でシュッと書いてしまうコードです。
Sample と同じロジックですが、Sample では対応していなかったデータの中に ```,``` や ```"```、改行などに対応できています。    
CSV の有名ライブラリが、どれも一行で書けるかんじではなかったので、自分でライブラリを作ってそれを使っています。そこまでして一行で書きたいかといわれると書きたいです。

# 実行結果
一列目列でソート
```ps1
.\Kaito.exe 1,`'2,a`'`n\`"3`nb\`",6`n5,4 0
1,"2,a"
"3
b",6
5,4
```

# C# コードの解説
素直な教科書通りの変哲のないコードだと思います。特に解説はいらないかな。