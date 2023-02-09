namespace Overture.Physics.Mechanics.Mass;

public interface Unit<T> : Math.Pure.Analysis.Measure.Unit<T>
    where T : Literal<T>
{ }
