namespace AcesUp.Common;

public class Game
{
    private readonly Pile[] _piles = { new(), new(), new(), new() };

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
        foreach (var pile in _piles)
        {
            pile.Push(deck.Take());
        }
    }

    private void RemoveLowerRankedCards()
    {
        while (TryRemoveOneCard())
        {
        }
    }

    private bool TryRemoveOneCard()
    {
        foreach (var pile in _piles)
        {
            if (pile.TryPeek(out var card) && ThereIsPileWithSameSuitAndHigherRank(card))
            {
                pile.Pop();
                return true;
            }
        }

        return false;
    }
    
    private bool ThereIsPileWithSameSuitAndHigherRank(Card card)
    {
        foreach (var pile in _piles)
        {
            if (pile.TryPeek(out var otherCard) &&
                otherCard.Suit == card.Suit &&
                otherCard.Rank > card.Rank)
            {
                return true;
            }
        }

        return false;
    }

    private bool AreThereMovesToEmptyPiles()
    {
        return _piles.Any(static pile => pile.IsEmpty) &&
               _piles.Any(static pile => pile.Count > 1);
    }

    private void MoveCardToEmptyPile()
    {
        var bigEnoughPile = _piles.First(static pile => pile.Count > 1);
        var emptyPile = _piles.First(static pile => pile.IsEmpty);
        emptyPile.Push(bigEnoughPile.Pop());
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