using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Data.String;

public class Concat : Monoid<string>
{
    public static string Identity => "";

    public static Func<string, string, string> Combine => (x, y) => x + y;
}
