var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n');

// Parse
var seeds = inputLines[0].Split(' ').Skip(1).Select(long.Parse).ToList();

var maps = new List<Map>();
Map currentMap = null;

foreach (var line in inputLines.Skip(1))
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    if (!char.IsNumber(line.First()))
    {
        var name = line.Replace(":", "");
        currentMap = new Map(name, new List<MapUnit>());
        maps.Add(currentMap);
    }
    else
    {
        var n = line.Split(' ');
        currentMap!.MapUnits.Add(
            new MapUnit(long.Parse( n[0]), long.Parse(n[1]), long.Parse(n[2])));
    }
}

// Part 1
var res = seeds.Select(GetLocation).Min();

Console.WriteLine($"Part 1 answer: {res}");

var nearestLocation = long.MaxValue;
// Part 2
for (var i = 0; i < seeds.Count; i += 2)
{
    var seed = seeds[i];
    var count = seeds[i + 1];
    
    for (var j = seed; j < seed + count; j++)
    {
        var seedLocation = GetLocation(j);
        if (seedLocation < nearestLocation)
            nearestLocation = seedLocation;

        if (j % 1000000 == 0)
        {
            Console.WriteLine($"{seed} Looped 1M times. Nearest location is {nearestLocation}");
        }
    }
}

Console.WriteLine($"Part 2 answer: {nearestLocation}");

return;

long GetLocation(long seed)
{
    var s = seed;
    foreach (Map map in maps)
    {
        var mu = map.MapUnits
            .SingleOrDefault(m => s >= m.SourceStart && s < m.SourceStart + m.Length);
        if (mu != null)
            s = s - mu.SourceStart + mu.DestinationStart;
    }
    return s;
}

public record MapUnit(long DestinationStart, long SourceStart, long Length);

public record Map(string Name, List<MapUnit> MapUnits);