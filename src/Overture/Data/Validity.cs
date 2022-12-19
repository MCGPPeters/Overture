namespace Overture.Data;

public interface Validity<T>
{
    public abstract Func<string, Func<T, Validated<T>>> Validate { get; }
}
