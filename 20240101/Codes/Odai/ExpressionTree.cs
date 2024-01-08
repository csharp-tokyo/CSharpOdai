using System.Globalization;
using System.Linq.Expressions;

namespace Odai;

public sealed class ExpressionTree :
    CodeGenerationBase
{
    public static readonly ExpressionTree Instance = new();

    protected override GeneratedCode GenerateCode()
    {
        var invariantCulture = typeof(CultureInfo).GetProperty(nameof(CultureInfo.InvariantCulture));
        var asSpan = typeof(MemoryExtensions).GetMethod("AsSpan", [typeof(string)]);
        var parseExact = typeof(DateTime).GetMethod(nameof(DateTime.ParseExact), [typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>), typeof(IFormatProvider), typeof(DateTimeStyles)]);
        var tryFormat = typeof(DateTime).GetMethod(nameof(DateTime.TryFormat), [typeof(Span<char>), typeof(int).MakeByRefType(), typeof(ReadOnlySpan<char>), typeof(IFormatProvider)]);

        var inputParameter = Expression.Parameter(typeof(ReadOnlySpan<char>), "input");
        var outputParameter = Expression.Parameter(typeof(Span<char>), "input");
        var culture = Expression.Property(null, invariantCulture!);
        var charsWritten = Expression.Variable(typeof(int), "charsWritten");

        var lambda = Expression.Lambda<GeneratedCode>(
            Expression.Block(
                [charsWritten],
                Expression.Call(
                    Expression.Call(
                        parseExact!,
                        inputParameter,
                        Expression.Call(asSpan!, Expression.Constant("MM-dd-yyyy")),
                        culture,
                        Expression.Constant(DateTimeStyles.None)),
                    tryFormat!,
                    outputParameter,
                    charsWritten,
                    Expression.Call(asSpan!, Expression.Constant("yyyy/MM/dd")),
                    culture)),
            inputParameter,
            outputParameter);

        return lambda.Compile();
    }
}
