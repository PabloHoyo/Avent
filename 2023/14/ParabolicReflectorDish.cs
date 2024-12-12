namespace Avent;

internal class ParabolicReflectorDish : Puzzle
{
    public ParabolicReflectorDish() : base("./14/input.txt") { }

    public override string Part1()
    {
        int result = 0;
        var map = lines.Select(x => x.ToCharArray()).ToArray();
        for (int i = 0; i < map.Count(); i++) 
        { 
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 'O')
                {
                    SlideNorth(i, j);
                }
            }
        }
        return result.ToString();

        void SlideNorth(int i, int j) 
        { 
            if (i > 0 && map[i-1][j] == '.')
            {
                map[i][j] = '.';
                map[i - 1][j] = 'O';
                SlideNorth(i - 1, j);
            }
            else
            {
                result += map.Length - i;
            }
        }
    }

    public override string Part2()
    {
        return "";
    }

}
