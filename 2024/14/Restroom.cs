using System.Text.RegularExpressions;

namespace Avent;

internal partial class Restroom : Puzzle
{
    public Restroom() : base("./2024/14/input.txt") { }

    public override string Part1()
    {
        var robots = ParseRobots();
        foreach (var robot in robots) robot.Move(100);

        return robots.GroupBy(r => r.Quadrant).Select(g => g.Count()).Aggregate((q1, q2) => q1 * q2).ToString();
    }

    public override string Part2()
    {
        return "";
    }

    List<Robot> ParseRobots()
    {
        var regex = new Regex(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
        return lines.Select(line => regex.Match(line)).Select(match =>
            new Robot(
                (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
                (int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)))).ToList();
                
    }

    public class Robot
    {
        static (int x, int y) Bounds = (101, 103);
        public (int x, int y) Position { get; private set; }
        public int Quadrant => Position.x < Bounds.x / 2 ? Position.y < Bounds.y / 2 ? 1 : 2 : Position.y < Bounds.y / 2 ? 3 : 4;
        private (int x, int y) Vector { get; init; }
        public Robot((int x, int y) initialPosition, (int x, int y) vector)
        {
            Position = initialPosition;
            Vector = vector;
        }

        public void Move(int times)
        {
            var x = (Position.x + times * Vector.x) % Bounds.x;
            var y = (Position.y + times * Vector.y) % Bounds.y;
            x = x < 0 ? x + Bounds.x : x;
            y = y < 0 ? y + Bounds.y : y;
            Position = (x, y);
        }
    }
}
