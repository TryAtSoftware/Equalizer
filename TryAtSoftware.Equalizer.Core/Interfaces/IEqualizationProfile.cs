namespace TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationProfile
{
    bool CanExecuteFor(object? a, object? b);
    IEqualizationResult Equalize(object? expected, object? actual, IEqualizationOptions options);
}

public interface IEqualizationProfile<in TExpected, in TActual> : IEqualizationProfile
{
    IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options);
}