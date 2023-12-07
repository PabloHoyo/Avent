using System.Runtime.Serialization;

namespace Avent;

internal class CamelCards : Puzzle
{
    public CamelCards() : base("./07/input.txt") { }

    public override string Part1()
    {
        return CalculateWinnings();
    }

    public override string Part2()
    {
        return CalculateWinnings(withJokers: true);
    }

    private string CalculateWinnings(bool withJokers = false)
    {
        var result = 0;
        var hands = new List<CamelHand>();

        foreach (var line in lines)
        {
            var card = line.Split(' ')[0];
            var bid = line.Split(' ')[1];
            hands.Add(new CamelHand(card, int.Parse(bid), withJokers));
        }

        hands.Sort();

        for (var i = 0; i < hands.Count; i++)
        {
            result += hands[i].Bid * (i + 1);
        }
        return result.ToString();
    }
}

internal class CamelHand : IComparable<CamelHand>
{
    private readonly string sortableValue;
    public string Cards { get; init; }
    public int Bid { get; init; }

    public CamelHand(string cards, int bid, bool withJokers)
    {
        var cardsToProcess = cards;

        if (withJokers) 
        {
            var cardsWithoutJoker = cards.Replace("J", "");
            if (cardsWithoutJoker != string.Empty)
            {
                var mostFrequentChar = cardsWithoutJoker.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                cardsToProcess = cards.Replace('J', mostFrequentChar);
            }
        }

        var groups = cardsToProcess.GroupBy(c => c);

        var power = groups.Count() switch
        {
            1 => 7, // Five of a kind
            2 => groups.Any(g => g.Count() == 4)
                ? 6 // Four of a kind
                : 5, // Full house
            3 => groups.Any(g => g.Count() == 3)
                ? 4 // Three of a kind
                : 3, // Two pairs
            4 => 2, // One pair
            _ => 1, // High card
        };

        sortableValue = power.ToString() + cards
            .Replace('T', 'a')
            .Replace('J', withJokers ? '@' : 'b')
            .Replace('Q', 'c')
            .Replace('K', 'd')
            .Replace('A', 'e');

        this.Bid = bid;
        this.Cards = cards;
    }

    public int CompareTo(CamelHand? other)
    {
        return string.Compare(this.sortableValue, other?.sortableValue);
    }
}
