using Xunit.Abstractions;

namespace AdventOfCodeTests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        for (var i = 0; i < 20; i++)
            _testOutputHelper.WriteLine($"{i % 10}");
    }
}