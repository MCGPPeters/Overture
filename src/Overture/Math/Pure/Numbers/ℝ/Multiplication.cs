using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Math.Pure.Numbers.ℝ;

public class Multiplication : Field<double>, Algebra.Operations.Multiplication
{
    public static double Identity => 1.0;

    public static Func<double, double, double> Combine => new((x, y) => x * y);

    public static Func<double, double> Invert => a => 1 / a;
}
