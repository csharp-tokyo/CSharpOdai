using _202402.Modeling;
namespace _202402Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RegularCaseValueOrder()
    {
        var (command, error) = ラーメン購入コマンド.Parse(["1000", "1"]);
        Assert.Multiple(
            () =>
            {
                Assert.That(command, Is.EqualTo(new ラーメン購入コマンド(new JapaneseYen(1000), new ItemAmount(1))));
                Assert.That(error, Is.EqualTo(string.Empty));
            });
        Assert.Pass();
    }

    [TestCase("0", "1")]
    [TestCase("1", "100")]
    public void SuccessCases(params string[] arg)
    {
        var (command, error) = ラーメン購入コマンド.Parse(arg);
        Assert.Multiple(
            () =>
            {
                Assert.That(command, Is.Not.EqualTo(ラーメン購入コマンド.Empty));
                Assert.That(error, Is.EqualTo(string.Empty));
                Console.WriteLine(error);
            });
        Assert.Pass();
    }


    [TestCase("1")]
    [TestCase("1", "1", "1")]
    [TestCase("1", "1", "1", "1")]
    public void ErrorWhenParameterIsNot2(params string[] arg)
    {
        var (command, error) = ラーメン購入コマンド.Parse(arg);
        Assert.Multiple(
            () =>
            {
                Assert.That(command, Is.EqualTo(ラーメン購入コマンド.Empty));
                Assert.That(error, Is.Not.EqualTo(string.Empty));
                Console.WriteLine(error);
            });
        Assert.Pass();
    }

    [TestCase("1", "aaa")]
    [TestCase("aaa", "1")]
    [TestCase("aaa", "aaa")]
    [TestCase("-1", "1")]
    [TestCase("1", "-1")]
    [TestCase("1", "0")]
    [TestCase("1", "101")]
    public void ErrorWhenParameterIsNotNumber(params string[] arg)
    {
        var (command, error) = ラーメン購入コマンド.Parse(arg);
        Assert.Multiple(
            () =>
            {
                Assert.That(command, Is.EqualTo(ラーメン購入コマンド.Empty));
                Assert.That(error, Is.Not.EqualTo(string.Empty));
                Console.WriteLine(error);
            });
        Assert.Pass();
    }
}
