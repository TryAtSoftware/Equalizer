namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public abstract class BaseEqualizationProfile : IEqualizationProfile
{
    public abstract bool CanExecuteFor(object? a, object? b);

    public IEqualizationResult Equalize(object? expected, object? actual, IEqualizationOptions options)
    {
        Assert.True(this.CanExecuteFor(expected, actual), $"The equalization profile {this.GetType().Name} cannot be executed for the given objects.");

        return this.EqualizeInternally(expected, actual, options);
    }

    protected abstract IEqualizationResult EqualizeInternally(object? expected, object? actual, IEqualizationOptions options);
}