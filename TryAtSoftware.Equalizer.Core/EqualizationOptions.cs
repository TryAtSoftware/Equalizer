namespace TryAtSoftware.Equalizer.Core;

using System;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// A standard implementation of the <see cref="IEqualizationResult"/> interface.
/// </summary>
public class EqualizationOptions : IEqualizationOptions
{
    private readonly Func<object?, object?, IEqualizationResult> _equalize;
    private readonly Func<object?, object?, IEqualizationResult> _differentiate;

    /// <summary>
    /// Initializes a new instance of the <see cref="EqualizationOptions"/> class.
    /// </summary>
    /// <param name="expectedType">The value that should be set to the <see cref="ExpectedType"/> property.</param>
    /// <param name="actualType">The value that should be set to the <see cref="ActualType"/> property.</param>
    /// <param name="equalize">A delegate that will be executed whenever the <see cref="Equalize"/> method is called.</param>
    /// <param name="differentiate">A delegate that will be executed whenever the <see cref="Differentiate"/> method is called.</param>
    public EqualizationOptions(Type expectedType, Type actualType, Func<object?, object?, IEqualizationResult> equalize, Func<object?, object?, IEqualizationResult> differentiate)
    {
        this.ExpectedType = expectedType ?? throw new ArgumentNullException(nameof(expectedType));
        this.ActualType = actualType ?? throw new ArgumentNullException(nameof(actualType));
        this._equalize = equalize ?? throw new ArgumentNullException(nameof(equalize));
        this._differentiate = differentiate ?? throw new ArgumentNullException(nameof(differentiate));
    }

    /// <inheritdoc />
    public Type ExpectedType { get; }

    /// <inheritdoc />
    public Type ActualType { get; }

    /// <inheritdoc />
    public IEqualizationResult Equalize(object? expected, object? actual) => this._equalize(expected, actual);

    /// <inheritdoc />
    public IEqualizationResult Differentiate(object? expected, object? actual) => this._differentiate(expected, actual);
}