namespace Overture.Data;

public interface FromSpan<T>
{
    abstract Validated<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider);
}
