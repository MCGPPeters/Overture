using Overture.Data;
using Overture.Math.Pure.Logic.Order.Intervals;

namespace Overture.Domain.Data;

[Alias<string>]
public partial record TenantId
{
    public static TenantId Default = (TenantId)"";
}

public interface EventStore<in TEventStore, in TEventStoreSettings>
    where TEventStore : EventStore<TEventStore, TEventStoreSettings>
{
    static abstract Task<Result<ExistingVersion, AppendEventsError>> AppendEvents<TEvent>(TEventStoreSettings eventStoreSettings, TenantId tenantId, Stream stream, Version expectedVersion,
        params TEvent[] events);

    static abstract IAsyncEnumerable<Event<TEvent>> GetEvents<TEvent>(TEventStoreSettings eventStoreSettings, TenantId tenantId, Stream stream, Closed<Version> interval);
}