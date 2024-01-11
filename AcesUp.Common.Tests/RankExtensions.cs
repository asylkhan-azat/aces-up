namespace AcesUp.Common.Tests;

// For easier testing
public static class RankExtensions
{
    public static Card OfSpades(this Rank rank)
    {
        return new Card(rank, Suit.Spades);
    }
    
    public static Card OfClubs(this Rank rank)
    {
        return new Card(rank, Suit.Clubs);
    }
    
    public static Card OfHearts(this Rank rank)
    {
        return new Card(rank, Suit.Hearts);
    }
    
    public static Card OfDiamonds(this Rank rank)
    {
        return new Card(rank, Suit.Diamonds);
    }
}