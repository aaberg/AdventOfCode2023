
var races = new Race[]
{
    new Race(1, 2),
    new Race(1, 2),
    new Race(1, 2),
    new Race(1, 2),
};

var longRace = new Race(1111, 2222);

// Part 1
var resultPart1 = races.Select(CalcWaysToWin)
    .Aggregate(1L, (a, b) => a * b);

Console.WriteLine($"Part 1 answer: {resultPart1}");

// Part 2
var resultPart2 = CalcWaysToWin(longRace);
Console.WriteLine($"Part 2 answer: {resultPart2}");
return;

long CalcWaysToWin(Race r)
{
    var numberOfWaysToWin = 0;
    for (var i = 1L; i < r.Time; i++)
    {
        var dist = (r.Time - i) * i;
        if (dist > r.Distance) numberOfWaysToWin++;
    }
    return numberOfWaysToWin;
}

internal record Race(long Time, long Distance);