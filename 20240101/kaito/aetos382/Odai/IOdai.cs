namespace Odai;

public interface IOdai
{
    void Invoke(
        ReadOnlySpan<char> input,
        Span<char> output);
}
