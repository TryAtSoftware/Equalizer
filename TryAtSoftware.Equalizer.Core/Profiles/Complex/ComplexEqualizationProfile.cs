namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;
using TryAtSoftware.Extensions.Reflection;

public class ComplexEqualizationProfile<T1, T2> : BaseTypedEqualizationProfile<T1, T2>
{
    private readonly List<IEqualizationRule<T1, T2>> _rules = new();

    public override IEqualizationResult Equalize(T1 expected, T2 actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return new SuccessfulEqualizationResult();
        if (expected is null || actual is null) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        foreach (var rule in this._rules)
        {
            var equalizationResult = rule.Equalize(expected, actual, options);
            if (!equalizationResult.IsSuccessful)
                return equalizationResult;
        }

        return new SuccessfulEqualizationResult();
    }

    protected void Equalize(Expression<Func<T1, object>> expectedValueExpressionSelector, Expression<Func<T2, object>> actualValueExpressionSelector)
    {
        var expectedValueMemberInfo = expectedValueExpressionSelector.GetMemberInfo();
        var actualValueMemberInfo = actualValueExpressionSelector.GetMemberInfo();
        var rule = new EqualizationRule<T1, T2>(expectedValueMemberInfo.Name, actualValueMemberInfo.Name);
        this.AddRule(rule);
    }

    protected void Equalize<TValue>(TValue value, Expression<Func<T2, object>> actualValueExpressionSelector)
    {
        var actualValueMemberInfo = actualValueExpressionSelector.GetMemberInfo();
        var rule = new ConstantValueEqualizationRule<T1, T2>(value, actualValueMemberInfo.Name);
        this.AddRule(rule);
    }

    protected void Differentiate(Expression<Func<T1, object>> expectedValueExpressionSelector, Expression<Func<T2, object>> actualValueExpressionSelector)
    {
        var expectedValueMemberInfo = expectedValueExpressionSelector.GetMemberInfo();
        var actualValueMemberInfo = actualValueExpressionSelector.GetMemberInfo();
        var rule = new DifferentiationRule<T1, T2>(expectedValueMemberInfo.Name, actualValueMemberInfo.Name);
        this.AddRule(rule);
    }

    protected void Differentiate<TValue>(TValue value, Expression<Func<T2, object>> actualValueExpressionSelector)
    {
        var actualValueMemberInfo = actualValueExpressionSelector.GetMemberInfo();
        var rule = new ConstantValueDifferentiationRule<T1, T2>(value, actualValueMemberInfo.Name);
        this.AddRule(rule);
    }

    protected bool AddRule(IEqualizationRule<T1, T2> equalizationRule)
    {
        if (equalizationRule is null)
            return false;

        this._rules.Add(equalizationRule);
        return true;
    }
}