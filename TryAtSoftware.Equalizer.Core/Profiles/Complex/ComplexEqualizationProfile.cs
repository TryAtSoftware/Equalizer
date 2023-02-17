namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for equalizing complex types.
/// </summary>
/// <typeparam name="TExpected">The type of the expected value.</typeparam>
/// <typeparam name="TActual">The type of the actual value.</typeparam>
public class ComplexEqualizationProfile<TExpected, TActual> : BaseTypedEqualizationProfile<TExpected, TActual>
{
    private readonly List<IComplexEqualizationRule<TExpected, TActual>> _rules = new();

    /// <inheritdoc />
    protected override IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return new SuccessfulEqualizationResult();
        if (expected is null || actual is null) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        foreach (var rule in this._rules)
        {
            var equalizationResult = rule.Equalize(expected, actual, options);
            if (equalizationResult.IsSuccessful) continue;

            var errorMessage = this.UnsuccessfulEqualization(expected, actual);
            return new UnsuccessfulEqualizationResult(errorMessage.WithInner(equalizationResult));
        }

        return new SuccessfulEqualizationResult();
    }

    /// <summary>
    /// Use this method to add an <see cref="IComplexEqualizationRule{TExpected,TActual}"/> validating the equality between the selected expected and actual values.
    /// </summary>
    /// <param name="expectedValueSelector">A function selecting the expected value.</param>
    /// <param name="actualValueSelector">A function selecting the actual value.</param>
    protected void Equalize(Func<TExpected, object?> expectedValueSelector, Func<TActual, object?> actualValueSelector)
    {
        var rule = new EqualizationRule<TExpected, TActual>(expectedValueSelector, actualValueSelector);
        this.AddRule(rule);
    }

    /// <summary>
    /// Use this method to add an <see cref="IComplexEqualizationRule{TExpected,TActual}"/> validating the equality between the expected value and the selected actual value.
    /// </summary>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="actualValueSelector">A function selecting the actual value.</param>
    protected void Equalize<TValue>(TValue? expectedValue, Func<TActual, object?> actualValueSelector) => this.Equalize(_ => expectedValue, actualValueSelector);

    /// <summary>
    /// Use this method to add an <see cref="IComplexEqualizationRule{TExpected,TActual}"/> validating the inequality between the selected expected and actual values.
    /// </summary>
    /// <param name="expectedValueSelector">A function selecting the expected value.</param>
    /// <param name="actualValueSelector">A function selecting the actual value.</param>
    protected void Differentiate(Func<TExpected, object?> expectedValueSelector, Func<TActual, object?> actualValueSelector)
    {
        var rule = new DifferentiationRule<TExpected, TActual>(expectedValueSelector, actualValueSelector);
        this.AddRule(rule);
    }

    /// <summary>
    /// Use this method to add an <see cref="IComplexEqualizationRule{TExpected,TActual}"/> validating the inequality between the expected value and the selected actual value.
    /// </summary>
    /// <param name="expectedValue">The expected value.</param>
    /// <param name="actualValueSelector">A function selecting the actual value.</param>
    protected void Differentiate<TValue>(TValue? expectedValue, Func<TActual, object?> actualValueSelector) => this.Differentiate(_ => expectedValue, actualValueSelector);

    /// <summary>
    /// Use this method to extend this complex equalization profile with some common configuration from the provided <paramref name="commonProfile"/>.
    /// </summary>
    /// <param name="commonProfile">Another <see cref="ComplexEqualizationProfile{TExpected,TActual}"/> instance this one should extend from.</param>
    /// <exception cref="ArgumentNullException">Thrown of the provided <paramref name="commonProfile"/> is null.</exception>
    protected void Extend(ComplexEqualizationProfile<TExpected, TActual> commonProfile)
    {
        if (commonProfile is null) throw new ArgumentNullException(nameof(commonProfile));
        foreach (var commonProfileRule in commonProfile._rules) this.AddRule(commonProfileRule);
    }

    /// <summary>
    /// Use this method to register an external <see cref="IComplexEqualizationRule{TExpected,TActual}"/>.
    /// </summary>
    /// <param name="complexEqualizationRule">The <see cref="IComplexEqualizationRule{TExpected,TActual}"/> instance to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="complexEqualizationRule"/> is null.</exception>
    protected void AddRule(IComplexEqualizationRule<TExpected, TActual> complexEqualizationRule)
    {
        if (complexEqualizationRule is null) throw new ArgumentNullException(nameof(complexEqualizationRule));
        this._rules.Add(complexEqualizationRule);
    }
}