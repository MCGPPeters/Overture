namespace Overture.Data.String.Validity;

using System;
using static Overture.Control.Validated.Extensions;

public class StartsWithALetter : Validity<string>
{
    public Func<string, Func<string, Validated<string>>> Validate =>
        name =>
            value =>
                value.Length > 0 && Char.IsLetter(value[0])
                ? Valid(value)
                : Invalid<string>($"The first character of '{name}' must be a letter. Actual value '{value}'");
}
