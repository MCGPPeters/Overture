namespace Overture.Math.Pure.Geometry.Topology;

public delegate double Distance<in T, TArguments>(T x) where T : DistanceArguments<TArguments>;
