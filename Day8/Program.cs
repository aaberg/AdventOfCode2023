var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n');

var directions = inputLines[0];

var map = inputLines.Skip(2).Select(s => new MapLocation(
    s.Split(" = ")[0],
    s.Split(" = ")[1].Split(", ")[0].Replace("(", ""),
    s.Split(" = ")[1].Split(", ")[1].Replace(")", "")
)).ToDictionary(location => location.Location);


//Part 1
var currentCoordinates = "AAA";
var idx = 0;
var steps = 0;

while (currentCoordinates != "ZZZ")
{
    if (idx >= directions.Length) idx = 0;
    
    var direction = directions[idx];
    var currentLocation = map[currentCoordinates];
    currentCoordinates = direction switch
    {
        'L' => currentLocation.Left,
        'R' => currentLocation.Right,
    };

    idx++;
    steps++;
}

Console.WriteLine($"Answer part 1: {steps}");

// Part 2
var ghosts = map.Values.Where(l => l.Location[2] == 'A').Select(m => m.Location).ToList();

var step = 0;

var stepCnt = new int[ghosts.Count];

while (stepCnt.Any(s => s == 0))
{
    var direction = directions[step++ % directions.Length];
    for (int j = 0; j < ghosts.Count; j++)
    {
        if (stepCnt[j] != 0) continue;
        ghosts[j] = direction switch
        {
            'L' => map[ghosts[j]].Left,
            'R' => map[ghosts[j]].Right,
            _ => throw new Exception("Whaat?")
        };

        if (ghosts[j][2] == 'Z')
        {
            stepCnt[j] = step;
            Console.WriteLine($"Ghost {j} has a Z at steps { step }");
        }
    }
}

var theNumber = 1L;
var stepList = stepCnt.ToList();
while (stepList.Count > 0)
{
    theNumber = Helper.Lcm(theNumber, stepList[0]);
    stepList.RemoveAt(0);
}

Console.WriteLine($"Answer part 2: {theNumber}");

stepCnt.ToList().ForEach(i => Console.WriteLine($"final step: {i}"));

record MapLocation(string Location, string Left, string Right);

static class Helper
{
    static long Gcf(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static long Lcm(long a, long b)
    {
        return (a / Gcf(a, b)) * b;
    }
}