namespace TryAtSoftware.Equalizer.Core;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationOptions : IEqualizationOptions
{
    public EqualizationOptions([NotNull] Type principalType, [NotNull] Type subordinateType, [NotNull] Func<object, object, IEqualizationResult> assertEquality)
    {
        this.PrincipalType = principalType ?? throw new ArgumentNullException(nameof(principalType));
        this.SubordinateType = subordinateType ?? throw new ArgumentNullException(nameof(subordinateType));
        this.AssertEquality = assertEquality ?? throw new ArgumentNullException(nameof(assertEquality));
    }

    public Type PrincipalType { get; }
    public Type SubordinateType { get; }
    public Func<object, object, IEqualizationResult> AssertEquality { get; }
}