namespace Avent;

internal partial class PrintQueue : Puzzle
{
    public PrintQueue() : base("./2024/05/input.txt") { }


    public override string Part1()
    {
        var rules = GetRules();
        var pages = GetPages();

        return "";
    }

    public override string Part2()
    {
        return "";
    }

    private List<(int a, int b)> GetRules()
    {
        return lines.TakeWhile(line => line.Length > 0).Select(line => line.Split("|")).Select(rule => (int.Parse(rule[0]), int.Parse(rule[1]))).ToList();
    }

    private List<List<string>> GetPages() 
    { 
        return lines.SkipWhile(line => line.Length > 0).Skip(1).Select(line => line.Split(',').ToList()).ToList();
    }
}
