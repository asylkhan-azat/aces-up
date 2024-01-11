using System.Collections.Immutable;
using FluentAssertions;
using Xunit;

namespace AcesUp.Common.Tests;

public sealed class GameStateTests
{
    [Fact]
    public void Same_Game_States_Should_Be_Equal()
    {
        // Arrange
        var lhs = new GameState(
            new[]
            {
                ImmutablePile.Empty,
                ImmutablePile.Empty.Push(Rank.Ace.OfClubs()),
                ImmutablePile.Empty.Push(Rank.Eight.OfHearts()),
                ImmutablePile.Empty,
            }.ToImmutableArray());

        var rhs = new GameState(
            new[]
            {
                ImmutablePile.Empty,
                ImmutablePile.Empty.Push(Rank.Ace.OfClubs()),
                ImmutablePile.Empty.Push(Rank.Eight.OfHearts()),
                ImmutablePile.Empty,
            }.ToImmutableArray());

        // Assert
        lhs.Should().Be(rhs);
    }
}