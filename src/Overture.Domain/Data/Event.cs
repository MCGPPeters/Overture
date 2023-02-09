namespace Overture.Domain.Data;

public record Event<TEvent>
{
    public required Version Version { get; init; }
    public required string EventType { get; init; }
    public required TEvent Value { get; init; }
}
