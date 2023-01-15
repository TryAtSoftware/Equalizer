namespace TryAtSoftware.Equalizer.Core.PartialValues;

using System;

/// <summary>
/// A static class used to make the syntax when using "partial values" more readable.
/// </summary>
public static class PartialValueExtensions
{
    /// <summary>
    /// Use this method to construct an <see cref="IPartialValue{T}"/> representation for the extended <paramref name="instance"/> including all members.
    /// </summary>
    /// <param name="instance">The extended instance for which a partial representation should be constructed.</param>
    /// <typeparam name="T">The type of the extended <paramref name="instance"/>.</typeparam>
    /// <returns>Returns a subsequently constructed partial representation for the extended <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the extended <paramref name="instance"/> is null.</exception>
    public static IPartialValue<T> AsPartialValue<T>(this T instance)
        where T : notnull
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        return new UnlimitedPartialValue<T>(instance);
    }

    /// <summary>
    /// Use this method to construct an <see cref="IPartialValue{T}"/> representation for the extended <paramref name="instance"/> that includes all members but the specified ones (<paramref name="membersToExclude"/>).
    /// </summary>
    /// <param name="instance">The extended instance for which a partial representation should be constructed.</param>
    /// <param name="membersToExclude">A collection of the member keys that should be excluded from the subsequently built representation.</param>
    /// <typeparam name="T">The type of the extended <paramref name="instance"/>.</typeparam>
    /// <returns>Returns a subsequently constructed partial representation for the extended <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the extended <paramref name="instance"/> is null.</exception>
    public static IPartialValue<T> Exclude<T>(this T instance, params string[] membersToExclude)
        where T : notnull
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        return new ExclusivePartialValue<T>(instance, membersToExclude);
    }
    
    /// <summary>
    /// Use this method to construct an <see cref="IPartialValue{T}"/> representation for the extended <paramref name="instance"/> that includes only the specified members (<paramref name="membersToInclude"/>).
    /// </summary>
    /// <param name="instance">The extended instance for which a partial representation should be constructed.</param>
    /// <param name="membersToInclude">A collection of the member keys that should be included within the subsequently built representation.</param>
    /// <typeparam name="T">The type of the extended <paramref name="instance"/>.</typeparam>
    /// <returns>Returns a subsequently constructed partial representation for the extended <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the extended <paramref name="instance"/> is null.</exception>
    public static IPartialValue<T> Include<T>(this T instance, params string[] membersToInclude)
        where T : notnull
    {
        if (instance is null) throw new ArgumentNullException(nameof(instance));
        return new InclusivePartialValue<T>(instance, membersToInclude);
    }
}