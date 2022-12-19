namespace Overture.Data.String.Validity;

using System;
using static Overture.Control.Validated.Extensions;

public class EndsWithALetterOrNumber : Validity<string>
{
    public Func<string, Func<string, Validated<string>>> Validate =>
        name =>
            value =>
                value.Length > 0 && (Char.IsLetter(value[value.Length - 1]) || Char.IsNumber(value[value.Length - 1]))
                ? Valid(value)
                : Invalid<string>($"The last character of '{name}' must be a letter or a number. Actual value '{value}'");
}
