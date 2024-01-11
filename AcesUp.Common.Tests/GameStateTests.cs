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
    
    [Fact]
    public void DealNewCards_Should_Add_Card_To_Each_Pile()
    {
        // Arrange
        var state = GameState.CreateNew();
        var deck = Deck.CreateWith(
            Rank.Two.OfClubs(),
            Rank.Three.OfClubs(),
            Rank.Four.OfClubs(),
            Rank.Five.OfClubs());

        // Act
        state = state.DealNewCards(deck);

        // Assert
        state.Piles.Should().AllSatisfy(pile => pile.Count().Should().Be(1));
        state.Piles[0].Peek().Should().Be(Rank.Two.OfClubs());
        state.Piles[1].Peek().Should().Be(Rank.Three.OfClubs());
        state.Piles[2].Peek().Should().Be(Rank.Four.OfClubs());
        state.Piles[3].Peek().Should().Be(Rank.Five.OfClubs());
    }
}