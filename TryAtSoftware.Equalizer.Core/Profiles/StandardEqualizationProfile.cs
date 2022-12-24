namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// A standard implementation of the <see cref="IEqualizationProfile"/> interface.
/// </summary>
/// <remarks>This equalization profile will check for equality using the <see cref="object.Equals(object)"/> method.</remarks>
public class StandardEqualizationProfile : IEqualizationProfile
{
    /// <inheritdoc />
    public bool CanExecuteFor(object? expected, object? actual) => true;

    /// <inheritdoc />
    public IEqualizationResult Equalize(object? expected, object? actual, IEqualizationOptions options)
    {
        if (Equals(expected, actual)) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));
    }
}