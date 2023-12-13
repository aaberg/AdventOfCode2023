var inputStream = File.OpenRead(args[0]);
var input = new StreamReader(inputStream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n').ToList();

var count = 0L;

foreach (var line in inputLines)
{
    var groups = line.Split(' ')[1].Split(',').Select(int.Parse).ToArray();
    var inputStr = line.Split(' ')[0];

    
    var indices = new int[groups.Length];
    var exhausted = false;
    
    var mandatoryIndeces = inputStr.Select((c, i) => new{c, i}).Where(arg => arg.c == '#').Select(arg => arg.i).ToArray();
    
    Console.WriteLine($"\nFinding combinations for this line: {inputStr}");

    while (!exhausted)
    {
        if (CheckCombination(out var combination))
        {
            count++;
            //Console.WriteLine($"Combination: {combination}");
        }

        for (var i = indices.Length-1; i >= 0; i--)
        {
            indices[i]++;
            if (indices[i] < inputStr.Length)
                break;

            indices[i] = i == 0 ? 0 : indices[i - 1] + 1 >= inputStr.Length ? inputStr.Length - 1 : indices[i - 1] + 1;
        }

        exhausted = indices.All(i => i == inputStr.Length - 1);
        
        continue;

        bool CheckCombination(out string combination)
        {
            combination = "";
            var usedIndices = new HashSet<int>();
            var indicesPartOfCombination = new HashSet<int>();
            for (var gi = 0; gi < groups.Length; gi++)
            {
                for (var ci = indices[gi]; ci < indices[gi] + groups[gi]; ci++)
                {
                    if (usedIndices.Contains(ci)) return false;
                    
                    if (ci >= inputStr.Length) return false;
                    
                    if (inputStr[ci] == '#' || inputStr[ci] == '?')
                    {
                        usedIndices.Add(ci);
                        indicesPartOfCombination.Add(ci);
                    }
                    else
                    {
                        return false;
                    }

                    if (ci == indices[gi] + groups[gi] - 1)
                    {
                        usedIndices.Add(ci + 1);
                        
                    }
                }
            }
            
            if (mandatoryIndeces.Except(indicesPartOfCombination).Any())
            {
                return false;
            }

            combination = new string('.', inputStr.Length);
            foreach (var usedIndex in indicesPartOfCombination)
            {
                combination = combination.Remove(usedIndex, 1).Insert(usedIndex, "#");
            }
            
            return true;
        }
    }
}

Console.WriteLine($"Part 1: {count}");

count = 0;

Console.WriteLine("\nPart 2:\n\n\n");


foreach (var line in inputLines)
{
    var g = line.Split(' ')[1].Split(',').Select(int.Parse).ToArray();
    var inputStr = string.Join('?', Enumerable.Range(0, 5).Select(_ => line.Split(' ')[0]));

    var groups = new List<int>();
    for (int i = 0; i < 5; i++)
    {
        groups.AddRange(g);
    }
    
    var indices = new int[groups.Count];
    var exhausted = false;
    
    var mandatoryIndeces = inputStr.Select((c, i) => new{c, i}).Where(arg => arg.c == '#').Select(arg => arg.i).ToArray();
    
    Console.WriteLine($"\nFinding combinations for this line: {inputStr}");

    while (!exhausted)
    {
        if (CheckCombination(out var combination))
        {
            count++;
            //Console.WriteLine($"Combination: {combination}");
        }

        for (var i = indices.Length-1; i >= 0; i--)
        {
            indices[i]++;
            if (indices[i] < inputStr.Length)
                break;

            indices[i] = i == 0 ? 0 : indices[i - 1] + 1 >= inputStr.Length ? inputStr.Length - 1 : indices[i - 1] + 1;
        }

        exhausted = indices.All(i => i == inputStr.Length - 1);
        
        continue;

        bool CheckCombination(out string combination)
        {
            combination = "";
            var usedIndices = new HashSet<int>();
            var indicesPartOfCombination = new HashSet<int>();
            for (var gi = 0; gi < groups.Count; gi++)
            {
                for (var ci = indices[gi]; ci < indices[gi] + groups[gi]; ci++)
                {
                    if (usedIndices.Contains(ci)) return false;
                    
                    if (ci >= inputStr.Length) return false;
                    
                    if (inputStr[ci] == '#' || inputStr[ci] == '?')
                    {
                        usedIndices.Add(ci);
                        indicesPartOfCombination.Add(ci);
                    }
                    else
                    {
                        return false;
                    }

                    if (ci == indices[gi] + groups[gi] - 1)
                    {
                        usedIndices.Add(ci + 1);
                        
                    }
                }
            }
            
            if (mandatoryIndeces.Except(indicesPartOfCombination).Any())
            {
                return false;
            }

            combination = new string('.', inputStr.Length);
            foreach (var usedIndex in indicesPartOfCombination)
            {
                combination = combination.Remove(usedIndex, 1).Insert(usedIndex, "#");
            }
            
            return true;
        }
    }
}