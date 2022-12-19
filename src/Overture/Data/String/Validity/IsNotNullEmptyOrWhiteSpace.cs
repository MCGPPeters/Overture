namespace Overture.Data.String.Validity;

using System;
using static Overture.Control.Validated.Extensions;

public class IsNotNullEmptyOrWhiteSpace : Validity<string>
{
    public Func<string, Func<string, Validated<string>>> Validate =>
        name =>
            value =>
                string.IsNullOrWhiteSpace(value)
                ? Invalid<string>($"The value for '{name}' may not be null, empty or whitespace.")
                : Valid(value);
}
