namespace AcesUp.Common;

public class Game
{
    private GameState _state = GameState.CreateNew();

    public void RunAllSteps(Deck deck)
    {
        _state = _state.DealNewCards(deck);

        var currentState = _state;
        var newState = currentState
            .RemoveLowerRankedCards()
            .MoveCardsToEmptyPile();

        while (!newState.Equals(currentState))
        {
            currentState = newState;
            newState = currentState.RemoveLowerRankedCards().MoveCardsToEmptyPile();
        }

        _state = newState;
    }

    public bool IsGameWon()
    {
        foreach (var pile in _state.Piles)
        {
            if (pile.Count() != 1)
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
            Console.WriteLine($"Pile {i}: {_state.Piles[i]}");
        }

        Console.WriteLine();
    }
}