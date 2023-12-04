using System.Diagnostics;
using System.Reflection;

var inputStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Day4.input.txt")!;
var input = new StreamReader(inputStream).ReadToEnd();

var inputLines = input.Split("\n").Select(l => l.Replace("\r", "")).ToList();

// Parse
var cards = inputLines.Select(s => s.ParseCard()).ToList();

// Part 1
var points = cards
    .Select(card => card.CalculatePoints())
    .Sum();

Console.WriteLine($"Answer to part 1: {points}");

// Part 2
Stopwatch s = new Stopwatch();
s.Start();
var part2Cards = cards
    .Select(card => new Part2Card(card.CardNumber, card.NumberOfWinningNumbers))
    .ToList();

var part2CardsDictionary = part2Cards.ToDictionary(part2Card => part2Card.CardNumber);
var cardIdx = 0;
do
{
    var (cardNumber, numberOfWinningNumbers) = part2Cards[cardIdx];
    for(var idx = 0; idx < numberOfWinningNumbers; idx++)
    {
        if (part2CardsDictionary.TryGetValue(cardNumber + 1 + idx, out var cardToCopy))
        {
            part2Cards.Add(new Part2Card(cardToCopy.CardNumber, cardToCopy.NumberOfWinningNumbers));
        }
    }
} while(++cardIdx < part2Cards.Count);

s.Stop();
Console.WriteLine($"Answer to part 2: {part2Cards.Count} in {s.ElapsedMilliseconds}ms");

class Card
{
    public required int CardNumber { get; init; }
    public required int[] WinningNumbers { get; init; }
    public required int[] CardNumbers { get; init; }

    public int NumberOfWinningNumbers => CardNumbers.Intersect(WinningNumbers).Count();
    public int CalculatePoints()
    {
        var numberOfWinningNumbers = NumberOfWinningNumbers;
        var points = numberOfWinningNumbers > 0 ? 1 : 0;
        for (var i = 1; i < numberOfWinningNumbers; i++)
        {
            points *= 2;
        }

        return points;
    }
}

record Part2Card(int CardNumber, int NumberOfWinningNumbers);

static class Extensions
{
    public static Card ParseCard(this string inputLine)
    {
        var cardNumber = int.Parse(inputLine.Split(":")[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray()[1]);

        var parts = inputLine.Split(":")[1]
            .Split('|');

        return new Card
        {
            CardNumber = cardNumber,
            WinningNumbers = parts[0]
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .Select(int.Parse)
                .ToArray(),
            CardNumbers = parts[1]
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .Select(int.Parse)
                .ToArray(),
        };
    }
}