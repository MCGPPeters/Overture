﻿using Overture.Data;
using Overture.Domain.Data.Aggregate;
using Overture.Math.Pure.Logic.Order.Intervals;
using System.Diagnostics.Contracts;

namespace Overture.Domain.Data;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TEvent"></typeparam>
/// <typeparam name="TEventStore"></typeparam>
public record Context<TCommand, TEvent, TEventStore>
    where TEventStore : EventStore<TEventStore>
{
    /// <summary>
    /// Create a new instance of an aggregate, where the aggregate uses specialized events and commands within the context for the aggregate
    /// </summary>
    /// <param name="aggregateId">By default an new Id will be generated, but you can pass a predefined one</param>
    /// <typeparam name="TState">The aggregate type</typeparam>
    /// <typeparam name="TAggregateCommand">The specialized aggregate command type</typeparam>
    /// <typeparam name="TAggregateEvent">The specialized aggregate command type</typeparam>
    /// <returns></returns>
    public async Task<Instance<TState, TCommand, TAggregateCommand, TEvent, TAggregateEvent, TEventStore>> Create<TState, TAggregateCommand, TAggregateEvent>()
        where TState : Aggregate<TState, TAggregateCommand, TAggregateEvent>
        where TAggregateCommand : TCommand
        where TAggregateEvent : TEvent
        =>
            await Get<TState, TAggregateCommand, TAggregateEvent>(this, (Id)Guid.NewGuid(), new MinimumVersion());

    /// <summary>
    /// Create a new instance of an aggregate, where the aggregate uses top level context commands and events
    /// </summary>
    /// <param name="aggregateId">By default an new Id will be generated, but you can pass a predefined one</param>
    /// <typeparam name="TState">The aggregate type</typeparam>
    /// <returns></returns>
    public async Task<Instance<TState, TCommand, TCommand, TEvent, TEvent, TEventStore>> Create<TState>()
        where TState : Aggregate<TState, TCommand, TEvent>
        =>
            await Get<TState, TCommand, TEvent>(this, (Id)Guid.NewGuid(), new MinimumVersion());

    public Task<Instance<TState, TCommand, TAggregateCommand, TEvent, TAggregateEvent, TEventStore>> Get<TState, TAggregateCommand, TAggregateEvent>(Aggregate.Id instanceId)
        where TState : Aggregate<TState, TAggregateCommand, TAggregateEvent>
        where TAggregateCommand : TCommand
        where TAggregateEvent : TEvent
        => Get<TState, TAggregateCommand, TAggregateEvent>(this, instanceId, new AnyVersion());

    public async Task<Instance<TState, TCommand, TAggregateCommand, TEvent, TAggregateEvent, TEventStore>> Get<TState, TAggregateCommand, TAggregateEvent>(
        Context<TCommand, TEvent, TEventStore> context, Id instanceId, Version version)
        where TState : Aggregate<TState, TAggregateCommand, TAggregateEvent>
        where TAggregateCommand : TCommand
        where TAggregateEvent : TEvent

    {
        var state = TState.Create();
        var stream = new Stream { Id = (StreamId)instanceId.Value, Name = (StreamName)TState.Id };
        var events = TEventStore.GetEvents<TAggregateEvent>(stream.ToString(), new Closed<Version>(new MinimumVersion(), version));
        List<TAggregateEvent> history = new();
        state = await events.AggregateAsync(state, (current, @event) =>
        {
            history.Add(@event.Value);
            return TState.Apply(current, @event.Value);
        });

        return new Instance<TState, TCommand, TAggregateCommand, TEvent, TAggregateEvent, TEventStore> { Id = instanceId, State = state, Version = version, History = history, Context = context };
    }

    [Pure]
    internal async Task<Instance<TState, TCommand, TAggregateCommand, TEvent, TAggregateEvent, TEventStore>> Handle<TState, TAggregateCommand, TAggregateEvent>(Instance<TState, TCommand, TAggregateCommand, TEvent, TAggregateEvent, TEventStore> instance, TAggregateCommand command)
        where TState : Aggregate<TState, TAggregateCommand, TAggregateEvent>
        where TAggregateCommand : TCommand
        where TAggregateEvent : TEvent
    {
        var stream = new Stream { Id = (StreamId)instance.Id.Value, Name = (StreamName)TState.Id };
        var ourEvents = TState.Decide(instance.State, command);
        var theirEvents = TEventStore
            .GetEvents<TAggregateEvent>(stream.ToString(), new Closed<Version>(instance.Version, new MaximumVersion()))
            .OrderBy(@event => @event.Version);
        var eventsToAppend = await TState.ResolveConflicts(instance.State, ourEvents, theirEvents).ToArrayAsync();
        var actualState =
            await theirEvents.AggregateAsync(instance.State, (state, @event) => TState.Apply(state, @event.Value));
        var appendEventsResult = await TEventStore.AppendEvents(stream.ToString(),
            await theirEvents.MaxAsync(@event => @event.Version), eventsToAppend);
        switch (appendEventsResult)
        {
            case Error<ExistingVersion, AppendEventsError>(var error):
                return error switch
                {
                    OptimisticConcurrencyError _ =>
                        // just retry, the conflict handling shall deal with this
                        await Handle(instance, command),
                    _ => throw new NotSupportedException()
                };
            case Ok<ExistingVersion, AppendEventsError>(var version):
                {
                    var newState = eventsToAppend.Aggregate(actualState, TState.Apply);
                    return instance with
                    {
                        State = newState,
                        Version = version,
                        History = instance.History.Concat(eventsToAppend)
                    };
                }
            default:
                throw new NotSupportedException();
        }
    }
}
