namespace Avent;

internal class Trebuchet : Puzzle
{
    public Trebuchet() : base("./01/input.txt") { }

    private readonly Dictionary<string, int> numbers = new()
    {
        { "one" , 1 },
        { "two" , 2 },
        { "three" , 3},
        { "four" , 4 },
        { "five" , 5 },
        { "six" , 6 },
        { "seven" , 7 },
        { "eight" , 8 },
        { "nine" , 9 },
    };

    public override string Part1()
    {
        int result = 0;
        foreach (var line in lines) 
        {
            var first = line.First(char.IsDigit);
            var last = line.Last(char.IsDigit);
            result += Convert.ToInt32(string.Concat(first, last));
        }
        return result.ToString();
    }
    public override string Part2()
    {
        int result = 0;
        foreach (var line in lines)
        {
            var first = FindNumber(line);
            var last = FindNumber(line, fromEnd: true);
            result += Convert.ToInt32(string.Concat(first, last));
        }
        return result.ToString();
    }

    private int FindNumber(string text, bool fromEnd = false)
    {
        while (text.Length > 0)
        {
            var currentChar = fromEnd ? text.Last() : text.First();
            if (char.IsDigit(currentChar))
            {
                return Convert.ToInt32(currentChar.ToString());
            }
            var spelled = numbers.Keys.FirstOrDefault(fromEnd ? text.EndsWith : text.StartsWith);
            if (spelled != null)
            {
                return numbers[spelled];
            }
            text = fromEnd ? text.Substring(0, text.Length - 1) : text.Substring(1);
        }
        throw new Exception($"no numbers found in text");
    }
}
