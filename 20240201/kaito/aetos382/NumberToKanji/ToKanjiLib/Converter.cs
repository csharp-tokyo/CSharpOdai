using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace ToKanjiLib;

public static class Converter
{
    private const string Chars1 = "〇一二三四五六七八九";
    private const string Chars2 = "一十百千";
    private const string Chars3 = "一万億兆";

    public static int ToKanji(
        long number,
        Span<char> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(number, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(number, 9999_9999_9999_9999);

        if (number == 0)
        {
            destination[0] = '〇';
            return 1;
        }

#pragma warning disable IDE0055

        ReadOnlySpan<byte> table =
        [
             0,  0,  0,  1,  1,  1,  2,  2,  2,  3,  3,  3,  3,  4,  4,  4,
             5,  5,  5,  6,  6,  6,  6,  7,  7,  7,  8,  8,  8,  9,  9,  9,
             9, 10, 10, 10, 11, 11, 11, 12, 12, 12, 12, 13, 13, 13, 14, 14,
            14, 15, 15, 15, 15, 16, 16, 16, 17, 17, 17, 18, 18, 18, 18, 19
        ];

        ReadOnlySpan<long> powersOf10 =
        [
                                1,
                               10,
                              100,
                             1000,
                           1_0000,
                          10_0000,
                         100_0000,
                        1000_0000,
                      1_0000_0000,
                     10_0000_0000,
                    100_0000_0000,
                   1000_0000_0000,
                 1_0000_0000_0000,
                10_0000_0000_0000,
               100_0000_0000_0000,
              1000_0000_0000_0000,
            1_0000_0000_0000_0000
        ];

#pragma warning restore IDE0055

        var count = (int)table[BitOperations.Log2(unchecked((ulong)number))];

        if (number < powersOf10[count])
        {
            --count;
        }

        Debug.Assert((count + 1) == number.ToString(CultureInfo.InvariantCulture).Length);

        var (man, ju) = Math.DivRem(count, 4);

        var length = 0;
        var addMan = false;

        for (var i = count; i >= 0; --i)
        {
            var (div, rem) = Math.DivRem(number, powersOf10[i]);

            if (div != 0)
            {
                if (div > 1 || ju == 0 || man > 0)
                {
                    destination[length++] = Chars1[(int)div];
                    addMan = number >= 10000;
                }

                if (ju > 0)
                {
                    destination[length++] = Chars2[ju];
                    addMan |= number >= 10000;
                }
            }

            number = rem;

            if (ju > 0)
            {
                --ju;
            }
            else
            {
                if (man > 0 && addMan)
                {
                    destination[length++] = Chars3[man];
                    addMan = false;
                }

                if (number == 0 && !addMan)
                {
                    break;
                }

                ju = 3;
                --man;
            }
        }

        return length;
    }

    public static string ToKanji(long number)
    {
        Span<char> buffer = stackalloc char[50];

        var filledChars = ToKanji(number, buffer);

        return buffer[..filledChars].ToString();
    }
}
