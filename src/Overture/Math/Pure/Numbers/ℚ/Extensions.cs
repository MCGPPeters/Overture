using Overture.Data;
using static Overture.Math.Pure.Numbers.ℤ.Extensions;

namespace Overture.Math.Pure.Numbers.ℚ;

public static class Extensions
{
    public static Func<Rational, Result<Rational, Error>> Reduce =>
        (x) =>
        {
            int d = Gcd(x.Numerator, x.Denominator);
            return Rational.Create(x.Numerator % d, x.Denominator % d);
        };
}
