using Day3;

var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();

var inputLines = input.Split("\n");

List<PartNumber> partNumbers = new();
List<int> positions = new();
var number = "";
for (var lineIdx = 0; lineIdx < inputLines.Length; lineIdx++)
{
    var line = inputLines[lineIdx];
    for (var characterIdx = 0; characterIdx < line.Length; characterIdx++)
    {
        var character = line[characterIdx];
        if (char.IsNumber(character))
        {
            number += character;
            positions.Add(characterIdx);
        }
        else if (number != "")
        {
            partNumbers.Add(new PartNumber
            {
                Number = number,
                LineIndex = lineIdx,
                Positions = new List<int>(positions),
            });
            
            number = "";
            positions.Clear();
        }
        
        if (characterIdx == line.Length - 1 && number != "")
        {
            partNumbers.Add(new PartNumber
            {
                Number = number,
                LineIndex = lineIdx,
                Positions = new List<int>(positions),
            });
            
            number = "";
            positions.Clear();
        }
    }
}

// Search for symbols around the number
foreach (var partNumber in partNumbers)
{
    var startLine = partNumber.LineIndex - 1;
    startLine = startLine < 0 ? 0 : startLine;
    var endLine = partNumber.LineIndex + 1;
    endLine = endLine > inputLines.Length - 1 ? inputLines.Length - 1 : endLine;
    
    var positionsToCheck = partNumber.Positions
        .Append(partNumber.Positions.First() - 1)
        .Append(partNumber.Positions.Last() + 1)
        .Where(p => p >= 0 && p < inputLines[partNumber.LineIndex].Length)
        .ToList();
    
    for (int lineIdx = startLine; lineIdx <= endLine; lineIdx++)
    {
        var line = inputLines[lineIdx];
        var hasSymbol = positionsToCheck.Any(i =>
        {
            var isSymbol = line[i].IsSymbol();
            if (isSymbol)
            {
                partNumber.Symbol = line[i];
                partNumber.SymbolX = i;
                partNumber.SymbolY = lineIdx;
            }
            return isSymbol;
            
        });
        if (hasSymbol)
        {
            partNumber.HasSymbol = true;
            break;
        }
    }
}

partNumbers.Where(p => p.HasSymbol).ToList().ForEach(p => Console.WriteLine(p.Number));

var sumOfPartNumbers = partNumbers
    .Where(partNumber => partNumber.HasSymbol)
    .Select(partNumber => int.Parse(partNumber.Number))
    .Sum();

Console.WriteLine("Answer part 1: " + sumOfPartNumbers);

// part 2
var gears = partNumbers
    .Where(p => p.Symbol == '*')
    .Where(p => partNumbers.Count(p1 => p1.SymbolX == p.SymbolX && p1.SymbolY == p.SymbolY) == 2)
    .ToList();

List<long> gearRatios = new();

while (gears.Count > 0)
{
    var currentGear = gears.First();
    gears.Remove(currentGear);
    var matchingGear = gears.First(g => g.SymbolX == currentGear.SymbolX && g.SymbolY == currentGear.SymbolY);
    gears.Remove(matchingGear);
    
    gearRatios.Add(long.Parse(currentGear.Number) * long.Parse(matchingGear.Number));
}

Console.WriteLine("Answer part 3: " + gearRatios.Sum());

class PartNumber
{
    public required string Number {get; init; }
    public required int LineIndex { get; init; }
    public required List<int> Positions { get; init; }
    public bool HasSymbol { get; set; } = false;
    
    public char Symbol { get; set; } = ' ';
    public int SymbolX { get; set; } = 0;
    public int SymbolY { get; set; } = 0;
}