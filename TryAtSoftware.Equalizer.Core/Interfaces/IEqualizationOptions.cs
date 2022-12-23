namespace TryAtSoftware.Equalizer.Core.Interfaces;

using System;

public interface IEqualizationOptions
{
    Type PrincipalType { get; }
    Type SubordinateType { get; }

    IEqualizationResult Equalize(object? expected, object? actual);
}