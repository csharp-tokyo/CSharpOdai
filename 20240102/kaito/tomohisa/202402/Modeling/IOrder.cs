using System.Collections.Immutable;
namespace _202402.Modeling;

public interface IOrder
{
    public ImmutableList<OrderDetail> Items { get; }
    public JapaneseYen TotalPrice { get; }
}
