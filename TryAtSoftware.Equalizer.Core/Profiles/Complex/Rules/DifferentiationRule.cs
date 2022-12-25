namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IComplexEqualizationRule{TExpected,TActual}"/> interface responsible for validating the inequality between two segments of the equalized complex objects.
/// </summary>
/// <typeparam name="TExpected">The type of the expected value.</typeparam>
/// <typeparam name="TActual">The type of the actual value.</typeparam>
public class DifferentiationRule<TExpected, TActual> : IComplexEqualizationRule<TExpected, TActual>
{
    private readonly Func<TExpected, object?> _expectedValueSelector;
    private readonly Func<TActual, object?> _actualValueSelector;

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentiationRule{TExpected,TActual}"/> class.
    /// </summary>
    /// <param name="expectedValueSelector">A function selecting the expected value.</param>
    /// <param name="actualValueSelector">A function selecting the actual value.</param>
    public DifferentiationRule(Func<TExpected, object?> expectedValueSelector, Func<TActual, object?> actualValueSelector)
    {
        this._expectedValueSelector = expectedValueSelector ?? throw new ArgumentNullException(nameof(expectedValueSelector));
        this._actualValueSelector = actualValueSelector ?? throw new ArgumentNullException(nameof(actualValueSelector));
    }

    /// <inheritdoc />
    public IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options)
    {
        var expectedValue = this._expectedValueSelector(expected);
        var actualValue = this._actualValueSelector(actual);

        var equalizationResult = options.Equalize(expectedValue, actualValue);
        if (!equalizationResult.IsSuccessful) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulDifferentiation(expectedValue, actualValue));
    }
}