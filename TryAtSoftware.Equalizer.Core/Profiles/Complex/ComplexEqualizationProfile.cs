namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

public class ComplexEqualizationProfile<TPrincipal, TSubordinate> : BaseTypedEqualizationProfile<TPrincipal, TSubordinate>
{
    private readonly List<IEqualizationRule<TPrincipal, TSubordinate>> _rules = new();

    public override IEqualizationResult Equalize(TPrincipal expected, TSubordinate actual, IEqualizationOptions options)
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

    protected void Equalize(Func<TPrincipal, object> expectedValueSelector, Func<TSubordinate, object> actualValueSelector)
    {
        var rule = new EqualizationRule<TPrincipal, TSubordinate>(expectedValueSelector, actualValueSelector);
        this.AddRule(rule);
    }

    protected void Equalize<TValue>(TValue value, Func<TSubordinate, object> actualValueSelector) => this.Equalize(_ => value, actualValueSelector);

    protected void Differentiate(Func<TPrincipal, object> expectedValueSelector, Func<TSubordinate, object> actualValueSelector)
    {
        var rule = new DifferentiationRule<TPrincipal, TSubordinate>(expectedValueSelector, actualValueSelector);
        this.AddRule(rule);
    }

    protected void Differentiate<TValue>(TValue value, Func<TSubordinate, object> actualValueSelector) => this.Differentiate(_ => value, actualValueSelector);

    protected bool AddRule(IEqualizationRule<TPrincipal, TSubordinate> equalizationRule)
    {
        if (equalizationRule is null)
            return false;

        this._rules.Add(equalizationRule);
        return true;
    }
}