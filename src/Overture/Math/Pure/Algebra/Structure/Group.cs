namespace Overture.Math.Pure.Algebra.Structure;

public interface Group<A> : Monoid<A>
{
    abstract Func<A, A> Invert { get; }
}
