namespace TheLibrary;

public static class Day1Helper
{
    static string[] Numbers = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

    public static string FindFirst(this string input)
    {
        var currentIdx = 1000;
        var currentNumber = "";

        foreach (var number in Numbers)
        {
            var idx = input.IndexOf(number, StringComparison.Ordinal);
            if (idx < 0 || idx >= currentIdx) continue;
            currentIdx = idx;
            currentNumber = number;
        }

        return currentNumber;
    }

    public static string FindLast(this string input)
    {
        var currentIdx = -1;
        var currentNumber = "";

        foreach (var number in Numbers)
        {
            var idx = input.LastIndexOf(number, StringComparison.Ordinal);
            if (idx < 0 || idx <= currentIdx) continue;
            currentIdx = idx;
            currentNumber = number;
        }

        return currentNumber;
    }

    private static Dictionary<string, int> _numberMap = new Dictionary<string, int>
    {
        {"0", 0},
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"zero", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };
    
    public static int StringNumberToIntNumber(this string input)
    {
        try
        {
            return _numberMap[input];
        }
        catch
        {
            Console.WriteLine("Didn't work for: " + input);
            throw;
        }
    }
}