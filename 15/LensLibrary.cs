using System.Collections.Specialized;

namespace Avent;

internal class LensLibrary : Puzzle
{
    public LensLibrary() : base("./15/input.txt") { }

    public override string Part1()
    {
        return lines.First().Split(',').Sum(Hash).ToString();
    }

    public override string Part2()
    {
        var result = 0;
        var hashmap = new Dictionary<int, OrderedDictionary>();
        var steps = lines.First().Split(",").ToList();
        foreach (var step in steps) 
        { 
            if (step.Contains('='))
            {
                var label = step.Split('=').First();
                var power = step.Split('=').Last();
                var hash = Hash(label);
                if (!hashmap.ContainsKey(hash)) hashmap.Add(hash, new OrderedDictionary());
                hashmap[hash][label] = power;            
            }
            else 
            {
                var label = step.TrimEnd('-');
                var hash = Hash(label);
                if (hashmap.ContainsKey(hash)) hashmap[hash].Remove(label);
            }
        }

        foreach (var box in hashmap)
        {
            var enumerator = box.Value.GetEnumerator();
            int position = 0;
            while (enumerator.MoveNext())
            {
                result += (box.Key + 1) * int.Parse(enumerator.Value.ToString()) * ++position;
            }
        }
        return result.ToString();
    }

    private int Hash(string s)
    {
        var currentValue = 0;
        foreach (var character in s)
        {
            currentValue += character;
            currentValue *= 17;
            currentValue %= 256;
        }
        return currentValue;
    }
}
