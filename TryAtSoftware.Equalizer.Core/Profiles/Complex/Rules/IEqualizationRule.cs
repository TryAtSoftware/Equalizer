namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationRule<in TExpected, in TActual>
{
    IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options);
}