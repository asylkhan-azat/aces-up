namespace AcesUp.Common;

public class Deck
{
    private readonly Queue<Card> _cards = new();

    private Deck()
    {
    }

    public Card Take() => _cards.Dequeue();

    public bool IsEmpty => _cards.Count == 0;

    public static Deck CreateShuffledDeck(Random r)
    {
        var all = Enumerable.Range(0, 52).Select(a => new Card((Rank)(a % 13), (Suit)(a / 13))).ToArray();

        var deck = new Deck();

        for(var i = 51; i >= 0; i--)
        {
            var pick = r.Next(i + 1);
            deck._cards.Enqueue(all[pick]);
            (all[pick], all[i]) = (all[i], all[pick]);
        }

        return deck;
    }
}