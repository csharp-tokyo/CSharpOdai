using System.Diagnostics;

namespace Odai;

public abstract class CodeGenerationBase :
    IOdai
{
    public void Invoke(
        ReadOnlySpan<char> input,
        Span<char> output)
    {
        Debug.Assert(input.Length >= 10);
        Debug.Assert(output.Length >= 10);

        this.Code(input, output);
    }

    protected delegate void GeneratedCode(ReadOnlySpan<char> input, Span<char> output);

    private GeneratedCode? _code;

    private GeneratedCode Code
    {
        get
        {
            return LazyInitializer.EnsureInitialized(ref this._code, GenerateCode);
        }
    }

    protected abstract GeneratedCode GenerateCode();
}
