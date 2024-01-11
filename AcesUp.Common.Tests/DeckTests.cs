using Xunit;

namespace AcesUp.Common.Tests
{
    public class DeckTests
    {
        [Fact]
        public void ShuffledDeckContains52UniqueCards()
        {
            var random = new Random(1234);
            var deck = Deck.CreateShuffledDeck(random);

            var seenCards = new HashSet<Card>();
            while (!deck.IsEmpty)
            {
                var card = deck.Take();
                if (seenCards.Contains(card))
                {
                    Assert.Fail($"The card {card} was not unique.");
                }

                seenCards.Add(card);
            }

            Assert.Equal(52, seenCards.Count);
        }
    }
}