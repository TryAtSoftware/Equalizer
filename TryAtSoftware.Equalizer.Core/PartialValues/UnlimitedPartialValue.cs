namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;

/// <summary>
/// An implementation <see cref="IPartialValue{T}"/> interface that includes all members by default.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public class UnlimitedPartialValue<T> : IPartialValue<T>
    where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnlimitedPartialValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value that should be assigned to the <see cref="Value"/> property.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    public UnlimitedPartialValue(T value)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <inheritdoc />
    public T Value { get; }

    /// <inheritdoc />
    public bool IncludesMember(string memberKey) => true;
}