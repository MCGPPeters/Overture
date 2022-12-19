using Overture.Data;
using Overture.Math.Pure.Algebra.Operations;
using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Data.Collections.Generic.Enumerable;

public static class Extensions
{
    /// <summary>
    ///     Generates a sequence of evenly spaced points in the closed interval [start,stop].
    /// </summary>
    public static IEnumerable<double> Sequence(double start, double stop, int length = 100)
    {
        double step = (start - stop) / (length - 1);

        double[]? data = new double[length];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = start + i * step;
        }

        data[data.Length - 1] = stop;
        return data;
    }
}
