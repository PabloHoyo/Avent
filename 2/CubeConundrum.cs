namespace Avent;

internal class CubeConundrum
{
    public int Part1()
    {
        int result = 0;
        var lines = File.ReadAllLines("./2/input.txt");
        foreach (var line in lines)
        {
            var game = ParseGame(line);
            if (game.IsPossible(maxRed: 12, maxGreen: 13, maxBlue: 14))
            {
                result += game.Id;
            }
        }
        return result;
    }

    public int Part2()
    {
        int result = 0;
        var lines = File.ReadAllLines("./2/input.txt");
        foreach (var line in lines)
        {
            var game = ParseGame(line);
            result += game.GetPower();
        }
        return result;
    }

    private CubeGame ParseGame(string line)
    {
        var sets = line.Split(':')[1].Split(';');
        var game = new CubeGame
        {
            Id = int.Parse(line.Split(':')[0].Split(' ')[1]),
        };
        foreach (var set in sets)
        {
            var newSet = new CubeSet();
            var colors = set.Split(',');
            foreach (var color in colors)
            {
                var amount = int.Parse(color.TrimStart().Split(' ')[0]);
                var kind = color.TrimStart().Split(' ')[1];
                switch (kind)
                {
                    case "blue": newSet.Blue = amount; break;
                    case "green": newSet.Green = amount; break;
                    case "red": newSet.Red = amount; break;
                }
            }
            game.Sets.Add(newSet);
        }
        return game;
    }
}

internal class CubeGame
{
    public int Id { get; init; }
    public List<CubeSet> Sets { get; init; } = new();

    public bool IsPossible(int maxRed, int maxGreen, int maxBlue)
    {
        return Sets.All(set => set.Red <= maxRed && set.Green <= maxGreen && set.Blue <= maxBlue);
    }

    public int GetPower()
    {
        var maxRed = Sets.Max(set => set.Red);
        var maxGreen = Sets.Max(set => set.Green);
        var maxBlue = Sets.Max(set => set.Blue);

        return Math.Max(1, maxRed) * Math.Max(1, maxGreen) * Math.Max(1, maxBlue);
    }
}

internal class CubeSet
{
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }
}


