namespace TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An interface defining the structure of a component responsible for providing an appropriate <see cref="IEqualizationProfile"/> instance for each such request.
/// </summary>
public interface IEqualizationProfileProvider
{
    /// <summary>
    /// Use this method in order to get an <see cref="IEqualizationProfile"/> instance that is suitable for validation the equality between <paramref name="expected"/> and <paramref name="actual"/>.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    /// <returns>Returns an <see cref="IEqualizationProfile"/> instance that meets the described criteria. Null may be returned if none of the internally registered profiles can be used appropriately.</returns>
    IEqualizationProfile? GetProfile(object? expected, object? actual);
}