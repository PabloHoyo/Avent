namespace Avent;

internal class HistorianHysteria : Puzzle
{
    public HistorianHysteria() : base("./2024/01/input.txt") { }


    public override string Part1()
    {
        var (l, r) = ParseLists();
        l.Sort();
        r.Sort();
        var result = l.Zip(r).Sum(x => Math.Abs(x.First - x.Second));
        return result.ToString();
    }

    public override string Part2()
    {
        var (left, right) = ParseLists();
        var result = left.Sum(l => l * right.Count(r => r == l));
        return result.ToString();
    }

    private (List<int> l, List<int> r) ParseLists()
    {
        var l = new List<int>();
        var r = new List<int>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            l.Add(int.Parse(parts.First()));
            r.Add(int.Parse(parts.Last()));
        }
        return (l, r);
    }
}
