namespace _202402.Modeling;

public record 濃厚MAXラーメン : IItem
{
    public static 濃厚MAXラーメン Instance => new();
    public string Name => "濃厚 MAX ラーメン";
    public JapaneseYen Price => new(1080);
}
