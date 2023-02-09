namespace Overture.Data.Collections.Generic;


public interface List<T> : IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
    where T : notnull
{
    public static List<T> Create(IEnumerable<T> values) =>
        values.Any()
            ? new NonEmptyList<T>(values.First(), List<T>.Create(values.Skip(1)))
            : new EmptyList<T>();
}

