namespace Avent;

internal class CosmicExpansion : Puzzle
{
    public CosmicExpansion() : base("./11/input.txt") { }

    private List<int> expansionsX = new();
    private List<int> expansionsY = new();
    private List<Tuple<Galaxy, Galaxy>> galaxyCombinations = new();

    public override string Part1()
    {
        DetectExpansions();
        DetectGalaxies();
        return galaxyCombinations.Sum(x => CalculateDistance(x.Item1, x.Item2)).ToString();
    }
    public override string Part2()
    {
        DetectExpansions();
        DetectGalaxies();
        return galaxyCombinations.Sum(x => CalculateDistance(x.Item1, x.Item2, isPart2: true)).ToString();
    }

    private void DetectExpansions()
    {
        for (int x = 0; x < lines.First().Length; x++)
        {
            if (lines.All(l => l[x] != '#')) expansionsX.Add(x);
        }
        for (int y = 0; y < lines.Count(); y++)
        {
            if (!lines[y].Contains('#')) expansionsY.Add(y);
        }
    }

    private void DetectGalaxies()
    {
        var galaxies = new List<Galaxy>();
        for (int y = 0; y < lines.Count(); y++)
            for (int x = 0; x < lines[y].Length; x++)
                if (lines[y][x] == '#')
                    galaxies.Add(new Galaxy(x, y));

        galaxyCombinations = galaxies
            .SelectMany(x => galaxies, (x, y) => Tuple.Create(x, y))
            .Where(tuple => tuple.Item1.GetHashCode() < tuple.Item2.GetHashCode())
            .ToList();
    }

    private long CalculateDistance(Galaxy a, Galaxy b, bool isPart2 = false)
    {
        var minX = Math.Min(a.x, b.x);
        var maxX = Math.Max(a.x, b.x);
        var minY = Math.Min(a.y, b.y);
        var maxY = Math.Max(a.y, b.y);
        var expansionsInX = expansionsX.Count(x => minX < x && x < maxX);
        var expansionsInY = expansionsY.Count(y => minY < y && y < maxY);
        if (isPart2)
        {
            expansionsInX *= 999999;
            expansionsInY *= 999999;
        }
        return (maxX - minX) + (maxY - minY) + expansionsInX + expansionsInY;
    }

    public struct Galaxy
    {
        public Galaxy(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
        public override int GetHashCode()
        {
            return (base.GetHashCode() * x) + y;
        }
    }
}
