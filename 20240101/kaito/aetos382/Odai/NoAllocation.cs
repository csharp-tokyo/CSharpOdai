using System.Diagnostics;

namespace Odai;

public readonly struct NoAllocation :
    IOdai
{
    public void Invoke(
        ReadOnlySpan<char> input,
        Span<char> output)
    {
        Debug.Assert(input.Length >= 10);
        Debug.Assert(output.Length >= 10);

        input[6..10].CopyTo(output);
        output[4] = '/';
        input[0..2].CopyTo(output[5..7]);
        output[7] = '/';
        input[3..5].CopyTo(output[8..10]);
    }

    public static readonly NoAllocation Instance = new();
}