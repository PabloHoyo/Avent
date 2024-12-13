namespace Avent;

internal class Reports : Puzzle
{
    public Reports() : base("./2024/02/input.txt") { }


    public override string Part1()
    {
        return ParseReports().Count(IsSafe).ToString();
    }

    public override string Part2()
    {
        return ParseReports().Count(report => report.Select((x, i) => { List<int> v = [.. report]; v.RemoveAt(i); return v; }).Any(IsSafe)).ToString();
    }

    private List<List<int>> ParseReports()
    {
        return lines.Select(line => line.Split(" ").Select(int.Parse).ToList()).ToList();
    }

    private bool IsSafe(List<int> report)
    {
        var levels = report.Zip(report.Skip(1), (a, b) => new Level(a - b)).ToList();
        var okCount = Math.Max(levels.Count(l => l.IsPosOk), levels.Count(l => l.IsNegOk));
        return okCount == levels.Count;
    }

    internal record Level(int diff)
    {
        public bool IsPosOk => IsDistOk && diff > 0;
        public bool IsNegOk => IsDistOk && diff < 0;
        private bool IsDistOk => Math.Abs(diff) >= 1 && Math.Abs(diff) <= 3;
    }
}
