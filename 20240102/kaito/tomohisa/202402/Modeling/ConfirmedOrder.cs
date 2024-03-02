using System.Collections.Immutable;
namespace _202402.Modeling;

public record ConfirmedOrder(ImmutableList<OrderDetail> Items, JapaneseYen 受領金, JapaneseYen 売り上げ, JapaneseYen おつり) : IOrder
{
    public JapaneseYen TotalPrice => new(Items.Select(x => x.Item.Price.Value * x.Amount.Value).Sum());
}
