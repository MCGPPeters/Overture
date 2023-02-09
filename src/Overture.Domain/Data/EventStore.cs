using Overture.Data;
using Overture.Math.Pure.Logic.Order.Intervals;

namespace Overture.Domain.Data;

public interface EventStore<TEventStore>
    where TEventStore : EventStore<TEventStore>
{
    static abstract Task<Result<ExistingVersion, AppendEventsError>> AppendEvents<TEvent>(string streamName, Version expectedVersion,
        params TEvent[] events);

    static abstract IAsyncEnumerable<Event<TEvent>> GetEvents<TEvent>(string streamName, Closed<Version> interval);
}
