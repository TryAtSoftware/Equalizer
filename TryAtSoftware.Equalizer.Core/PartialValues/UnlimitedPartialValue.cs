namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Extensions.Collections;

public class UnlimitedPartialValue<T> : IPartialValue<T>
    where T : notnull
{
    public UnlimitedPartialValue(T value)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public T Value { get; }

    public bool IncludesMember(string memberKey) => true;
}