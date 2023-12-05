using System.Reflection;
using TheLibrary.Day2;

// Read file and parse
var inputStream = File.OpenRead(args[0]);
var inputLines = new StreamReader(inputStream)
    .ReadToEnd()
    .Split("\n")
    .Where(s => !string.IsNullOrWhiteSpace(s));

var games = inputLines
    .Select(s => s.ParseStringAsDay2Game())
    .ToList();

// Part 1
var sumOfIds = games
    .Where(g => g.Sets.All(set => set.Red <= 12 && set.Green <= 13 && set.Blue <= 14))
    .Sum(game => game.Id);

Console.WriteLine($"Answer part 1: {sumOfIds}");

// Part 2
var part2Answer =  games.Select(g =>
{
    var minimumRed = g.Sets.Max(set => set.Red);
    var minimumGreen = g.Sets.Max(set => set.Green);
    var minimumBlue = g.Sets.Max(set => set.Blue);

    return minimumRed * minimumGreen * minimumBlue;
}).Sum();

Console.WriteLine($"Answer part 2: {part2Answer}");