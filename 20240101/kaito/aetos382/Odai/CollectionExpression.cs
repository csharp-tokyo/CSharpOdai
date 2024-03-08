using System.Diagnostics;

namespace Odai;

public readonly struct CollectionExpression :
    IOdai
{
    public void Invoke(
        ReadOnlySpan<char> input,
        Span<char> output)
    {
        Debug.Assert(input.Length >= 10);
        Debug.Assert(output.Length >= 10);

        Span<char> result = [
            ..input[6..10],
            '/',
            ..input[0..2],
            '/',
            ..input[3..5]
        ];

        result.CopyTo(output);
    }

    public static readonly CollectionExpression Instance = new();
}
