using System.IO.IsolatedStorage;

namespace Avent;

internal class Reports : Puzzle
{
    public Reports() : base("./2024/02/input.txt") { }


    public override string Part1()
    {
        return ParseReports().Count(x => IsSafe(x)).ToString();
    }

    public override string Part2()
    {
        return ParseReports().Count(x => IsSafe(x, dampenerProblem: true)).ToString();
    }

    private List<List<int>> ParseReports()
    {
        return lines.Select(line => line.Split(" ").Select(int.Parse).ToList()).ToList();
    }

    private bool IsSafe(List<int> report, bool dampenerProblem = false)
    {
        var diffs = report.Zip(report.Skip(1), (a, b) => a - b).ToList();
        var minAllowedOkDiffs = dampenerProblem ? diffs.Count - 1 : diffs.Count;
        var sequence = Math.Max(diffs.Count(diff => diff > 0), diffs.Count(diff => diff < 0));
        var distance = diffs.Count(diff => 1 <= Math.Abs(diff) && Math.Abs(diff) <= 3);
        return sequence >= minAllowedOkDiffs && distance >= minAllowedOkDiffs;
    }

}
