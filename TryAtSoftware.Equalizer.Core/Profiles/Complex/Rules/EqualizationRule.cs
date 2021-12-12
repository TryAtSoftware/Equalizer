namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Extensions.Reflection;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationRule<T1, T2> : IEqualizationRule<T1, T2>
{
    private readonly string _expectedValueMemberName;
    private readonly string _actualValueMemberName;

    public EqualizationRule([NotNull] string expectedValueMemberName, [NotNull] string actualValueMemberName)
    {
        if (string.IsNullOrWhiteSpace(expectedValueMemberName))
            throw new ArgumentNullException(nameof(expectedValueMemberName));
        if (string.IsNullOrWhiteSpace(actualValueMemberName))
            throw new ArgumentNullException(nameof(actualValueMemberName));

        this._expectedValueMemberName = expectedValueMemberName;
        this._actualValueMemberName = actualValueMemberName;
    }

    public IEqualizationResult Equalize(T1 expected, T2 actual, IEqualizationOptions options)
    {
        var expectedValueMemberInfo = MembersBinderCache<T1>.Binder.GetRequiredMemberInfo(this._expectedValueMemberName);

        var expectedValue = expectedValueMemberInfo.GetValue(expected);
        var constantEqualizationRule = new ConstantValueEqualizationRule<T1, T2>(expectedValue, this._actualValueMemberName);

        return constantEqualizationRule.Equalize(expected, actual, options);
    }
}