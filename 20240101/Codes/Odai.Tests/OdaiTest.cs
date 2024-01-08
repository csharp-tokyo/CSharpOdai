using Xunit;

namespace Odai.Tests;

public class OdaiTest
{
    [Theory]
    [MemberData(nameof(GetImplementations))]
    public void Test<T>(
        T implementation)
        where T : IOdai
    {
        Span<char> output = stackalloc char[10];

        implementation.Invoke("01-02-2024", output);

        Assert.Equal("2024/01/02", output.ToString());
    }

    public static IEnumerable<object[]> GetImplementations()
    {
        yield return [DateTimeParseExactAndToString.Instance];
        yield return [DateOnlyParseExactAndToString.Instance];
        yield return [ExpressionTree.Instance];
        yield return [ILEmit.Instance];
        yield return [Roslyn.Instance];
        yield return [CollectionExpression.Instance];
        yield return [NoAllocation.Instance];
    }
}
