namespace Avent;

internal class MirageMaintenance : Puzzle
{
    public MirageMaintenance() : base("./09/input.txt") { }

    public override string Part1()
    {
        var result = 0;
        var values = lines.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
        foreach (var value in values)
        {
            var extrapolations = new List<List<int>>(){ value };
            for (var i = 1; true; i++)
            {
                extrapolations.Add(new());

                for (var j = 0; j < value.Count - i; j++)
                {
                    extrapolations[i].Add(extrapolations[i - 1][j + 1] - extrapolations[i - 1][j]);
                }
                if (extrapolations[i].All(x => x == 0)) break;
            }
            result += extrapolations.Sum(x => x.Last());
        }

        return result.ToString();
    }

    public override string Part2()
    {
        var result = 0;
        var values = lines.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
        foreach (var value in values)
        {
            value.Reverse();
            var extrapolations = new List<List<int>>() { value };
            for (var i = 1; true; i++)
            {
                extrapolations.Add(new());

                for (var j = 0; j < value.Count - i; j++)
                {
                    extrapolations[i].Add(extrapolations[i - 1][j + 1] - extrapolations[i - 1][j]);
                }
                if (extrapolations[i].All(x => x == 0)) break;
            }
            result += extrapolations.Sum(x => x.Last());
        }

        return result.ToString();
    }
}
