
namespace Avent;

internal class PipeMaze : Puzzle
{
    public PipeMaze() : base("./10/input.txt") { }

    HashSet<(int x, int y)> visited;

    public override string Part1()
    {
        visited = new();
        var current = FindStart();
        do
        {
            visited.Add(current);
            current = Next(current);
        }
        while (current.x >= 0);
        return (visited.Count / 2).ToString();
    }

    public override string Part2()
    {
        return "";
    }

    private (int x, int y) FindStart()
    {
        for (var y = 0; y < lines.Length; y++)
        {
            if (lines[y].Contains('S'))
            {
                return (lines[y].IndexOf('S'), y);
            }
        }
        throw new Exception("No start tile found");
    }

    private (int x, int y) Next((int x, int y) tile)
    {
        var x = tile.x;
        var y = tile.y;
        var c = lines[y][x];
        if ("S-J7".Contains(c) && x - 1 >= 0)
        {
            var left = lines[y][x - 1];
            if (!visited.Contains((x - 1, y)) && "-FL".Contains(left))
            {
                return (x - 1, y);
            }
        }
        if ("S-FL".Contains(c) && x + 1 < lines[y].Length)
        {
            var right = lines[y][x + 1];
            if (!visited.Contains((x + 1, y)) && "-7J".Contains(right))
            {
                return (x + 1, y);
            }
        }
        if ("S|LJ".Contains(c) && y - 1 >= 0)
        {
            var up = lines[y - 1][x];
            if (!visited.Contains((x, y - 1)) && "|7F".Contains(up))
            {
                return (x, y - 1);
            }
        }
        if ("S|F7".Contains(c) && y + 1 < lines.Count())
        {
            var down = lines[y + 1][x];
            if (!visited.Contains((x, y + 1)) && "|LJ".Contains(down))
            {
                return (x, y + 1);
            }
        }
        return (-1, -1);
    }
}
