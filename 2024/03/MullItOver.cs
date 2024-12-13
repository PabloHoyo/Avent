using System.Text.RegularExpressions;

namespace Avent;

internal partial class MullItOver : Puzzle
{
    private Regex regex = Mul();

    public MullItOver() : base("./2024/03/input.txt") { }


    public override string Part1()
    {
        return lines.Sum(line => Mul().Matches(line).Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))).ToString();
    }

    public override string Part2()
    {
        var result = 0;
        var on = true;
        var matches = All().Matches(string.Join(null, lines));
        foreach (Match match in matches)
        {
            if (match.Value == "do()") on = true;
            else if (match.Value == "don't()") on = false;
            else if (on) result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }
        return result.ToString();
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex Mul();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|don't\(\)|do\(\)")]
    private static partial Regex All();
}
