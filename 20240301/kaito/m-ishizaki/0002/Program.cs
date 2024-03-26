using Card = (char Suit, int No);

// 出力を UTF8 にする
Console.OutputEncoding = System.Text.Encoding.UTF8;

// デッキを作る
var deck = new[] { '♤', '♡', '♢', '♧' }.Select(suit => Enumerable.Range(1, 13).Select(no => new Card(suit, no))).SelectMany(card => card).OrderBy(x => Guid.NewGuid()).ToArray();

// 手札を作る (5 毎引く) 役を表示
(_, var hands) = DrawCards(5);

for (; ; )
{
    // 手札と役を表示
    WriteHandsAndHand();
    // 交換する手札を選択
    Console.WriteLine("交換する手札を選んでください。一番左のカードが 1、一番右のカードが 5 になります。一番左のカードと一番右のカードを交換する場合、15 と入力してください。交換する必要がない場合は、何も入力せず Enter を押下してください。");
    var line = Console.ReadLine();
    if (string.IsNullOrEmpty(line)) break;
    // 交換する手札
    var changes = line.Select(input => int.TryParse(input.ToString(), out var no) ? no : -1).Where(no => no is >= 1 and <= 5).Distinct().OrderDescending().ToArray();
    // カードを引く
    var drawed = DrawCards(changes.Length);
    if (!drawed.Success)
    {
        Console.WriteLine("手札を交換できません。山にカードが足りません。");
        continue;
    }
    // 交換
    foreach (var change in changes) hands.RemoveAt(change - 1);
    foreach (var card in drawed.Cards) hands.Add(card);
}

// 手札と役を表示して終了
WriteHandsAndHand();

// -- 以降メソッド

// カードを引く
(bool Success, IList<Card> Cards) DrawCards(int numberOfCards)
{
    if (deck.Length < numberOfCards) return (false, new List<Card>());
    var result = (true, deck.Take(numberOfCards).ToList());
    deck = deck.Skip(numberOfCards).ToArray();
    return result;
};

// 手札の並べ替え
void Sort() => hands = hands.OrderBy(card => card.No).ThenBy(card => card.Suit).ToList();

// 手札と役を表示
void WriteHandsAndHand()
{
    Sort();
    Console.WriteLine(string.Join(", ", hands.Select(card => $"{card.Suit} {card.No}")));
    Console.WriteLine(Hand());
}

// 役を判定する
string Hand()
{
    if (hands.GroupBy(x => x.Suit).Count() == 1 && Enumerable.SequenceEqual(hands.Take(2).Select(x => x.No), new[] { 1, 10 }))
        return "ロイヤルストレートフラッシュ";
    if (hands.GroupBy(x => x.Suit).Count() == 1 && (hands.Last().No - hands.First().No) == 4)
        return "ストレートフラッシュ";
    if (hands.GroupBy(x => x.No).Max(x => x.Count()) == 4)
        return "フォーカード";
    if (Enumerable.SequenceEqual(hands.GroupBy(x => x.No).Select(x => x.Count()).OrderByDescending(x => x).Take(2), new[] { 3, 2 }))
        return "フルハウス";
    if (hands.GroupBy(x => x.Suit).Count() == 1)
        return "フラッシュ";
    if ((hands.GroupBy(x => x.No).Count() == 5 && (hands.Last().No - hands.First().No) == 4) || Enumerable.SequenceEqual(hands.Take(2).Select(x => x.No), new[] { 1, 10 }))
        return "ストレート";
    if (hands.GroupBy(x => x.No).Max(x => x.Count()) == 3)
        return "スリーカード";
    if (Enumerable.SequenceEqual(hands.GroupBy(x => x.No).Select(x => x.Count()).OrderByDescending(x => x).Take(2), new[] { 2, 2 }))
        return "ツーペア";
    if (hands.GroupBy(x => x.No).Max(x => x.Count()) == 2)
        return "ワンペア";
    return "ブタ";
}
