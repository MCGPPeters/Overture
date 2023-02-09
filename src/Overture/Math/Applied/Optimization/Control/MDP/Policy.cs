using Overture.Math.Applied.Probability;

namespace Overture.Math.Applied.Optimization.Control.MDP;

/// <summary>
/// A stochastic policy is a mapping from states to the probabilities of selecting each possible action
/// </summary>
/// <typeparam name="S"></typeparam>
/// <typeparam name="A"></typeparam>
/// <param name="state"></param>
/// <returns></returns>
public delegate Distribution<A> Policy<in S, A>(S state) where A : notnull;
