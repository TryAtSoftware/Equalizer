namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Extensions.Collections;

public class ExclusivePartialValue<T> : IPartialValue<T>
    where T : notnull
{
    private readonly HashSet<string> _membersToExclude;

    public ExclusivePartialValue(T value, params string[] membersToExclude)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
        this._membersToExclude = membersToExclude.OrEmptyIfNull().IgnoreNullValues().ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    public T Value { get; }

    public bool IncludesMember(string memberKey) => !this._membersToExclude.Contains(memberKey);
}