using Overture.Data.Collections.Generic.List;
using System.Collections;

namespace Overture.Data.Collections.Generic;

public sealed record NonEmptyList<T> : List<T>, IEnumerable
    where T : notnull
{
    public NonEmptyList(T head)
    {
        Head = head;
        Tail = new EmptyList<T>();
    }

    public NonEmptyList(T head, List<T> tail)
    {
        Head = head;
        Tail = tail;
    }

    public T this[int index] => this.ElementAt(index);

    public int Count =>
        this.Count();

    public T Head { get; }
    public List<T> Tail { get; }

    IEnumerator IEnumerable.GetEnumerator() => new NonEmptyListEnumerator<T>(this);
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => new NonEmptyListEnumerator<T>(this);
}
