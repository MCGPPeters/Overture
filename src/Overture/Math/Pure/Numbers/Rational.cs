﻿using Overture.Data;
using Overture.Math.Pure.Numbers.ℚ;
using static Overture.Control.Result.Extensions;

namespace Overture.Math.Pure.Numbers;

public record Rational : Number
{

    internal Rational(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public static Result<Rational, Error> Create(int numerator, int denominator)
        => denominator switch
        {
            > 0 => Ok<Rational, Error>(new Rational(numerator, denominator)),
            _ => Error<Rational, Error>($"The {nameof(denominator)} must be greater then zero")
        };

    public int Numerator { get; }

    public int Denominator { get; }

    public static Rational operator *(Rational x, Rational y) =>
        Multiplication.Combine(x, y);

    public static Rational operator -(Rational x, Rational y)
    {
        Rational inverse = Addition.Invert(y);
        return Addition.Combine(x, inverse);
    }

    public static Rational operator /(Rational x, Rational y)
    {
        Rational inverse = Multiplication.Invert(y);
        return Multiplication.Combine(x, inverse);
    }

    public static Rational operator +(Rational x, Rational y) =>
        Addition.Combine(x, y);

    /// <summary>
    /// Additive inverse
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Rational operator -(Rational x) =>
        Addition.Invert(x);
}
