using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace Odai;

public readonly struct ILEmit :
    IOdai
{
    public ILEmit()
    {
        this._code = GenerateCode();
    }

    public void Invoke(
        ReadOnlySpan<char> input,
        Span<char> output)
    {
        Debug.Assert(input.Length >= 10);
        Debug.Assert(output.Length >= 10);

        this._code(input, output);
    }

    private delegate void GeneratedCode(ReadOnlySpan<char> input, Span<char> output);

    private readonly GeneratedCode _code;

    public static readonly ILEmit Instance = new();

    private static GeneratedCode GenerateCode()
    {
        var method = new DynamicMethod("Odai", typeof(void), [typeof(ReadOnlySpan<char>), typeof(Span<char>)])
        {
            InitLocals = false
        };

        var gen = method.GetILGenerator();

        var invariantCulture = typeof(CultureInfo).GetProperty(nameof(CultureInfo.InvariantCulture))!.GetMethod;
        var asSpan = typeof(MemoryExtensions).GetMethod("AsSpan", [typeof(string)]);
        var parseExact = typeof(DateTime).GetMethod(nameof(DateTime.ParseExact), [typeof(ReadOnlySpan<char>), typeof(ReadOnlySpan<char>), typeof(IFormatProvider), typeof(DateTimeStyles)]);
        var tryFormat = typeof(DateTime).GetMethod(nameof(DateTime.TryFormat), [typeof(Span<char>), typeof(int).MakeByRefType(), typeof(ReadOnlySpan<char>), typeof(IFormatProvider)]);

        method.DefineParameter(1, ParameterAttributes.In, "input");
        method.DefineParameter(1, ParameterAttributes.In, "output");

        // CultureInfo culture;
        var culture = gen.DeclareLocal(typeof(CultureInfo));

        // DateTime datetime;
        var datetime = gen.DeclareLocal(typeof(DateTime));

        // int charsWritten;
        var charsWritten = gen.DeclareLocal(typeof(int));

        // culture = CultureInfo.InvariantCulture
        gen.Emit(OpCodes.Call, invariantCulture!);
        gen.Emit(OpCodes.Stloc, culture);

        // datetime = DateTime.ParseExact(input, "MM-dd-yyyy".AsSpan(), culture, DateTimeStyles.None);
        gen.Emit(OpCodes.Ldarg_0);
        gen.Emit(OpCodes.Ldstr, "MM-dd-yyyy");
        gen.Emit(OpCodes.Call, asSpan!);
        gen.Emit(OpCodes.Ldloc, culture);
        gen.Emit(OpCodes.Ldc_I4, (int)DateTimeStyles.None);
        gen.Emit(OpCodes.Call, parseExact!);
        gen.Emit(OpCodes.Stloc, datetime);

        // _ = datetime.TryFormat(output, out charsWritten, "yyyy/MM/dd".AsSpan(), culture);
        gen.Emit(OpCodes.Ldloca, datetime);
        gen.Emit(OpCodes.Ldarg_1);
        gen.Emit(OpCodes.Ldloca, charsWritten);
        gen.Emit(OpCodes.Ldstr, "yyyy/MM/dd");
        gen.Emit(OpCodes.Call, asSpan!);
        gen.Emit(OpCodes.Ldloc, culture);
        gen.Emit(OpCodes.Call, tryFormat!);
        gen.Emit(OpCodes.Pop);

        gen.Emit(OpCodes.Ret);

        var d = method.CreateDelegate<GeneratedCode>();
        return d;
    }
}