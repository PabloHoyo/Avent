﻿namespace Avent;

internal class CubeConundrum : Puzzle
{
    public CubeConundrum() : base("./02/input.txt") { }

    public override string Part1()
    {
        int result = 0;
        foreach (var line in lines)
        {
            var game = CubeGame.Parse(line);
            if (game.IsPossible(maxRed: 12, maxGreen: 13, maxBlue: 14))
            {
                result += game.Id;
            }
        }
        return result.ToString();
    }

    public override string Part2()
    {
        int result = 0;
        foreach (var line in lines)
        {
            var game = CubeGame.Parse(line);
            result += game.GetPower();
        }
        return result.ToString();
    }
}

internal class CubeGame
{
    public int Id { get; init; }
    public List<CubeSet> Sets { get; init; } = new();

    public static CubeGame Parse(string line)
    {
        var sets = line.Split(':')[1].Split(';');
        var game = new CubeGame
        {
            Id = int.Parse(line.Split(':')[0].Split(' ')[1]),
        };
        foreach (var set in sets)
        {
            var colors = set.Split(',');
            var red = 0;
            var green = 0;
            var blue = 0;
            foreach (var color in colors)
            {
                var amount = int.Parse(color.TrimStart().Split(' ')[0]);
                var kind = color.TrimStart().Split(' ')[1];

                switch (kind)
                {
                    case "blue": blue = amount; break;
                    case "green": green = amount; break;
                    case "red": red = amount; break;
                }
            }
            game.AddSet(red, green, blue);
        }
        return game;
    }

    public void AddSet(int red, int green, int blue)
        => Sets.Add(new CubeSet{ Red = red, Green = green, Blue = blue });

    public bool IsPossible(int maxRed, int maxGreen, int maxBlue)
        => Sets.All(set => set.Red <= maxRed && set.Green <= maxGreen && set.Blue <= maxBlue);

    public int GetPower()
        => Sets.Max(set => set.Red) * Sets.Max(set => set.Green) * Sets.Max(set => set.Blue);
    
    internal class CubeSet
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}