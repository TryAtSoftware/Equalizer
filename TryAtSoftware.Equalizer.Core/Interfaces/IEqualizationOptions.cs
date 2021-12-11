namespace TryAtSoftware.Equalizer.Core.Interfaces;

using System;
using JetBrains.Annotations;

public interface IEqualizationOptions
{
    [NotNull]
    Type PrincipalType { get; }

    [NotNull]
    Type ActualType { get; }

    [NotNull]
    Action<object, object> AssertEquality { get; }
}