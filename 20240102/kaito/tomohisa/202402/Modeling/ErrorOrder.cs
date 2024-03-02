using System.Collections.Immutable;
namespace _202402.Modeling;

public record ErrorOrder(ImmutableList<OrderDetail> Items, string Message, JapaneseYen おつり) : IOrder
{
    public JapaneseYen TotalPrice => new(Items.Select(x => x.Item.Price.Value * x.Amount.Value).Sum());
}
