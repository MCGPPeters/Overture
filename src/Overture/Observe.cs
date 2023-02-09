namespace Overture;

public delegate TCommand Observe<in TEvent, out TCommand>(TEvent[] @event);
