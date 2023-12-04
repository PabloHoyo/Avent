namespace Avent;

internal class Scratchcards : Puzzle, IPuzzle
{
    public Scratchcards() : base("./4/input.txt") { }

    public int Part1()
    {
        var result = 0;
        foreach (var line in lines)
        {
            var winningNumbers = line.Split(':')[1].Split('|')[0].Trim().Split(' ').Where(s => s != string.Empty);
            var playingNumbers = line.Split(':')[1].Split('|')[1].Trim().Split(' ').Where(s => s != string.Empty);
            var matches = winningNumbers.Intersect(playingNumbers);
            result += matches.Any() ? (int)Math.Pow(2, matches.Count() - 1) : 0;
        }
        return result;
    }

    public int Part2()
    {
        var result = 0;
        var matchCounters = new Dictionary<int, int>(); // [cardId, matchesCount]
        var copiesToProcess = new Dictionary<int, int>(); // [cardId, numberOfCopies]
        foreach (var line in lines)
        {
            var cardId = int.Parse(line.Split(':')[0].Split(' ').Where(s => s != string.Empty).Last());
            var winningNumbers = line.Split(':')[1].Split('|')[0].Trim().Split(' ').Where(s => s != string.Empty);
            var playingNumbers = line.Split(':')[1].Split('|')[1].Trim().Split(' ').Where(s => s != string.Empty);
            matchCounters[cardId] = winningNumbers.Intersect(playingNumbers).Count();
            copiesToProcess[cardId] = 1;
            result++;
        }

        foreach (var cardId in copiesToProcess.Keys) 
        { 
            ProcessCard(cardId);
        }

        return result;

        void ProcessCard(int cardId)
        {
            if (!matchCounters.ContainsKey(cardId)) return;

            var matches = matchCounters[cardId];
            for (int i = 1; i <= matches; i++)
            {
                result ++;
                ProcessCard(cardId + i);
            }
        }
    }
}