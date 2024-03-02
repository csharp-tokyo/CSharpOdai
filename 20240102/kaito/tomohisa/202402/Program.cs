// See https://aka.ms/new-console-template for more information
using _202402.Modeling;
var (コマンド, errorMessage) = ラーメン購入コマンド.Parse(args);

if (!string.IsNullOrEmpty(errorMessage))
{
    Console.WriteLine(errorMessage);
    return;
}
Console.WriteLine($"所持金{コマンド.所持金.Value}円で、濃厚MAXラーメンを{コマンド.個数.Value}個購入希望ですね。");
var カート = AddingOrder.Empty;
if (!カート.買えますか(コマンド.所持金, new OrderDetail(new 濃厚MAXラーメン(), コマンド.個数)))
{
    Console.WriteLine("申し訳ありませんが、所持金が足りません。");
    return;
}
switch (カート.注文に追加(new OrderDetail(濃厚MAXラーメン.Instance, コマンド.個数)))
{
    case ErrorOrder error:
        Console.WriteLine(error.Message);
        return;
    case AddingOrder order:
        カート = order;
        Console.WriteLine($"{濃厚MAXラーメン.Instance.Name}を{コマンド.個数.Value}個カートに追加しました。");
        Console.WriteLine($"合計金額は{カート.TotalPrice.Value:N0}円です。");
        break;
    default:
        throw new InvalidOperationException();
}
Console.WriteLine($"残りの所持金で買えるだけ{叉焼スペシャル炒飯.Instance.Name}を購入希望ですね。");
var チャーハンの個数 = 0;
while (カート.買えますか(コマンド.所持金, new OrderDetail(叉焼スペシャル炒飯.Instance, new ItemAmount(チャーハンの個数 + 1))))
{
    チャーハンの個数++;
}
switch (チャーハンの個数)
{
    case 0:
        Console.WriteLine("ラーメンを購入した残り金額ではチャーハンは買えません。");
        break;
    default:
        カート = カート.注文に追加(new OrderDetail(叉焼スペシャル炒飯.Instance, new ItemAmount(チャーハンの個数))) switch
        {
            ErrorOrder error => throw new InvalidOperationException(error.Message),
            AddingOrder order when チャーハンの個数 > 0 => order,
            _ => throw new InvalidOperationException("Unable to add Fried Rice.")
        };
        Console.WriteLine($"{叉焼スペシャル炒飯.Instance.Name}を{チャーハンの個数}個カートに追加しました。");
        break;
}
Console.WriteLine("決済を実行します。");
var 最終カート = カート.注文を確定する(コマンド.所持金) switch
{
    ErrorOrder error => throw new InvalidOperationException(error.Message),
    ConfirmedOrder confirmed => confirmed,
    _ => throw new InvalidOperationException("Unable to confirm the order.")
};
Console.WriteLine("------決済完了------");
foreach (var item in 最終カート.Items)
{
    Console.WriteLine($"{item.Item.Name} : {item.Amount.Value:N0}点");
}
Console.WriteLine($"合計金額:{最終カート.TotalPrice.Value:N0}円");
Console.WriteLine($"受領金  :{最終カート.受領金.Value:N0}円");
Console.WriteLine($"売り上げ:{最終カート.売り上げ.Value:N0}円");
Console.WriteLine($"おつり  :{最終カート.おつり.Value:N0}円");
Console.WriteLine("ありがとうございました。");
