namespace AcesUp.Common;

public class Game
{
    private readonly Pile[] _piles = new Pile[] { new(), new(), new(), new() };

    public void RunAllSteps(Deck deck)
    {
        DealNewCards(deck);
        RemoveLowerRankedCards();
        while (AreThereMovesToEmptyPiles())
        {
            MoveCardToEmptyPile();
            RemoveLowerRankedCards();
        }
    }

    private void DealNewCards(Deck deck)
    {
        // Add code here
    }

    private void RemoveLowerRankedCards()
    {
        // Add code here
    }

    private bool AreThereMovesToEmptyPiles()
    {
        // Add code here

        return false;
    }

    private void MoveCardToEmptyPile()
    {
        // Add code here
    }

    public bool IsGameWon()
    {
        foreach (var pile in _piles)
        {
            if (pile.Count != 1)
            {
                return false;
            }

            if (pile.Peek().Rank != Rank.Ace)
            {
                return false;
            }
        }

        return true;
    }

    public void PrintPiles()
    {
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"Pile {i}: {_piles[i]}");
        }

        Console.WriteLine();
    }

}