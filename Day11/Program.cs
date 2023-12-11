var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n').ToList();

var emptyColumns = new List<int>();
//find empty columns
for (var i = 0; i < inputLines.Count; i++)
    if (inputLines.All(l => l[i] != '#')) emptyColumns.Add(i);

//find empty rows
var emptyRows = inputLines
    .Select((s, i) => new { s, i })
    .Where(arg => !arg.s.Contains('#'))
    .Select(arg => arg.i)
    .ToList();

Console.WriteLine($"Empty columns: {string.Join(',', emptyColumns)}");

var galaxies = inputLines
    .SelectMany((s, lineIdx) => s.Select((c, i) => new { x = i, y = lineIdx, c }).Where(arg => arg.c == '#').Select(arg => new { arg.x, arg.y}) )
    .ToList();

var part1Result = GetDistance(2);

Console.WriteLine($"Answer part 1: {part1Result}");

var part2Result = GetDistance(1000000);

Console.WriteLine($"Answer part 2: {part2Result}");
return;

long GetDistance(int expansionMultiplier)
{
    var sumOfDistances = 0L;
    for (var i = 0; i < galaxies.Count; i++)
    {
        for (var j = i + 1; j < galaxies.Count; j++)
        {
            var g1 = galaxies[i];
            var g2 = galaxies[j];
            var numberOfEmptyRowsBetween = emptyRows.Count(r => (r > g1.y && r < g2.y) || (r < g1.y && r > g2.y));
            var numberOfEmptyColumnsBetween = emptyColumns.Count(c => (c > g1.x && c < g2.x) || (c < g1.x && c > g2.x));
            sumOfDistances += Math.Abs(g1.x - g2.x) - numberOfEmptyColumnsBetween + 
                              Math.Abs(g1.y - g2.y) - numberOfEmptyRowsBetween +
                              numberOfEmptyColumnsBetween * expansionMultiplier +
                              numberOfEmptyRowsBetween * expansionMultiplier;
        }
    }

    return sumOfDistances;
}



