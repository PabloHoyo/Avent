namespace Avent;

internal class WaitForIt : Puzzle
{
    public WaitForIt() : base("./06/input.txt") { }

    public override string Part1()
    {
        var times = lines[0].Split(':')[1].Split(' ').Where(x => x != string.Empty).Select(long.Parse);
        var distances = lines[1].Split(':')[1].Split(' ').Where(x => x != string.Empty).Select(long.Parse);
        List<(long time, long distanceRecord)> races = Enumerable.Zip(times, distances).ToList();

        return ProcessRacesV2(races);
    }

    public override string Part2()
    {
        var time = long.Parse(lines[0].Split(':')[1].Replace(" ", ""));
        var distance = long.Parse(lines[1].Split(':')[1].Replace(" ", ""));
        List<(long time, long distanceRecord)> races = new() { (time, distance) };

        return ProcessRacesV2(races);
    }

    private static string ProcessRacesV1(List<(long time, long distanceRecord)> races)
    {
        long result = 1;
        foreach (var (time, distanceRecord) in races)
        {
            long minPushTime = 0;
            for (long pushTime = 0; pushTime < time; pushTime++)
            {
                var distance = (time - pushTime) * pushTime;
                if (distance > distanceRecord)
                {
                    minPushTime = pushTime;
                    break;
                }
            }

            long maxPushTime = 0;
            for (long pushTime = time; pushTime > minPushTime; pushTime--)
            {
                var distance = (time - pushTime) * pushTime;
                if (distance > distanceRecord)
                {
                    maxPushTime = pushTime;
                    break;
                }
            }
            result *= maxPushTime - minPushTime + 1;
        }
        return result.ToString();
    }

    private static string ProcessRacesV2(List<(long time, long distanceRecord)> races)
    {
        long result = 1;
        foreach (var (time, distanceRecord) in races)
        {
            var (min, max) = CalculatePushTimeBounds(time, distanceRecord);
            result *= max - min;
        }
        return result.ToString();
    }

    private static (long min, long max) CalculatePushTimeBounds(long raceTime, long distance)
    {
        var min = (long)(raceTime - Math.Sqrt(Math.Pow(raceTime, 2) - (4 * distance))) / 2;
        var max = (long)(raceTime + Math.Sqrt(Math.Pow(raceTime, 2) - (4 * distance))) / 2;
        return (min, max);
    }
}
