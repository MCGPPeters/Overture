namespace Overture.Data;

public interface Show<in T>
{
    abstract string Format(T t);

    abstract string Format(T t, string? format, IFormatProvider? provider);

    abstract string Format(T t, Option<string> format, Option<IFormatProvider> provider);
}
