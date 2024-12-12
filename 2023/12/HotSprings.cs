namespace Avent;

internal class HotSprings : Puzzle
{
    public HotSprings() : base("./12/input.txt") { }

    public override string Part1()
    {
        var result = 0;
        foreach(var line in lines) 
        {
            var springs = line.Split(' ')[0];
            var groups = line.Split(' ')[1].Split(',').Select(int.Parse).ToList();
            var arrangements = GetArrangements(springs, groups);
            arrangements = arrangements.Where(a => groups.SequenceEqual(a.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).ToList())).ToList();
            result += arrangements.Count;
        }
        return result.ToString();
    }

    public override string Part2()
    {
        return "";
    }

    private List<string> GetArrangements(string springs, List<int> groups)
    {
        var index = springs.IndexOf('?');
        if (index < 0)
        {
            return new List<string> { springs };
        }
        if (index > 0 && springs[index - 1] == '.')
        {
            var temp = springs.Substring(0, index - 1).Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).ToList();
            if (!groups.Take(temp.Count()).SequenceEqual(temp))
            {
                return new();
            }
        }
        var option1 = springs.ToCharArray();
        var option2 = springs.ToCharArray();
        option1[index] = '.';
        option2[index] = '#';
        var arr1 = GetArrangements(new string(option1), groups);
        var arr2 = GetArrangements(new string(option2), groups);
        return Enumerable.Concat(arr1, arr2).ToList();
    }
}
