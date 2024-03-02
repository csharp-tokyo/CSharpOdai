namespace _202402.Modeling;

public record 叉焼スペシャル炒飯 : IItem
{
    public static 叉焼スペシャル炒飯 Instance => new();
    public string Name => "叉焼スペシャル炒飯";
    public JapaneseYen Price => new(980);
}
