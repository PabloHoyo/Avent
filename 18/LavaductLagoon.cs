namespace Avent;

internal class LavaductLagoon : Puzzle
{
    public LavaductLagoon() : base("./18/input.txt") { }

    public override string Part1()
    {
        var shape = new HashSet<(int x, int y)>();
        (int x, int y) coords = new (0,0);
        foreach(var line in lines) 
        { 
            var direction = char.Parse(line.Split(' ')[0]);
            var length = int.Parse(line.Split(' ')[1]);
            for (var i = 0; i < length; i++)
            {
                var x = direction == 'L' ? coords.x - 1 : direction == 'R' ? coords.x + 1 : coords.x;
                var y = direction == 'U' ? coords.y - 1 : direction == 'D' ? coords.y + 1 : coords.y;
                coords = (x, y);
                shape.Add(coords);
            }
        }

        // TODO flood fill
        return "";
    }

    public override string Part2()
    {
        return "";
    }
}
