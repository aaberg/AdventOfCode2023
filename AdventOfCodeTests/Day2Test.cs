using Shouldly;
using TheLibrary.Day2;

namespace AdventOfCodeTests;

public class Day2Test
{
    [Fact]
    public void Parse_parsesCorrectly()
    {
        var input1 =
            "Game 1: 3 blue, 7 green, 10 red; 4 green, 4 red; 1 green, 7 blue, 5 red; 8 blue, 10 red; 7 blue, 19 red, 1 green";
        var game1 = input1.ParseStringAsDay2Game();
        
        game1.Id.ShouldBe(1);
        game1.Sets.Count.ShouldBe(5);

    }
}