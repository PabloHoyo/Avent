namespace Avent;

internal class SeedFertilizer : Puzzle
{
    public SeedFertilizer() : base("./05/input.txt") { }


    private List<List<(long destination, long source, long range)>> converters = new();
    private List<long> seeds = new();
    private List<(long min, long max)> seedRanges = new();

    public override string Part1()
    {
        ParseSeedsAndConverters();

        var seedLocations = new List<(long seed, long location)>();
        foreach (var seed in seeds)
        {
            var lastValue = seed;
            foreach (var converter in converters)
            {
                var match = converter.FirstOrDefault(x => x.source <= lastValue && lastValue < (x.source + x.range));
                if (match == default)
                {
                    continue;
                }
                var offset = lastValue - match.source;
                lastValue = match.destination + offset;
            }
            seedLocations.Add((seed, lastValue));
        }

        return seedLocations.Min(x => x.location).ToString();
    }

    public override string Part2()
    { 
        ParseSeedsAndConverters();

        var lowestLocation = converters.Last().Min(x => x.destination);
        converters.Reverse();
        do
        {
            var lastValue = lowestLocation;
            foreach (var converter in converters)
            {
                var match = converter.FirstOrDefault(x => x.destination <= lastValue && lastValue < (x.destination + x.range));
                if (match == default)
                {
                    continue;
                }
                var offset = lastValue - match.destination;
                lastValue = match.source + offset;
            }

            var seed = seedRanges.FirstOrDefault(x => x.min <= lastValue && lastValue < x.max);
            if (seed != default)
            {
                return lowestLocation.ToString();
            }

            lowestLocation++;
        } 
        while (true);
    }

    private void ParseSeedsAndConverters()
    {
        foreach (var line in lines)
        {
            if (line.StartsWith("seeds:"))
            {
                // Part 1
                seeds = lines.First().Substring(7).Split(' ').Select(long.Parse).ToList();
                // Part 2
                for (var i = 0; i < seeds.Count; i = i+2)
                {
                    seedRanges.Add((min: seeds[i], max: seeds[i] + seeds[i +1]));
                }
            }
            else if (line.EndsWith("map:"))
            {
                converters.Add(new());
            }
            else if (line == string.Empty)
            {
                continue;
            }
            else
            {
                var range = line.Split(' ').Select(long.Parse).ToList();
                converters.Last().Add((range[0], range[1], range[2]));
            }
        }
    }
}
