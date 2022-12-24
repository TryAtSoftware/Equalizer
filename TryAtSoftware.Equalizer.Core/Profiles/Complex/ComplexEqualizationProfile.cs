namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

public class ComplexEqualizationProfile<TExpected, TActual> : BaseTypedEqualizationProfile<TExpected, TActual>
{
    private readonly List<IEqualizationRule<TExpected, TActual>> _rules = new();

    protected sealed override bool AllowNullExpected => false;
    protected sealed override bool AllowNullActual => false;

    protected override IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return new SuccessfulEqualizationResult();
        if (expected is null || actual is null) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        foreach (var rule in this._rules)
        {
            var equalizationResult = rule.Equalize(expected, actual, options);
            if (equalizationResult.IsSuccessful) continue;

            var errorMessage = this.UnsuccessfulEqualization(expected, actual);
            return new UnsuccessfulEqualizationResult(errorMessage.With(equalizationResult));
        }

        return new SuccessfulEqualizationResult();
    }

    protected void Equalize(Func<TExpected, object?> expectedValueSelector, Func<TActual, object?> actualValueSelector)
    {
        var rule = new EqualizationRule<TExpected, TActual>(expectedValueSelector, actualValueSelector);
        this.AddRule(rule);
    }

    protected void Equalize<TValue>(TValue? value, Func<TActual, object?> actualValueSelector) => this.Equalize(_ => value, actualValueSelector);

    protected void Differentiate(Func<TExpected, object?> expectedValueSelector, Func<TActual, object?> actualValueSelector)
    {
        var rule = new DifferentiationRule<TExpected, TActual>(expectedValueSelector, actualValueSelector);
        this.AddRule(rule);
    }

    protected void Differentiate<TValue>(TValue? value, Func<TActual, object?> actualValueSelector) => this.Differentiate(_ => value, actualValueSelector);

    protected void Extend(ComplexEqualizationProfile<TExpected, TActual> commonProfile)
    {
        if (commonProfile is null) throw new ArgumentNullException(nameof(commonProfile));
        foreach (var commonProfileRule in commonProfile._rules) this.AddRule(commonProfileRule);
    }

    private void AddRule(IEqualizationRule<TExpected, TActual> equalizationRule)
    {
        if (equalizationRule is null) throw new ArgumentNullException(nameof(equalizationRule));
        this._rules.Add(equalizationRule);
    }
}