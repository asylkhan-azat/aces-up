namespace AcesUp.Common;

public readonly record struct Card(Rank Rank, Suit Suit)
{
    private static readonly string[] RankStrings = { " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", " J", " Q", " K", " A" };

    private static readonly string[] SuitStrings = { "♣", "♦", "♥", "♠" };

    public override string ToString()
    {
        return $"{RankStrings[(int)Rank]}-{SuitStrings[(int)Suit]}";
    }
}