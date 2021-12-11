namespace TryAtSoftware.Equalizer.Core;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationOptions : IEqualizationOptions
{
    public EqualizationOptions([NotNull] Type principalType, [NotNull] Type actualType, [NotNull] Action<object, object> assertEquality)
    {
        this.PrincipalType = principalType ?? throw new ArgumentNullException(nameof(principalType));
        this.ActualType = actualType ?? throw new ArgumentNullException(nameof(actualType));
        this.AssertEquality = assertEquality ?? throw new ArgumentNullException(nameof(assertEquality));
    }

    public Type PrincipalType { get; }
    public Type ActualType { get; }
    public Action<object, object> AssertEquality { get; }
}