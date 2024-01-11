using System.Collections;
using System.Collections.Immutable;

namespace AcesUp.Common;

public sealed class Pile : IEnumerable<Card>, IEquatable<Pile>
{
    public static readonly Pile Empty = new(ImmutableStack<Card>.Empty);

    private readonly ImmutableStack<Card> _innerValue;

    public Pile(ImmutableStack<Card> innerValue)
    {
        _innerValue = innerValue;
    }

    public bool IsEmpty => _innerValue.IsEmpty;

    public Card Peek()
    {
        return _innerValue.Peek();
    }

    public Pile Pop()
    {
        return new Pile(_innerValue.Pop());
    }

    public Pile Push(Card card)
    {
        return new Pile(_innerValue.Push(card));
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

    public static Pile Create(Card[] cards)
    {
        return new Pile(ImmutableStack.Create(cards));
    }

    public bool Equals(Pile? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _innerValue.SequenceEqual(other._innerValue);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Pile other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _innerValue.GetHashCode();
    }
}