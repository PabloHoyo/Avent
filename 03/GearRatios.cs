namespace Avent;

internal class GearRatios : Puzzle
{
    public GearRatios() : base("./03/input.txt") { }

    public override string Part1()
    {
        int result = 0;
        var currentNumber = string.Empty;
        var symbolFound = false;
        for (int y = 0; y < lines.Length; y++)
        {
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
                    NumberFinished();
                }
            }
            NumberFinished();
        }
        return result.ToString();

        void NumberFinished()
        {
            if (currentNumber.Length > 0 && symbolFound)
            {
                result += Convert.ToInt32(currentNumber);
            }
            currentNumber = string.Empty;
            symbolFound = false;
        }

        bool IsSymbol(int x, int y)
        {
            if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length) return false;
            var c = lines[y][x];
            return c != '.' && !char.IsDigit(c);
        }
    }

    public override string Part2()
    {
        int result = 0;
        var lines = File.ReadAllLines("./3/input.txt");
        var allGears = new Dictionary<(int x, int y), List<int>>();
        var currentGears = new HashSet<(int x, int y)>();
        var currentNumber = string.Empty;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (char.IsDigit(lines[y][x]))
                {
                    currentNumber += lines[y][x];

                    if (IsGear(x - 1, y - 1)) currentGears.Add((x - 1, y - 1));
                    if (IsGear(x - 1, y)) currentGears.Add((x - 1, y));
                    if (IsGear(x - 1, y + 1)) currentGears.Add((x - 1, y + 1));
                    if (IsGear(x, y - 1)) currentGears.Add((x, y - 1));
                    if (IsGear(x, y + 1)) currentGears.Add((x, y + 1));
                    if (IsGear(x + 1, y - 1)) currentGears.Add((x + 1, y - 1));
                    if (IsGear(x + 1, y)) currentGears.Add((x + 1, y));
                    if (IsGear(x + 1, y + 1)) currentGears.Add((x + 1, y + 1));
                }
                else
                {
                    NumberFinished();
                }
            }
            NumberFinished();
        }

        foreach (var gear in allGears)
        {
            if (gear.Value.Count == 2)
            {
                result += gear.Value.Aggregate((a, b) => a * b);
            }
        }
        return result.ToString();

        void NumberFinished()
        {
            if (currentNumber.Length > 0 && currentGears.Any())
            {
                foreach(var gear in currentGears) 
                {
                    if (!allGears.ContainsKey((gear.x, gear.y)))
                    {
                        allGears[(gear.x, gear.y)] = new List<int>();
                    }

                    allGears[(gear.x, gear.y)].Add(Convert.ToInt32(currentNumber));
                }
            }
            currentNumber = string.Empty;
            currentGears.Clear();
        }

        bool IsGear(int x, int y)
        {
            if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length) return false;
            return lines[y][x] == '*';
        }
    }
}
