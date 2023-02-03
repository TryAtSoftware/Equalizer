namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Extensions.Collections;

/// <summary>
/// An implementation <see cref="IPartialValue{T}"/> interface that includes all members but a predefined set of excluded ones.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public class ExclusivePartialValue<T> : BasePartialValue<T>
    where T : notnull
{
    private readonly HashSet<string> _membersToExclude;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExclusivePartialValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value that should be set to the <see cref="IPartialValue{T}.Value"/> property.</param>
    /// <param name="membersToExclude">A collection of member keys that should not be included within this representation.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    public ExclusivePartialValue(T value, params string[] membersToExclude)
        : base(value)
    {
        this._membersToExclude = membersToExclude.OrEmptyIfNull().IgnoreNullValues().ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override bool IncludesMember(string memberKey) => !this._membersToExclude.Contains(memberKey);
}