namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public abstract class BaseTypedEqualizationProfile<TPrincipal, TSubordinate> : BaseEqualizationProfile, IEqualizationProfile<TPrincipal, TSubordinate>
{
    public override bool CanExecuteFor(object a, object b)
    {
        if (a is not TPrincipal && (a is not null || !this.AllowNullPrincipal)) return false;
        if (b is not TSubordinate && (b is not null || !this.AllowNullSubordinate)) return false;

        return true;
    }

    protected override IEqualizationResult EqualizeInternally(object expected, object actual, IEqualizationOptions options)
    {
        var typedExpected = this.AllowNullPrincipal ? (TPrincipal)expected : Assert.OfType<TPrincipal>(expected, nameof(expected));
        var typedActual = this.AllowNullSubordinate ? (TSubordinate)actual : Assert.OfType<TSubordinate>(actual, nameof(actual));
        Assert.NotNull(options, nameof(options));

        return this.Equalize(typedExpected, typedActual, options);
    }

    public abstract IEqualizationResult Equalize(TPrincipal expected, TSubordinate actual, IEqualizationOptions options);

    protected virtual bool AllowNullPrincipal => false;
    protected virtual bool AllowNullSubordinate => false;
}