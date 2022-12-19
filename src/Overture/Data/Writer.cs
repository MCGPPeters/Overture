using Overture.Math.Pure.Algebra.Structure;

namespace Overture.Data;

public record struct Writer<T, TOutput>(T value, TOutput output) where TOutput : Monoid<TOutput>;
