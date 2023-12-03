namespace Avent;

internal class Puzzle
{
    protected string[] lines;

    public Puzzle(string inputPath)
    {
        lines = File.ReadAllLines(inputPath);
    }
}
