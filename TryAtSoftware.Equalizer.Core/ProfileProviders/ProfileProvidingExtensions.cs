namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Extensions.Collections;

/// <summary>
/// A static class containing extension methods that should simplify the equalization profile providing process.
/// </summary>
public static class ProfileProvidingExtensions
{
    /// <summary>
    /// Use this method to get the first <see cref="IEqualizationProfile"/> instance from the extended <paramref name="profiles"/> that can execute for the provided <paramref name="expected"/> and <paramref name="actual"/> values.
    /// </summary>
    /// <param name="profiles">The extended collection of <see cref="IEqualizationProfile"/> instances.</param>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    /// <returns>Returns an <see cref="IEqualizationProfile"/> instance that can execute for the provided <paramref name="expected"/> and <paramref name="actual"/> values or null if no such was found.</returns>
    public static IEqualizationProfile? FirstExecutable(this IEnumerable<IEqualizationProfile> profiles, object? expected, object? actual) => profiles.OrEmptyIfNull().IgnoreNullValues().FirstOrDefault(x => x.CanExecuteFor(expected, actual));
}