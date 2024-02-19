var kanji = "〇一二三四五六七八九".ToCharArray();
var result = new string(args[0].Select(x => kanji[int.Parse(x.ToString())]).ToArray());
Console.WriteLine(result.ToString());