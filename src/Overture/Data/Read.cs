namespace Overture.Data;

public interface Read<T>
{
    abstract Validated<T> Parse(string s);

    abstract Validated<T> Parse(string s, string validationErrorMessage);
}
