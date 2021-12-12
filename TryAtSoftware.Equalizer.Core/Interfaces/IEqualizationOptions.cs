namespace TryAtSoftware.Equalizer.Core.Interfaces;

using System;
using JetBrains.Annotations;

public interface IEqualizationOptions
{
    [NotNull]
    Type PrincipalType { get; }

    [NotNull]
    Type SubordinateType { get; }

    [NotNull]
    Func<object, object, IEqualizationResult> AssertEquality { get; }
}