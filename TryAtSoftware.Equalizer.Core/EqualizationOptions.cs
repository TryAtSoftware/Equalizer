namespace TryAtSoftware.Equalizer.Core;

using System;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class EqualizationOptions : IEqualizationOptions
{
    private readonly Func<object?, object?, IEqualizationResult> _equalize;

    public EqualizationOptions(Type principalType, Type subordinateType, Func<object?, object?, IEqualizationResult> equalize)
    {
        this.PrincipalType = principalType ?? throw new ArgumentNullException(nameof(principalType));
        this.SubordinateType = subordinateType ?? throw new ArgumentNullException(nameof(subordinateType));
        this._equalize = equalize ?? throw new ArgumentNullException(nameof(equalize));
    }

    public Type PrincipalType { get; }
    public Type SubordinateType { get; }
    public IEqualizationResult Equalize(object? expected, object? actual) => this._equalize(expected, actual);
}