﻿using System.Collections.Immutable;

namespace AcesUp.Common;

public readonly struct GameState : IEquatable<GameState>
{
    public GameState(ImmutableArray<ImmutablePile> piles)
    {
        Piles = piles;
    }

    public ImmutableArray<ImmutablePile> Piles { get; }

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