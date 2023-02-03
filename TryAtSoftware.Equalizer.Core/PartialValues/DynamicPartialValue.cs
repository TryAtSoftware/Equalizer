namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;

/// <summary>
/// An implementation <see cref="IPartialValue{T}"/> interface that includes members according to a dynamically executed function.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public class DynamicPartialValue<T> : BasePartialValue<T>
    where T : notnull
{
    private readonly Func<string, bool> _includesMember;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicPartialValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value that should be set to the <see cref="IPartialValue{T}.Value"/> property.</param>
    /// <param name="includesMember">A function defining whether or not a requested key should be included within this representation.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="includesMember"/> is null.</exception>
    public DynamicPartialValue(T value, Func<string, bool> includesMember)
        : base(value)
    {
        this._includesMember = includesMember ?? throw new ArgumentNullException(nameof(includesMember));
    }

    /// <inheritdoc />
    public override bool IncludesMember(string memberKey) => this._includesMember(memberKey);
}