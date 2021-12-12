namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Extensions.Reflection;

public class ConstantValueEqualizationRule<T1, T2> : IEqualizationRule<T1, T2>
{
    private readonly object _expectedValue;
    private readonly string _actualValueMemberName;

    public ConstantValueEqualizationRule([CanBeNull] object expectedValue, [NotNull] string actualValueMemberName)
    {
        if (string.IsNullOrWhiteSpace(actualValueMemberName))
            throw new ArgumentNullException(nameof(actualValueMemberName));

        this._expectedValue = expectedValue;
        this._actualValueMemberName = actualValueMemberName;
    }

    public IEqualizationResult Equalize(T1 expected, T2 actual, IEqualizationOptions options)
    {
        var actualValueMemberInfo = MembersBinderCache<T2>.Binder.GetRequiredMemberInfo(this._actualValueMemberName);
        var actualValue = actualValueMemberInfo.GetValue(actual);

        return options.AssertEquality(this._expectedValue, actualValue);
    }
}