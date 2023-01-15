namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;

public static class PartialValueExtensions
{
    public static IPartialValue<T> AsPartialValue<T>(this T instance)
        where T : notnull
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        return new UnlimitedPartialValue<T>(instance);
    }

    public static IPartialValue<T> Exclude<T>(this T instance, params string[] membersToExclude)
        where T : notnull
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        return new ExclusivePartialValue<T>(instance, membersToExclude);
    }
    
    public static IPartialValue<T> Include<T>(this T instance, params string[] membersToInclude)
        where T : notnull
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        return new InclusivePartialValue<T>(instance, membersToInclude);
    }
}