namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class ComplexEqualizationProfile<T1, T2> : BaseEqualizationProfile<T1, T2>
    where T1 : class
    where T2 : class
{
    private readonly List<IEqualizationRule<T1, T2>> _rules = new();

    public override void AssertEquality(T1 expected, T2 actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return;

        Assert.NotNull(expected, nameof(expected));
        Assert.NotNull(actual, nameof(actual));

        var membersBinder = MembersBinderCache<T2>.Binder;
        foreach (var rule in this._rules)
        {
            Assert.True(membersBinder.MemberInfos.TryGetValue(rule.MemberName, out var memberInfo), $"Member {rule.MemberName} was not found");
            var actualValue = memberInfo.GetValue(actual);
            var expectedValue = rule.ValueProvider.GetValue(expected);

            options.AssertEquality(actualValue, expectedValue);
        }
    }

    protected void Equalize(Expression<Func<T1, object>> expectedValueExpressionSelector, Expression<Func<T2, object>> actualValueExpressionSelector)
    {
        var valueProvider = new ContainedValueProvider<T1>(expectedValueExpressionSelector);
        var rule = new EqualizationRule<T1, T2>(actualValueExpressionSelector, valueProvider);
        this.AddRule(rule);
    }

    protected void Equalize<TValue>(TValue value, Expression<Func<T2, object>> actualValueExpressionSelector)
    {
        var valueProvider = new ConstantValueProvider<T1,TValue>(value);
        var rule = new EqualizationRule<T1, T2>(actualValueExpressionSelector, valueProvider);
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