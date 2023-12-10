using System.Diagnostics;

var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n');
var map = inputLines.Select(l => l.ToCharArray()).ToArray();
var csInLoop = new List<Coordinate>();

// find coordinates of S
var sCoordinates = inputLines
    .Select((line, i) => new {line, i})
    .Where(l => l.line.Contains('S'))
    .Select(l => new Coordinate(l.line.IndexOf('S'),l.i))
    .Single();


// Par 1
var nextStep = new { x = 1, y = 0 };
var cc = sCoordinates with {};
var steps = 0;
var tile = 'S';

do
{
    csInLoop.Add(cc);
    cc = cc with
    {
        X = cc.X + nextStep.x, 
        Y = cc.Y + nextStep.y
    };
    steps++;

    tile = map[cc.Y][cc.X];
    nextStep = tile switch
    {
        '|' => new { x = 0, y = nextStep.y == 1 ? 1 : -1 },
        '-' => new { x = nextStep.x == 1 ? 1 : -1, y = 0 },
        'F' => nextStep.x == -1 ? new { x = 0, y = 1 } : new { x = 1, y = 0 },
        '7' => nextStep.x == 1 ? new { x = 0, y = 1 } : new { x = -1, y = 0 },
        'J' => nextStep.x == 1 ? new { x = 0, y = -1 } : new { x = -1, y = 0 },
        'L' => nextStep.x == -1 ? new { x = 0, y = -1 } : new { x = 1, y = 0 },
        'S' => new {x = 0, y = 0},
        '.' => throw new Exception("I'm on a .!"),
        _ => throw new Exception("Ouch")
    };

} while (tile != 'S');

Console.WriteLine($"Answer part 1: {steps / 2}");

// Part 2
var on = 'N';
var inside = false;
var insideCount = 0;
for (var y = 0; y < map.Length; y++)
{
    for (var x = 0; x < map[0].Length; x++)
    {
        tile = map[y][x];
        
        if (tile == 'S')
            tile = 'L';
        
        // Check if the coordinate is one of the pipes in the loop. If not, replace it with a '.'
        if (csInLoop.Count(c => c.X == x && c.Y == y) == 0)
            tile = '.';

        // outside -> in && inside -> out
        if (on == 'N' && tile == '|')
        {
            inside = !inside;
        }
        else if (on == 'N' && (tile == 'F' || tile == 'L'))
        {
            on = tile;
        }
        else if(on == 'F' && tile == 'J')
        {
            inside = !inside;
            on = 'N';
        }
        else if (on == 'F' && tile == '7')
        {
            on = 'N';
        }
        else if (on == 'L' && tile == '7')
        {
            inside = !inside;
            on = 'N';
        }
        else if (on == 'L' && tile == 'J')
        {
            on = 'N';
        }
        
        // We are either inside or outside, no change in state
        else if (inside && tile == '.')
        {
            insideCount++;
        }
    }
}

Console.WriteLine($"Answer part 2: {insideCount}");

record Coordinate(int X, int Y);