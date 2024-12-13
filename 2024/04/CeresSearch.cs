namespace Avent;

internal partial class CeresSearch : Puzzle
{
    public CeresSearch() : base("./2024/04/input.txt") { }


    public override string Part1()
    {
        return lines.Select((line, y) => line.Select((c, x) => CountXmas(y, x)).Sum()).Sum().ToString();
    }

    public override string Part2()
    {
        return lines.Select((line, y) => line.Select((c, x) => CountMas(y, x)).Sum()).Sum().ToString();
    }

    int CountXmas(int y, int x)
    {
        if (Get(y, x) != 'X') return 0;
        var count = 0;
        if (Get(y - 1, x) == 'M' && Get(y - 2, x) == 'A' && Get(y - 3, x) == 'S') count++;
        if (Get(y + 1, x) == 'M' && Get(y + 2, x) == 'A' && Get(y + 3, x) == 'S') count++;
        if (Get(y, x - 1) == 'M' && Get(y, x - 2) == 'A' && Get(y, x - 3) == 'S') count++;
        if (Get(y, x + 1) == 'M' && Get(y, x + 2) == 'A' && Get(y, x + 3) == 'S') count++;
        if (Get(y - 1, x - 1) == 'M' && Get(y - 2, x - 2) == 'A' && Get(y - 3, x - 3) == 'S') count++;
        if (Get(y + 1, x + 1) == 'M' && Get(y + 2, x + 2) == 'A' && Get(y + 3, x + 3) == 'S') count++;
        if (Get(y - 1, x + 1) == 'M' && Get(y - 2, x + 2) == 'A' && Get(y - 3, x + 3) == 'S') count++;
        if (Get(y + 1, x - 1) == 'M' && Get(y + 2, x - 2) == 'A' && Get(y + 3, x - 3) == 'S') count++;
        return count;
    }

    int CountMas(int y, int x)
    {
        if (Get(y, x) != 'A') return 0;
        List<string> axes = ["MS", "SM"];
        var axe1 = string.Concat(Get(y - 1, x - 1), Get(y + 1, x + 1));
        var axe2 = string.Concat(Get(y + 1, x - 1), Get(y - 1, x + 1));
        return axes.Contains(axe1) && axes.Contains(axe2) ? 1 : 0;
    }

    char Get(int y, int x)
    {
        if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length) return ' ';
        return lines[y][x];
    }
}
