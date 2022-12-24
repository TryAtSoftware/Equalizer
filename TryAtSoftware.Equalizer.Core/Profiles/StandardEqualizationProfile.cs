namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

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