string[] suits = ["spade", "heart", "diamond", "club"];
(string Suit, int No)[] deck = suits.Select(suit => Enumerable.Range(1, 13).Select(no => (suit, no)).ToArray()).SelectMany(x => x).ToArray();
Random.Shared.Shuffle(deck);

(string Suit, int No)[] hands = deck.Take(5).OrderBy(x => x.No).ThenBy(x => x.Suit).ToArray();

foreach (var card in hands) Console.WriteLine($"{card.Suit}: {card.No}");
Console.WriteLine(
    hands.GroupBy(x => x.Suit).Count() == 1 && Enumerable.SequenceEqual(hands.Take(2).Select(x => x.No), new[] { 1, 10 }) ? "ロイヤルストレートフラッシュ" :
    hands.GroupBy(x => x.Suit).Count() == 1 && (hands.Last().No - hands.First().No) == 4 ? "ストレートフラッシュ" :
    hands.GroupBy(x => x.No).Max(x => x.Count()) == 4 ? "フォーカード" :
    Enumerable.SequenceEqual(hands.GroupBy(x => x.No).Select(x => x.Count()).OrderByDescending(x => x).Take(2), new[] { 3, 2 }) ? "フルハウス" :
    hands.GroupBy(x => x.Suit).Count() == 1 ? "フラッシュ" :
    (hands.GroupBy(x => x.No).Count() == 5 && (hands.Last().No - hands.First().No) == 4 || Enumerable.SequenceEqual(hands.Take(2).Select(x => x.No), new[] { 1, 10 })) ? "ストレート" :
    hands.GroupBy(x => x.No).Max(x => x.Count()) == 3 ? "スリーカード" :
    Enumerable.SequenceEqual(hands.GroupBy(x => x.No).Select(x => x.Count()).OrderByDescending(x => x).Take(2), new[] { 2, 2 }) ? "ツーペア" :
    hands.GroupBy(x => x.No).Max(x => x.Count()) == 2 ? "ワンペア" :
    "ブタ"
);
