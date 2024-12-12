namespace Avent;

internal class Aplenty : Puzzle
{
    public Aplenty() : base("./19/input.txt") { }

    public override string Part1()
    {
        var result = 0;
        var whiteLine = lines.ToList().IndexOf("");
        var workflows = lines
            .Take(whiteLine)
            .ToDictionary(
                line => line.Substring(0, line.IndexOf('{')), 
                line => line.Substring(line.IndexOf('{') + 1).TrimEnd('}').Split(',').Select(x => new WorkflowRule(x)));
        var ratings = lines.Skip(whiteLine + 1).Select(x => new Rating(x)).ToList();

        foreach (var rating in ratings) 
        {
            var next = "in";
            do
            {
                var workflow = workflows[next];

                foreach (var rule in workflow)
                {
                    if (rule.VerifiesCondition(rating))
                    {
                        next = rule.WhatsNext();
                        break;
                    }
                }
            }
            while (next != "A" && next != "R");

            if (next == "A")
            {
                result += rating.x + rating.m + rating.a + rating.s;
            }
        }
        return result.ToString();
    }

    public override string Part2()
    {
        return "";
    }



    internal class WorkflowRule
    {
        private readonly string variableName;
        private readonly string comparator;
        private readonly int value;
        private readonly string result;

        public WorkflowRule(string line)
        {
            if (line.Contains(':'))
            {
                variableName = line.Substring(0, 1);
                comparator = line.Substring(1, 1);
                value = int.Parse(line.Substring(2, line.IndexOf(':') - 2));
                result = line.Split(':')[1];
            }
            else result = line;
        }

        public bool VerifiesCondition(Rating r)
        {
            if (comparator is null) return true;

            int valueToCompare = (int)typeof(Rating).GetProperty(variableName).GetValue(r);
            if (comparator == "<") return valueToCompare < value;
            else return valueToCompare > value;
        }

        public string WhatsNext()
        {
            return result;
        }
    }

    internal class Rating
    {
        public Rating(string line)
        {
            x = int.Parse(new string(line.Skip(line.IndexOf("x=")+2).TakeWhile(char.IsDigit).ToArray()));
            m = int.Parse(new string(line.Skip(line.IndexOf("m=")+2).TakeWhile(char.IsDigit).ToArray()));
            a = int.Parse(new string(line.Skip(line.IndexOf("a=")+2).TakeWhile(char.IsDigit).ToArray()));
            s = int.Parse(new string(line.Skip(line.IndexOf("s=")+2).TakeWhile(char.IsDigit).ToArray()));
        }

        public int x { get; init; }
        public int m { get; init; }
        public int a { get; init; }
        public int s { get; init; }
    }
}
