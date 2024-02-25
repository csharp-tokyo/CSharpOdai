namespace _202402.Modeling;

public record ラーメン購入コマンド(JapaneseYen 所持金, ItemAmount 個数)
{
    public static ラーメン購入コマンド Empty => new(JapaneseYen.Empty, ItemAmount.Empty);
    public static (ラーメン購入コマンド, string) Parse(string[] args)
    {
        if (args.Length < 2)
        {
            return (Empty, "引数が足りません。 [所持金] [個数] で入力してください。");
        }
        if (args.Length > 2)
        {
            return (Empty, "引数が多すぎます。 [所持金] [個数] で入力してください。");
        }
        var (yen, yenMessage) = JapaneseYen.Parse(args[0]);
        var (amount, amountMessage) = ItemAmount.Parse(args[1]);
        if (!string.IsNullOrEmpty($"{yenMessage}{amountMessage}"))
        {
            return (Empty, $"{yenMessage}{amountMessage}");
        }
        return (new ラーメン購入コマンド(yen, amount), string.Empty);
    }
}
