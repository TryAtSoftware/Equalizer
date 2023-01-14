namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Extensions.Collections;

public class PartialValue<T>
{
    private readonly HashSet<string> _membersToIgnore;

    public PartialValue(T value, params string[] membersToIgnore)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
        this._membersToIgnore = membersToIgnore.OrEmptyIfNull().IgnoreNullValues().ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    public T Value { get; }

    public bool IncludesMember(string memberKey) => !string.IsNullOrWhiteSpace(memberKey) && !this._membersToIgnore.Contains(memberKey);
}