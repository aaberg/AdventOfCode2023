var stream = File.OpenRead(args[0]);
var input = new StreamReader(stream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n');

var hands = new List<Hand>();

foreach (string line in inputLines)
{
    var cardCharacters = line.Split(' ')[0];
    var bid = int.Parse(line.Split(' ')[1]);
    var cards = cardCharacters.Select(c => new Card(c));
    
    hands.Add(new Hand(cards.ToList(), bid));
}

hands.Sort();
var result = 0L;

for (int i = 1; i <= hands.Count; i++)
{
    result += hands[i-1].Bid * i;
}

Console.WriteLine($"Part one result: {result}");

record Card(char CardCharacter)
{
    public int Number => CardCharacter switch
    {
        'T' => 10,
        'J' => 11,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => int.Parse(CardCharacter.ToString())
    };
}

class Hand : IComparable<Hand>
{
    public Hand(List<Card> cards, int bid)
    {
        Cards = cards;
        Rank = CalculateRank();
        Bid = bid;
    }
    
    public List<Card> Cards { get; }
    public int Rank { get; }
    
    public int Bid { get; }

    private int CalculateRank()
    {
        if (Cards.All(c => c.CardCharacter == Cards.First().CardCharacter))
            return 7; // Five of a kind

        var distinctCards = Cards.Distinct().ToList();
        if (distinctCards.Any(dc => Cards.Count(card => card.CardCharacter == dc.CardCharacter) == 4))
            return 6; // Four of a kind

        if (distinctCards.Any(dc => Cards.Count(card => card.CardCharacter == dc.CardCharacter) == 3) &&
            distinctCards.Any(dc => Cards.Count(card => card.CardCharacter == dc.CardCharacter) == 2))
            return 5; // Full house

        if (distinctCards.Any(dc => Cards.Count(card => card.CardCharacter == dc.CardCharacter) == 3))
        {
            return 4; // three of a kind
        }

        if (distinctCards
                .Select(dc => Cards.Count(c => c.CardCharacter == dc.CardCharacter))
                .Count(i => i == 2) == 2)
        {
            return 3; // Two pairs
        }

        if (distinctCards.Select(dc => Cards.Count(c => c.CardCharacter == dc.CardCharacter)).Any(i => i == 2))
        {
            return 2; // One pair
        }

        return 1; // High card
    }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Rank > other.Rank) return 1;
        if (Rank < other.Rank) return -1;

        for (int i = 0; i < 5; i++)
        {
            var numberCompare = Cards[i].Number.CompareTo(other.Cards[i].Number);
            if (numberCompare != 0) return numberCompare;
        }

        return 0;
    }

    public override string ToString()
    {
        return new string(Cards.Select(c => c.CardCharacter).ToArray());
    }
}