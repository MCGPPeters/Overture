namespace Overture.Math.Pure.Algebra.Structure;

public interface Monoid<A> : Semigroup<A>
{
    abstract A Identity { get; }
}
