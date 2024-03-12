using Xunit;

using FluentAssertions;

namespace ToKanjiLib.Tests;

public sealed class ConverterTest
{
    [Theory]
    [InlineData(0, "〇")]
    [InlineData(1, "一")]
    [InlineData(10, "十")]
    [InlineData(11, "十一")]
    [InlineData(30, "三十")]
    [InlineData(207, "二百七")]
    [InlineData(1_0000, "一万")]
    [InlineData(1_4010, "一万四千十")]
    [InlineData(307_9005, "三百七万九千五")]
    [InlineData(1000_0000, "一千万")]
    [InlineData(1_0000_0000, "一億")]
    [InlineData(1000_0000_0000, "一千億")]
    [InlineData(1000_0000_0008, "一千億八")]
    [InlineData(9, "九")]
    [InlineData(99, "九十九")]
    [InlineData(999, "九百九十九")]
    [InlineData(9999, "九千九百九十九")]
    [InlineData(9_9999, "九万九千九百九十九")]
    [InlineData(99_9999, "九十九万九千九百九十九")]
    [InlineData(999_9999, "九百九十九万九千九百九十九")]
    [InlineData(9999_9999, "九千九百九十九万九千九百九十九")]
    [InlineData(9_9999_9999, "九億九千九百九十九万九千九百九十九")]
    [InlineData(99_9999_9999, "九十九億九千九百九十九万九千九百九十九")]
    [InlineData(999_9999_9999, "九百九十九億九千九百九十九万九千九百九十九")]
    [InlineData(9999_9999_9999, "九千九百九十九億九千九百九十九万九千九百九十九")]
    [InlineData(9_9999_9999_9999, "九兆九千九百九十九億九千九百九十九万九千九百九十九")]
    [InlineData(99_9999_9999_9999, "九十九兆九千九百九十九億九千九百九十九万九千九百九十九")]
    [InlineData(999_9999_9999_9999, "九百九十九兆九千九百九十九億九千九百九十九万九千九百九十九")]
    [InlineData(9999_9999_9999_9999, "九千九百九十九兆九千九百九十九億九千九百九十九万九千九百九十九")]
    public void ToKanjiTest(
        long input,
        string expectedOutput)
    {
        var actual = Converter.ToKanji(input);

        actual.Should().Be(expectedOutput);
    }
}
