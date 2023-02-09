using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Math.Pure.Numbers.ℤ;

public class Multiplication : Monoid<int>, Algebra.Operations.Multiplication
{
    public static int Identity => 1;

    public static Func<int, int, int> Combine => (x, y) => x * y;
}
