namespace Overture.Data.Decimal;

using static Overture.Control.Validated.Extensions;

public class Read : Read<decimal>
{
    public Validated<decimal> Parse(string s) =>
        Parse(s, $"The value {s} is not a valid integer");

    public Validated<decimal> Parse(string s, string validationErrorMessage) =>
        decimal.TryParse(s, out decimal i)
            ? Valid(i)
            : Invalid<decimal>(validationErrorMessage);
}
