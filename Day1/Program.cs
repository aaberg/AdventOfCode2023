// See https://aka.ms/new-console-template for more information

using System.Reflection;
using TheLibrary;

var inputStream = File.OpenRead(args[0]);

var input = new StreamReader(inputStream).ReadToEnd();

var inputLines = input.Split("\n");

// Part 1
var sumOfCalibrationNumbersPart1 = inputLines.Select(s =>
{
    if (string.IsNullOrWhiteSpace(s)) return 0;
    var firstNumber = s.First(char.IsNumber);
    var lastNumber = s.Last(char.IsNumber);
    return int.Parse($"{firstNumber}{lastNumber}");
}).Sum();

Console.WriteLine($"Answer: {sumOfCalibrationNumbersPart1}");

// Part 2
var sumOfCalibrationNumbersPart2 = inputLines.Select(s =>
{
    if (string.IsNullOrWhiteSpace(s)) return 0;

    try
    {
        var firstNumber = s.FindFirst().StringNumberToIntNumber();
        var lastNumber = s.FindLast().StringNumberToIntNumber();
        return int.Parse($"{firstNumber}{lastNumber}");
    }
    catch
    {
        Console.WriteLine("Didn't work for: " + s);
        throw;
    }

    
}).Sum();

Console.WriteLine($"Answer: {sumOfCalibrationNumbersPart2}");
