namespace TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An interface defining the structure of a component responsible for the equalization of two values.
/// </summary>
public interface IEqualizationProfile
{
    /// <summary>
    /// Use this method to check whether or not the equalization profile can be used to equalize the two values.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    /// <returns>Returns a value indicating whether or not this equalization profile can be used to equalize the provided values.</returns>
    bool CanExecuteFor(object? expected, object? actual);
    
    /// <summary>
    /// Use this method to equalize the <paramref name="expected"/> and <paramref name="actual"/> values.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    /// <param name="options">An <see cref="IEqualizationOptions"/> instance exposing additional information about the equalization process.</param>
    /// <returns></returns>
    IEqualizationResult Equalize(object? expected, object? actual, IEqualizationOptions options);
}