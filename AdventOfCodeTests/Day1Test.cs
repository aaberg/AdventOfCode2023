using Shouldly;
using TheLibrary;

namespace AdventOfCodeTests;

public class Day1Test
{
    [Theory]
    [InlineData("six1mpffbnbnnlxthree", "six", "three")]
    [InlineData("4eight3one92", "4", "2")]
    [InlineData("5dtxxthtphbnc", "5", "5")]
    public void FindFirst_FindsFirstNumber(string input, string expectedFirst, string expectedLast)
    {
        var first = input.FindFirst();
        
        first.ShouldBe(expectedFirst);

        var last = input.FindLast();
        last.ShouldBe(expectedLast);
    }
    
    [Theory]
    [InlineData("six", 6)]
    [InlineData("three", 3)]
    [InlineData("4", 4)]
    [InlineData("2", 2)]
    public void StringNumberToIntNumber_CorrectlyConverts(string input, int expected)
    {
        input.StringNumberToIntNumber().ShouldBe(expected);
    }
}