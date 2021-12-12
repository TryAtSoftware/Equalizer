namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationRule<TPrincipal, TSubordinate> : IEqualizationRule<TPrincipal, TSubordinate>
{
    [NotNull]
    private readonly Func<TPrincipal, object> _expectedValueRetrieval;

    [NotNull]
    private readonly Func<TSubordinate, object> _actualValueRetrieval;

    public EqualizationRule([NotNull] Func<TPrincipal, object> expectedValueRetrieval, [NotNull] Func<TSubordinate, object> actualValueRetrieval)
    {
        this._expectedValueRetrieval = expectedValueRetrieval ?? throw new ArgumentNullException(nameof(expectedValueRetrieval));
        this._actualValueRetrieval = actualValueRetrieval ?? throw new ArgumentNullException(nameof(actualValueRetrieval));
    }

    public IEqualizationResult Equalize(TPrincipal expected, TSubordinate actual, IEqualizationOptions options)
    {
        var expectedValue = this._expectedValueRetrieval(expected);
        var actualValue = this._actualValueRetrieval(actual);

        return options.Equalize(expectedValue, actualValue);
    }
}