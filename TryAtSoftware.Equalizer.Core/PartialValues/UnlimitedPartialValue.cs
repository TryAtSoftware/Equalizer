namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;

/// <summary>
/// An implementation <see cref="IPartialValue{T}"/> interface that includes all members by default.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public class UnlimitedPartialValue<T> : BasePartialValue<T>
    where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnlimitedPartialValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value that should be assigned to the <see cref="IPartialValue{T}.Value"/> property.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    public UnlimitedPartialValue(T value)
        : base(value)
    {
    }

    /// <inheritdoc />
    public override bool IncludesMember(string memberKey) => true;
}