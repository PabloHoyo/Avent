namespace Avent._3;

internal class GearRatios
{
    public int Part1()
    {
        int result = 0;
        var lines = File.ReadAllLines("./3/input.txt");
        for (int y = 0; y < lines.Length; y++)
        {
            var currentNumber = string.Empty;
            var symbolFound = false;
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (char.IsDigit(lines[y][x]))
                {
                    currentNumber += lines[y][x];

                    symbolFound = symbolFound ||
                        IsSymbol(x - 1, y - 1) ||
                        IsSymbol(x - 1, y) ||
                        IsSymbol(x - 1, y + 1) ||
                        IsSymbol(x, y - 1) ||
                        IsSymbol(x, y + 1) ||
                        IsSymbol(x + 1, y - 1) ||
                        IsSymbol(x + 1, y) ||
                        IsSymbol(x + 1, y + 1);
                }
                else 
                {
                    if (currentNumber.Length > 0 && symbolFound) 
                    {
                        result += Convert.ToInt32(currentNumber);
                    }
                    currentNumber = string.Empty;
                    symbolFound = false;
                }
            }
        }
        return result;

        bool IsSymbol(int x, int y)
        {
            if (y < 0 || y >= lines.Length) return false;
            if (x < 0 || x >= lines[y].Length) return false;
            var c = lines[y][x];
            return c != '.' && !char.IsDigit(c);
        }
    }
}
