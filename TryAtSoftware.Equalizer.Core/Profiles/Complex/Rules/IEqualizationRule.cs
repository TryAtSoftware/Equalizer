namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationRule<in T1, in T2>
{
    IEqualizationResult Equalize(T1 expected, T2 actual, IEqualizationOptions options);
}