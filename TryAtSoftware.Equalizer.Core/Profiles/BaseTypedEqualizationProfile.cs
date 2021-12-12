namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public abstract class BaseTypedEqualizationProfile<TPrincipal, TSubordinate> : BaseEqualizationProfile, IEqualizationProfile<TPrincipal, TSubordinate>
{
    public override bool CanExecuteFor(object a, object b) => a is TPrincipal && b is TSubordinate;

    protected override IEqualizationResult EqualizeInternally(object expected, object actual, IEqualizationOptions options)
    {
        var typedExpected = Assert.OfType<TPrincipal>(expected, nameof(expected));
        var typedActual = Assert.OfType<TSubordinate>(actual, nameof(actual));
        Assert.NotNull(options, nameof(options));

        return this.Equalize(typedExpected, typedActual, options);
    }

    public abstract IEqualizationResult Equalize(TPrincipal expected, TSubordinate actual, IEqualizationOptions options);
}