namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DifferentiationRule<TExpected, TActual> : IEqualizationRule<TExpected, TActual>
{
    private readonly Func<TExpected, object?> _expectedValueRetrieval;
    private readonly Func<TActual, object?> _actualValueRetrieval;

    public DifferentiationRule(Func<TExpected, object?> expectedValueRetrieval, Func<TActual, object?> actualValueRetrieval)
    {
        this._expectedValueRetrieval = expectedValueRetrieval ?? throw new ArgumentNullException(nameof(expectedValueRetrieval));
        this._actualValueRetrieval = actualValueRetrieval ?? throw new ArgumentNullException(nameof(actualValueRetrieval));
    }

    public IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options)
    {
        var expectedValue = this._expectedValueRetrieval(expected);
        var actualValue = this._actualValueRetrieval(actual);

        var equalizationResult = options.Equalize(expectedValue, actualValue);
        if (!equalizationResult.IsSuccessful) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulDifferentiation(expectedValue, actualValue));
    }
}