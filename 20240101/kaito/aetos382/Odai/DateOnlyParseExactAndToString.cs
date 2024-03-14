using System.Diagnostics;
using System.Globalization;

namespace Odai;

public readonly struct DateOnlyParseExactAndToString :
    IOdai
{
    public void Invoke(
        ReadOnlySpan<char> input,
        Span<char> output)
    {
        Debug.Assert(input.Length >= 10);
        Debug.Assert(output.Length >= 10);

        var culture = CultureInfo.InvariantCulture;

        DateOnly
            .ParseExact(input, "MM-dd-yyyy", culture)
            .TryFormat(output, out _, "yyyy/MM/dd", culture);
    }

    public static readonly DateOnlyParseExactAndToString Instance = new();
}
