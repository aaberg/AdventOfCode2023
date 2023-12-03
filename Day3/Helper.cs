namespace Day3;

public static class Helper
{
    public static bool IsSymbol(this char c)
    {
        return !char.IsNumber(c) && c != '.';
    }
}