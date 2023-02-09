using Overture.Math.Pure.Analysis.Measure;
using Overture.Math.Pure.Numbers;

namespace Overture.Physics;

public interface Quantity<out T>
    where T : Unit<T>, Literal<T>
{
    Number Value { get; init; }
};




