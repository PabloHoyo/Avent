namespace Avent;

internal class Trebuchet
{
    public int Calculate()
    {
        int result = 0;
        var lines = File.ReadAllLines("./1/input.txt");
        foreach (var line in lines) 
        {
            var first = line.First(char.IsDigit);
            var last = line.Last(char.IsDigit);
            result += Convert.ToInt32(string.Concat(first, last));
        }
        return result;
    }
}
