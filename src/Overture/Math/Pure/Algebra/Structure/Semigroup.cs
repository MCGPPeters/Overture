namespace Overture.Math.Pure.Algebra.Structure;

public interface Semigroup<A>
{
     abstract Func<A, A, A> Combine { get; }
}
