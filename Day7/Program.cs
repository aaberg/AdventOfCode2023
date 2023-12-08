var stream = File.OpenRead(args[0]);
var input = new StreamReader(stream).ReadToEnd();
var inputLines = input.Replace("\r", "").Split('\n');

var jackHands = new List<Hand>();
var jokerHands = new List<Hand>();

foreach (string line in inputLines)
{
    var cardCharacters = line.Split(' ')[0];
    var bid = int.Parse(line.Split(' ')[1]);
    var cards = cardCharacters.Select(c => new Card(c)).ToList();
    
    jackHands.Add(new Hand(cards, bid, new JsAreJackStrategy()));
    jokerHands.Add(new Hand(cards, bid, new JsAsJokersStrategy()));
}

jackHands.Sort();
jokerHands.Sort();
var resultPart1 = 0L;
var resultPart2 = 0L;

for (int i = 1; i <= jackHands.Count; i++)
{
    resultPart1 += jackHands[i-1].Bid * i;
    resultPart2 += jokerHands[i-1].Bid * i;
}

Console.WriteLine($"Part one result: {resultPart1}");
Console.WriteLine($"Part two result: {resultPart2}");

record Card(char CardCharacter);

class Hand : IComparable<Hand>
{
    private IHandStrategy _handStrategy;
    
    public Hand(List<Card> cards, int bid, IHandStrategy handStrategy)
    {
        Cards = cards;
        Rank = handStrategy.GetRank(cards);
        Bid = bid;
        _handStrategy = handStrategy;
    }
    
    public List<Card> Cards { get; }
    public int Rank { get; }
    
    public int Bid { get; }

    public int CompareTo(Hand? other)
    {
        if (other == null)
            return 1;
        
        if (Rank > other.Rank) return 1;
        if (Rank < other.Rank) return -1;

        for (int i = 0; i < 5; i++)
        {
            var numberCompare = _handStrategy.GetCardValue(Cards[i]).CompareTo(_handStrategy.GetCardValue(other.Cards[i]));
            if (numberCompare != 0) return numberCompare;
        }
        return 0;
    }

    public override string ToString()
    {
        return new string(Cards.Select(c => c.CardCharacter).ToArray());
    }
}

interface IHandStrategy
{
    int GetRank(List<Card> cards);
    
    int GetCardValue(Card card);
}

class JsAreJackStrategy : IHandStrategy
{
    public int GetRank(List<Card> cards)
    {
        if (cards.All(c => c.CardCharacter == cards.First().CardCharacter))
            return 7; // Five of a kind

        var distinctCards = cards.Distinct().ToList();
        if (distinctCards.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) == 4))
            return 6; // Four of a kind

        if (distinctCards.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) == 3) &&
            distinctCards.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) == 2))
            return 5; // Full house

        if (distinctCards.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) == 3))
        {
            return 4; // three of a kind
        }

        if (distinctCards
                .Select(dc => cards.Count(c => c.CardCharacter == dc.CardCharacter))
                .Count(i => i == 2) == 2)
        {
            return 3; // Two pairs
        }

        if (distinctCards.Select(dc => cards.Count(c => c.CardCharacter == dc.CardCharacter)).Any(i => i == 2))
        {
            return 2; // One pair
        }
        return 1; // High card
    }

    public int GetCardValue(Card card) =>
        card.CardCharacter switch
        {
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => int.Parse(card.CardCharacter.ToString())
        };
}

class JsAsJokersStrategy : IHandStrategy
{
    public int GetRank(List<Card> cards)
    {
        var jokers = cards.Count(c => c.CardCharacter == 'J');
        var cardsExceptJokers = cards.Where(c => c.CardCharacter != 'J').ToList();
        
        if (cardsExceptJokers.Count(c => c.CardCharacter == cardsExceptJokers.First().CardCharacter) + jokers == 5)
            return 7; // Five of a kind

        var distinctCardsExceptJokers = cards.Where(c => c.CardCharacter != 'J').Distinct().ToList();
        if (distinctCardsExceptJokers.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) + jokers == 4))
            return 6; // Four of a kind
        
        if (distinctCardsExceptJokers.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) == 3) &&
            distinctCardsExceptJokers.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) == 2))
            return 5; // Full house

        if (distinctCardsExceptJokers
                .Select(dc => cards.Count(c => c.CardCharacter == dc.CardCharacter))
                .Count(i => i == 2) == 2)
        {
            if (jokers == 1)
            {
                return 5; // full house
            }
        }

        if (distinctCardsExceptJokers.Any(dc => cards.Count(card => card.CardCharacter == dc.CardCharacter) + jokers == 3))
        {
            return 4; // three of a kind
        }

        if (distinctCardsExceptJokers
                .Select(dc => cards.Count(c => c.CardCharacter == dc.CardCharacter))
                .Count(i => i == 2) == 2)
        {
            return 3; // Two pairs
        }

        if (distinctCardsExceptJokers.Select(dc => cards.Count(c => c.CardCharacter == dc.CardCharacter)).Any(i => i + jokers == 2))
        {
            return 2; // One pair
        }

        return 1; // High card
    }

    public int GetCardValue(Card card) => card.CardCharacter switch
    {
        'T' => 10,
        'J' => 1,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => int.Parse(card.CardCharacter.ToString())
    };
}