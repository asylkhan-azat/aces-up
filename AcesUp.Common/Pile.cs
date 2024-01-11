namespace AcesUp.Common;

public class Pile : Stack<Card>
{
    public override string ToString()
    {
        return string.Join(", ", this.Reverse().Select(c => c.ToString()));
    }

    public bool IsEmpty => Count == 0;
}