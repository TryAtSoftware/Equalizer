namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Extensions.Collections;

public class InclusivePartialValue<T> : IPartialValue<T>
    where T : notnull
{
    private readonly HashSet<string> _membersToInclude;

    public InclusivePartialValue(T value, params string[] membersToInclude)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
        this._membersToInclude = membersToInclude.OrEmptyIfNull().IgnoreNullValues().ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    public T Value { get; }

    public bool IncludesMember(string memberKey) => this._membersToInclude.Contains(memberKey);
}