// See https://aka.ms/new-console-template for more information

using System.Reflection;
using TheLibrary.Day2;

var inputStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Day2.input.txt");
if (inputStream is null) throw new Exception("Input wasn't read correctly.");
var inputLines = new StreamReader(inputStream)
    .ReadToEnd()
    .Split("\n")
    .Where(s => !string.IsNullOrWhiteSpace(s));

var games = inputLines.Select(s => s.ParseStringAsDay2Game()).ToList();

var possibleGames = games.Where(g => g.Sets.All(set => set.Red <= 12 && set.Green <= 13 && set.Blue <= 14));

var sumOfIds = possibleGames.Sum(g => g.Id);
Console.WriteLine("Answer part 1: " + sumOfIds);

var part2Answer = 0L;

foreach (var g in games)
{
    var minimumRed = g.Sets.Max(set => set.Red);
    var minimumGreen = g.Sets.Max(set => set.Green);
    var minimumBlue = g.Sets.Max(set => set.Blue);
    
    var product = minimumRed * minimumGreen * minimumBlue;

    part2Answer += product;
}

Console.WriteLine($"Part 2 answer: {part2Answer}");