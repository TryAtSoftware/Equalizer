namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DifferentiationRule<TPrincipal, TSubordinate> : IEqualizationRule<TPrincipal, TSubordinate>
{
    [NotNull]
    private readonly Func<TPrincipal, object> _expectedValueRetrieval;

    [NotNull]
    private readonly Func<TSubordinate, object> _actualValueRetrieval;

    public DifferentiationRule([NotNull] Func<TPrincipal, object> expectedValueRetrieval, [NotNull] Func<TSubordinate, object> actualValueRetrieval)
    {
        this._expectedValueRetrieval = expectedValueRetrieval ?? throw new ArgumentNullException(nameof(expectedValueRetrieval));
        this._actualValueRetrieval = actualValueRetrieval ?? throw new ArgumentNullException(nameof(actualValueRetrieval));
    }

    public IEqualizationResult Equalize(TPrincipal principal, TSubordinate subordinate, IEqualizationOptions options)
    {
        var expectedValue = this._expectedValueRetrieval(principal);
        var actualValue = this._actualValueRetrieval(subordinate);

        var equalizationResult = options.Equalize(expectedValue, actualValue);
        if (!equalizationResult.IsSuccessful) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulDifferentiation(expectedValue, actualValue));
    }
}