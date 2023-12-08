namespace Avent;

internal class HauntedWasteland : Puzzle
{
    public HauntedWasteland() : base("./08/input.txt") { }

    public override string Part1()
    {
        var instructions = lines.First();
        var nodes = GetNodes();
        var stepsCount = 0;
        var instructionsIndex = 0;
        var currentNode = "AAA";
        do
        {
            currentNode = instructions[instructionsIndex] == 'R' ? nodes[currentNode].right : nodes[currentNode].left;
            stepsCount++;
            instructionsIndex = instructionsIndex == instructions.Length - 1 ? 0 : instructionsIndex + 1;
        }
        while (currentNode != "ZZZ");

        return stepsCount.ToString();
    }

    public override string Part2()
    {
        var instructions = lines.First();
        var nodes = GetNodes();
        var stepsCount = default(long);
        var instructionsIndex = 0;
        var currentNodes = nodes.Keys.Where(x => x.EndsWith('A')).ToList();
        var loopLengths = currentNodes.Select(x => default(long)).ToList();
        do
        {
            for (var i = 0; i < currentNodes.Count(); i++)
            {
                currentNodes[i] = instructions[instructionsIndex] == 'R' ? nodes[currentNodes[i]].right : nodes[currentNodes[i]].left;
                if (currentNodes[i].EndsWith('Z') && loopLengths[i] == 0)
                {
                    loopLengths[i] = stepsCount + 1;
                }
            }
            stepsCount++;
            instructionsIndex = instructionsIndex == instructions.Length - 1 ? 0 : instructionsIndex + 1;
        }
        while (!loopLengths.All(x => x > 0));

        return BrutishLcm(loopLengths).ToString();
    }

    private Dictionary<string, (string left, string right)> GetNodes()
    {
        var nodes = new Dictionary<string, (string left, string right)>();
        foreach (var line in lines.Skip(2))
        {
            nodes[line.Substring(0, 3)] = new(line.Substring(7, 3), line.Substring(12, 3));
        }

        return nodes;
    }

    private static long BrutishLcm(List<long> numbers)
    {
        var i = default(long);
        var step = numbers.Max();

        do
        {
            i += step;
        }
        while (!numbers.All(x => i % x == 0));
        return i;
    }
}
