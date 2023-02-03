namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;

/// <summary>
/// An abstract class that should be inherited by every component willing to implement the <see cref="IPartialValue{T}"/> interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BasePartialValue<T> : IPartialValue<T>
    where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasePartialValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value that should be set to the <see cref="Value"/> property.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    protected BasePartialValue(T value)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <inheritdoc />
    object IPartialValue.Value => this.Value;

    /// <inheritdoc />
    public T Value { get; }

    /// <inheritdoc />
    public abstract bool IncludesMember(string memberKey);
}