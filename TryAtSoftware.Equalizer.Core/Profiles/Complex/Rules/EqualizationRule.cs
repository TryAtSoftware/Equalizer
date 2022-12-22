namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationRule<TPrincipal, TSubordinate> : IEqualizationRule<TPrincipal, TSubordinate>
{
    private readonly Func<TPrincipal, object?> _expectedValueRetrieval;
    private readonly Func<TSubordinate, object?> _actualValueRetrieval;

    public EqualizationRule(Func<TPrincipal, object?> expectedValueRetrieval, Func<TSubordinate, object?> actualValueRetrieval)
    {
        this._expectedValueRetrieval = expectedValueRetrieval ?? throw new ArgumentNullException(nameof(expectedValueRetrieval));
        this._actualValueRetrieval = actualValueRetrieval ?? throw new ArgumentNullException(nameof(actualValueRetrieval));
    }

    public IEqualizationResult Equalize(TPrincipal principal, TSubordinate subordinate, IEqualizationOptions options)
    {
        var expectedValue = this._expectedValueRetrieval(principal);
        var actualValue = this._actualValueRetrieval(subordinate);

        return options.Equalize(expectedValue, actualValue);
    }
}