namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationRule<TExpected, TActual> : IEqualizationRule<TExpected, TActual>
{
    private readonly Func<TExpected, object?> _expectedValueRetrieval;
    private readonly Func<TActual, object?> _actualValueRetrieval;

    public EqualizationRule(Func<TExpected, object?> expectedValueRetrieval, Func<TActual, object?> actualValueRetrieval)
    {
        this._expectedValueRetrieval = expectedValueRetrieval ?? throw new ArgumentNullException(nameof(expectedValueRetrieval));
        this._actualValueRetrieval = actualValueRetrieval ?? throw new ArgumentNullException(nameof(actualValueRetrieval));
    }

    public IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options)
    {
        var expectedValue = this._expectedValueRetrieval(expected);
        var actualValue = this._actualValueRetrieval(actual);

        return options.Equalize(expectedValue, actualValue);
    }
}