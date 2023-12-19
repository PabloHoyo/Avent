namespace Avent;

internal class TheFloorWillBeLava : Puzzle
{
    public TheFloorWillBeLava() : base("./16/input.txt") { }
    public HashSet<((int x, int y) current, (int x, int y) next)> steps = new();

    public override string Part1()
    {
        Navigate((-1, 0), (0,0));
        var energizedTiles = steps.Select(x => x.next).Distinct().Count();
        return energizedTiles.ToString();
    }

    private void Navigate((int x, int y) current, (int x, int y) next)
    {
        if (next.x < 0 || 
            next.y < 0 || 
            next.y >= lines.Count() || 
            next.x >= lines[next.y].Count())
        {
            return;
        }

        if (steps.Contains((current, next)))
        {
            return;
        }

        steps.Add((current, next));
        switch (lines[next.y][next.x])
        {
            case '.':
            case '|' when current.x == next.x:
            case '-' when current.y == next.y:
                Navigate(next, (2 * next.x - current.x, 2 * next.y - current.y));
                break;

            case '|' when current.y == next.y:
                Navigate(next, (next.x, next.y - 1));
                Navigate(next, (next.x, next.y + 1));
                break;

            case '-' when current.x == next.x:
                Navigate(next, (next.x - 1, next.y));
                Navigate(next, (next.x + 1, next.y));
                break;

            case '/' when current.y == next.y && current.x < next.x:
            case '\\' when current.y == next.y && current.x > next.x:
                Navigate(next, (next.x, next.y - 1));
                break;

            case '/' when current.y == next.y && current.x > next.x:
            case '\\' when current.y == next.y && current.x < next.x:
                Navigate(next, (next.x, next.y + 1));
                break;

            case '/' when current.x == next.x && current.y < next.y:
            case '\\' when current.x == next.x && current.y > next.y:
                Navigate(next, (next.x - 1, next.y));
                break;

            case '/' when current.x == next.x && current.y > next.y:
            case '\\' when current.x == next.x && current.y < next.y:
                Navigate(next, (next.x + 1, next.y));
                break;
        }
    }

    public override string Part2()
    {
        var maxResult = 0;
        for (int y = 0; y < lines.Count(); y++) 
        {
            // From left
            steps.Clear();
            Navigate((-1, y), (0, y));
            maxResult = Math.Max(maxResult, steps.Select(x => x.next).Distinct().Count());

            // From right
            steps.Clear();
            Navigate((lines.First().Count(), y), (lines.First().Count() - 1, y));
            maxResult = Math.Max(maxResult, steps.Select(x => x.next).Distinct().Count());
        }
        for (int x = 0; x < lines.First().Count(); x++)
        {
            // From top
            steps.Clear();
            Navigate((x, -1), (x, 0));
            maxResult = Math.Max(maxResult, steps.Select(x => x.next).Distinct().Count());

            // From bottom
            steps.Clear();
            Navigate((x, lines.Count()), (x, lines.Count() - 1));
            maxResult = Math.Max(maxResult, steps.Select(x => x.next).Distinct().Count());
        }
        return maxResult.ToString();
    }
}
