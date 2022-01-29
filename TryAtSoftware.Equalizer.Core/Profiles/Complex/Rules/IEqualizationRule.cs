namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationRule<in TPrincipal, in TSubordinate>
{
    IEqualizationResult Equalize(TPrincipal principal, TSubordinate subordinate, IEqualizationOptions options);
}