namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Extensions.Collections;

/// <summary>
/// An implementation <see cref="IPartialValue{T}"/> interface that includes only a predefined set of members.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public class InclusivePartialValue<T> : BasePartialValue<T>
    where T : notnull
{
    private readonly HashSet<string> _membersToInclude;

    /// <summary>
    /// Initializes a new instance of the <see cref="InclusivePartialValue{T}"/> class.
    /// </summary>
    /// <param name="value">The value that should be set to the <see cref="IPartialValue{T}.Value"/> property.</param>
    /// <param name="membersToInclude">A collection of member keys that should be included within this representation.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    public InclusivePartialValue(T value, params string[] membersToInclude)
        : base(value)
    {
        this._membersToInclude = membersToInclude.OrEmptyIfNull().IgnoreNullValues().ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override bool IncludesMember(string memberKey) => this._membersToInclude.Contains(memberKey);
}