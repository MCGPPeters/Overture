using Overture.Data;
using static Overture.Control.Task.Extensions;

namespace Overture.Control.Validated;

public static class Extensions
{
    public static Validated<T> Valid<T>(T t) => new Valid<T>(t);

    public static Validated<T> Invalid<T>(params Reason[] reasons) => new Invalid<T>(reasons);
    public static Validated<T> Invalid<T>(params string[] reasons) => new Invalid<T>(reasons);
    public static Validated<T> Invalid<T>(params (string title, string description)[] reasons) => new Invalid<T>(reasons.Select(reason => new Reason(reason.title, reason.description)).ToArray());
    public static Validated<T> Invalid<T>(string title, string description) => new Invalid<T>(new Reason(title, description));

    public static Validated<TResult> Bind<T, TResult>(this Validated<T> result, Func<T, Validated<TResult>> function) =>
        result switch
        {
            Valid<T>(var valid) => function(valid),
            Invalid<T>(var reasons) => Invalid<TResult>(reasons),
            _ => throw new NotSupportedException("Unlikely")
        };

    /// <summary>
    ///     For linq syntax support
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="result"></param>
    /// <param name="function"></param>
    /// <returns></returns>
    public static Validated<TResult> SelectMany<T, TResult>(this Validated<T> result, Func<T, Validated<TResult>> function) =>
        result.Bind(function);

    public static Validated<TProjection> SelectMany<T, TResult, TProjection>(this Validated<T> result, Func<T, Validated<TResult>> function,
        Func<T, TResult, TProjection> project) => result switch
        {
            Valid<T>(var valid) => function(valid).Bind(r => Valid(project(valid, r))),
            Invalid<T>(var reasons) => Invalid<TProjection>(reasons),
            _ => throw new NotSupportedException("Unlikely")
        };

    public static Validated<TResult> Map<T, TResult>(this Validated<T> result, Func<T, TResult> function) =>
        result.Bind(x => Valid(function(x)));

    /// <summary>
    ///     For linq syntax support
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="this"></param>
    /// <param name="function"></param>
    /// <returns></returns>
    public static Validated<TResult> Select<T, TResult>(this Validated<T> @this, Func<T, TResult> function) => @this.Map(function);

    public static Validated<TResult> Apply<T, TResult>(this Validated<Func<T, TResult>> fValidated, Validated<T> xValidated) =>
        (fValidated, xValidated) switch
        {
            (Valid<Func<T, TResult>>(var f), Valid<T>(var x)) => Valid(f(x)),
            (Invalid<Func<T, TResult>>(var error), Valid<T>(_)) => Invalid<TResult>(error),
            (Valid<Func<T, TResult>>(_), Invalid<T>(var error)) => Invalid<TResult>(error),
            (Invalid<Func<T, TResult>>(Reason[] reasons), Invalid<T>(Reason[] otherReasons)) => Invalid<TResult>(reasons.Concat(otherReasons).ToArray()),
            _ => throw new NotSupportedException("Unlikely")
        };


    /// <summary>
    /// Convenience for helping the compiler with type inference
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static Func<IEnumerable<T>, T, IEnumerable<T>> Append<T>() => (ts, t) => ts.Append(t);

    public static Task<Validated<R>> Traverse<T, R>(this Validated<T> @this, Func<T, Task<R>> f)
        => @this switch
        {
            Valid<T> (var valid) => f(valid).Map(Valid),
            Invalid<T> (var invalid) => System.Threading.Tasks.Task.FromResult(Invalid<R>(invalid)),
            _ => throw new ArgumentOutOfRangeException(nameof(@this))
        };

    public static Validated<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> values, Func<T, Validated<TResult>> func) =>
        values.
        Aggregate(
            Valid(Enumerable.Empty<TResult>()),
            (emptyList, t) =>
                Valid(Append<TResult>())
                .Apply(emptyList)
                .Apply(func(t)));

    public static Validated<T> Validate<T>(this T t, params Func<T, Validated<T>>[] validators)
        => validators.Traverse(validate => validate(t)).Map(_ => t);

    public static Unit Match<T>(this Validated<T> validated, Action<T> handleOk, Action<Reason[]> handleReasons)
        where T : notnull
        => validated switch
        {
            Valid<T>(var ok) => handleOk.AsFunction()(ok),
            Invalid<T>(var reasons) => handleReasons.AsFunction()(reasons),
            _ => throw new ArgumentOutOfRangeException(nameof(validated))
        };

    public static IEnumerable<T> WhereValid<T>(this IEnumerable<Validated<T>> xs)
    {
        foreach (Validated<T> validated in xs)
        {
            switch (validated)
            {
                case Valid<T>(var valid):
                    yield return valid;
                    break;
                default:
                    continue;
            }

            ;
        }
    }

    public static IEnumerable<Reason[]> WhereNotValid<T>(this IEnumerable<Validated<T>> xs)
    {
        foreach (Validated<T> validated in xs)
        {
            switch (validated)
            {
                case Invalid<T>(var reasons):
                    yield return reasons;
                    break;
                default:
                    continue;
            }

            ;
        }
    }

    public static Validated<Func<T2, R>> Apply<T1, T2, R>
        (this Validated<Func<T1, T2, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.Curry), arg);

    public static Validated<Func<T2, T3, R>> Apply<T1, T2, T3, R>
        (this Validated<Func<T1, T2, T3, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);

    public static Validated<Func<T2, T3, T4, R>> Apply<T1, T2, T3, T4, R>
        (this Validated<Func<T1, T2, T3, T4, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);

    public static Validated<Func<T2, T3, T4, T5, R>> Apply<T1, T2, T3, T4, T5, R>
        (this Validated<Func<T1, T2, T3, T4, T5, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);

    public static Validated<Func<T2, T3, T4, T5, T6, R>> Apply<T1, T2, T3, T4, T5, T6, R>
        (this Validated<Func<T1, T2, T3, T4, T5, T6, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);

    public static Validated<Func<T2, T3, T4, T5, T6, T7, R>> Apply<T1, T2, T3, T4, T5, T6, T7, R>
        (this Validated<Func<T1, T2, T3, T4, T5, T6, T7, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);

    public static Validated<Func<T2, T3, T4, T5, T6, T7, T8, R>> Apply<T1, T2, T3, T4, T5, T6, T7, T8, R>
        (this Validated<Func<T1, T2, T3, T4, T5, T6, T7, T8, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);

    public static Validated<Func<T2, T3, T4, T5, T6, T7, T8, T9, R>> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>
        (this Validated<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>> @this, Validated<T1> arg) => Apply(@this.Map(Prelude.CurryFirst), arg);
}
