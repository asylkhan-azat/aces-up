using System.Collections;
using System.Collections.Immutable;

namespace AcesUp.Common;

// I really want to have good debugger view on stack of cards but
// I can't override ImmutableStack's ToString because it is sealed. So I have to write this wrapper
// I will replace Pile.cs with this class sooner
public sealed class ImmutablePile : IEnumerable<Card>, IEquatable<ImmutablePile>
{
    public static readonly ImmutablePile Empty = new(ImmutableStack<Card>.Empty);

    private readonly ImmutableStack<Card> _innerValue;

    public ImmutablePile(ImmutableStack<Card> innerValue)
    {
        _innerValue = innerValue;
    }

    public bool IsEmpty => _innerValue.IsEmpty;

    public Card Peek()
    {
        return _innerValue.Peek();
    }

    public ImmutablePile Pop()
    {
        return new ImmutablePile(_innerValue.Pop());
    }

    public ImmutablePile Push(Card card)
    {
        return new ImmutablePile(_innerValue.Push(card));
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return _innerValue.AsEnumerable().GetEnumerator();
    }

    public override string ToString()
    {
        return string.Join(", ", _innerValue.Reverse().Select(c => c.ToString()));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static ImmutablePile Create(Card[] cards)
    {
        return new ImmutablePile(ImmutableStack.Create(cards));
    }

    public bool Equals(ImmutablePile? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _innerValue.SequenceEqual(other._innerValue);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ImmutablePile other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _innerValue.GetHashCode();
    }
}