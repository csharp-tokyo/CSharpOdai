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
        var asSpan = typeof(MemoryExtensions).GetMethod(nameof(MemoryExtensions.AsSpan), [typeof(string)]);
        var parseExact = typeof(DateTime).GetMethod(nameof(DateTime.ParseExact), [typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>), typeof(IFormatProvider), typeof(DateTimeStyles)]);
        var tryFormat = typeof(DateTime).GetMethod(nameof(DateTime.TryFormat), [typeof(Span<char>), typeof(int).MakeByRefType(), typeof(ReadOnlySpan<char>), typeof(IFormatProvider)]);

        var input = Expression.Parameter(typeof(ReadOnlySpan<char>), "input");
        var output = Expression.Parameter(typeof(Span<char>), "output");

        var culture = Expression.Variable(typeof(CultureInfo), "culture");
        var charsWritten = Expression.Variable(typeof(int), "charsWritten");

        var assignment = Expression.Assign(
            culture,
            Expression.Property(null, invariantCulture!));

        var parse = Expression.Call(
            parseExact!,
            input,
            Expression.Call(asSpan!, Expression.Constant("MM-dd-yyyy")),
            culture,
            Expression.Constant(DateTimeStyles.None));

        var format = Expression.Call(
            parse,
            tryFormat!,
            output,
            charsWritten,
            Expression.Call(asSpan!, Expression.Constant("yyyy/MM/dd")),
            culture);

        var lambda = Expression.Lambda<GeneratedCode>(
            Expression.Block(
                [
                    culture,
                    charsWritten
                ],
                assignment,
                format),
            input,
            output);

        return lambda.Compile();
    }
}
