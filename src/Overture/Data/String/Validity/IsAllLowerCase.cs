namespace Overture.Data.String.Validity;

using System;
using static Overture.Control.Validated.Extensions;

public class AllLettersAreLowerCase : Validity<string>
{
    public Func<string, Func<string, Validated<string>>> Validate =>
        name =>
            value =>
                value.Length > 0 && value.All(s => Char.IsLetter(s) ? Char.IsLower(s) : true)
                ? Valid(value)
                : Invalid<string>($"The value of '{name}' may only container alphanumeric characters. Actual value '{value}'");
}
