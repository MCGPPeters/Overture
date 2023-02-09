using Overture.Data;
using Overture.Math.Applied.Optimization.Control;
using Overture.Math.Applied.Optimization.Control.MDP;

namespace Overture.Math.Applied.Optimization.Control.POMDP;

public record Environment<State, Action, Observation>(Dynamics<State, Action> Dynamics, Reward<Observation> Reward, DiscountFactor γ);

public delegate Reward Reward<Observation>(Observation observation);

public delegate Action Agent<Observation, Action>(Reward<Observation> reward, Observation observation);

public delegate Observation Reset<Observation>();

public delegate Observation Step<State, Observation, Action>(Environment<State, Action, Observation> environment, State state, Action action);

public delegate Unit Render<State, Action, Observation>(Environment<State, Action, Observation> environment);


