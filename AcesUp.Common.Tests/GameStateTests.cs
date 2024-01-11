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

    [Fact]
    public void RemoveLowerRankedCards_Should_Remove_Cards_Until_There_Is_No_Cards_To_Remove()
    {
        // Arrange
        var state = GameState.Create(new[]
        {
            new[] { Rank.Three.OfClubs() },
            new[] { Rank.Five.OfClubs(), Rank.Six.OfClubs() },
            new[] { Rank.Seven.OfClubs() },
            new[] { Rank.Jack.OfClubs() },
        });

        // Act
        state = state.RemoveLowerRankedCards();

        // Assert
        state.Piles[0].Should().BeEmpty();
        state.Piles[1].Should().BeEmpty();
        state.Piles[2].Should().BeEmpty();
        state.Piles[3].Should().OnlyContain(card => card.Equals(Rank.Jack.OfClubs()));
    }

    [Fact]
    public void MoveCardToEmptyPile_Should_Move_Card_From_Big_Enough_Pile()
    {
        // Arrange
        var state = GameState.Create(new[]
        {
            Array.Empty<Card>(),
            new[] { Rank.Two.OfClubs(), Rank.Three.OfClubs(), Rank.Four.OfClubs(), Rank.Five.OfClubs() },
            Array.Empty<Card>(),
            Array.Empty<Card>(),
        });

        // Act
        state = state.MoveCardsToEmptyPile();

        // Assert
        state.Piles.Should().AllSatisfy(pile => pile.Should().ContainSingle());
    }
}