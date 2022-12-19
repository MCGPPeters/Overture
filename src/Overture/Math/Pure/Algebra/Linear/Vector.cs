using System.Collections.ObjectModel;
using Overture.Math.Pure.Algebra.Operations;
using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Math.Pure.Algebra.Linear;

public interface Vector<T, FAdd, FMul>
    where T : Field<T>
    where FAdd : Field<T>, Addition
    where FMul : Field<T>, Multiplication
{
    ReadOnlyCollection<T> Elements { get; }
}
