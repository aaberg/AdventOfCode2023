var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n');

var lineNumbersList = inputLines
    .Select(l => l.Trim())
    .Select(l => l.Split(' '))
    .Select(l => l.Select(long.Parse).ToArray())
    .ToArray();


var part1Result = lineNumbersList
    .Select(PredictNextNumber)
    .Sum();

Console.WriteLine($"Answer part 1: {part1Result}");

var part2Result = lineNumbersList
    .Select(PredictPreviousNumber)
    .Sum();

Console.WriteLine($"Answer part 2: {part2Result}");

return;

static long PredictNextNumber(long[] line)
{
    return GetSequences(line)
        .Select(s => s[^1]).Sum();
}

static long PredictPreviousNumber(long[] line)
{
    return GetSequences(line)
        .Reverse()
        .Select(s => s[0])
        .Aggregate((l, l1) => l1 - l);
}

static IEnumerable<long[]> GetSequences(long[] line)
{
    List<long[]> sequences = [line];

    while (sequences[^1].Any(v => v != 0))
    {
        var nextSequence = new long[sequences[^1].Length - 1];
        for (var i = 1; i < sequences[^1].Length; i++)
        {
            nextSequence[i - 1] = sequences[^1][i] - sequences[^1][i - 1];
        }
        sequences.Add(nextSequence);
    }

    return sequences;
}