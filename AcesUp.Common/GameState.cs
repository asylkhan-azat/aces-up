using System.Collections.Immutable;

namespace AcesUp.Common;

public readonly struct GameState : IEquatable<GameState>
{
    public GameState(ImmutableArray<ImmutablePile> piles)
    {
        Piles = piles;
    }

    public ImmutableArray<ImmutablePile> Piles { get; }

    public GameState DealNewCards(Deck deck)
    {
        return new GameState(Piles
            .Select(pile => pile.Push(deck.Take()))
            .ToImmutableArray());
    }

    public GameState RemoveLowerRankedCards()
    {
        var newState = TryRemoveLowerRankedCards();

        if (newState.Equals(this))
        {
            return newState;
        }

        return newState.RemoveLowerRankedCards();
    }

    public GameState MoveCardsToEmptyPile()
    {
        if (!Piles.Any(p => p.IsEmpty))
        {
            return this;
        }

        var state = TryTakeCardFromBigPile(out var card);

        if (card.HasValue)
        {
            state = state.AddCardToEmptyPile(card.Value);
        }

        if (state.Equals(this))
        {
            return state;
        }

        return state.MoveCardsToEmptyPile();
    }

    public static GameState CreateNew()
    {
        return new GameState(Enumerable
            .Range(0, 4)
            .Select(_ => ImmutablePile.Empty)
            .ToImmutableArray());
    }

    public static GameState Create(Card[][] piles)
    {
        return new GameState(piles
            .Select(ImmutablePile.Create)
            .ToImmutableArray());
    }

    public bool Equals(GameState other)
    {
        for (var i = 0; i < Piles.Length; i++)
        {
            if (!Piles[i].Equals(other.Piles[i]))
            {
                return false;
            }
        }

        return true;
    }

    private GameState TryTakeCardFromBigPile(out Card? card)
    {
        card = default;

        var piles = Piles.ToArray();

        for (var i = 0; i < piles.Length; i++)
        {
            if (piles[i].Count() > 1)
            {
                card = piles[i].Peek();
                piles[i] = piles[i].Pop();
                break;
            }
        }

        return new GameState(piles.ToImmutableArray());
    }

    private GameState AddCardToEmptyPile(Card card)
    {
        var piles = Piles.ToArray();

        for (var i = 0; i < piles.Length; i++)
        {
            if (piles[i].IsEmpty)
            {
                piles[i] = piles[i].Push(card);
                break;
            }
        }

        return new GameState(piles.ToImmutableArray());
    }

    private GameState TryRemoveLowerRankedCards()
    {
        var piles = Piles.ToArray();

        for (var i = 0; i < piles.Length; i++)
        {
            if (piles[i].IsEmpty) continue;
            if (ThereIsPileWithSameSuitAndHigherRank(piles[i].Peek()))
            {
                piles[i] = piles[i].Pop();
            }
        }

        return new GameState(piles.ToImmutableArray());
    }

    private bool ThereIsPileWithSameSuitAndHigherRank(Card card)
    {
        foreach (var pile in Piles)
        {
            if (pile.IsEmpty) continue;
            if (pile.Peek().Suit == card.Suit && pile.Peek().Rank > card.Rank)
            {
                return true;
            }
        }

        return false;
    }

    public override bool Equals(object? obj)
    {
        return obj is GameState other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Piles.SelectMany(static pile => pile).Aggregate(37, HashCode.Combine);
    }

    public static bool operator ==(GameState left, GameState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GameState left, GameState right)
    {
        return !(left == right);
    }
}