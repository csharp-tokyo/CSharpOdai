using System.Collections.Immutable;
namespace _202402.Modeling;

public record AddingOrder(ImmutableList<OrderDetail> Items) : IOrder
{
    public static AddingOrder Empty => new(ImmutableList<OrderDetail>.Empty);
    public JapaneseYen TotalPrice => new(Items.Select(x => x.Item.Price.Value * x.Amount.Value).Sum());
    public bool 買えますか(JapaneseYen 所持金, OrderDetail 注文) => TotalPrice.Value + 注文.Item.Price.Value * 注文.Amount.Value <= 所持金.Value;
    public IOrder 注文に追加(OrderDetail 注文) => Items.Any(i => i.Item.Equals(注文.Item)) ? new ErrorOrder(Items, "すでに同じ商品が入っています", new JapaneseYen(0))
        : new AddingOrder(Items.Add(注文));
    public IOrder 注文を確定する(JapaneseYen 所持金) => TotalPrice.Value <= 所持金.Value ? new ConfirmedOrder(
            Items,
            所持金,
            new JapaneseYen(TotalPrice.Value),
            new JapaneseYen(所持金.Value - TotalPrice.Value))
        : new ErrorOrder(Items, "所持金が足りません", 所持金);
}
