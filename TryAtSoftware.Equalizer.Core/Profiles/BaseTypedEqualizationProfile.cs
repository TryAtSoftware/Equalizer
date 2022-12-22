namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public abstract class BaseTypedEqualizationProfile<TExpected, TActual> : BaseEqualizationProfile, IEqualizationProfile<TExpected, TActual>
{
    public override bool CanExecuteFor(object? a, object? b)
    {
        if (a is not TExpected && (a is not null || !this.AllowNullExpected)) return false;
        if (b is not TActual && (b is not null || !this.AllowNullActual)) return false;

        return true;
    }

    protected override IEqualizationResult EqualizeInternally(object? expected, object? actual, IEqualizationOptions options)
    {
        var typedExpected = this.AllowNullExpected && expected is null ? (TExpected)expected! : Assert.OfType<TExpected>(expected, nameof(expected));
        var typedActual = this.AllowNullActual && actual is null ? (TActual)actual! : Assert.OfType<TActual>(actual, nameof(actual));
        Assert.NotNull(options, nameof(options));

        return this.Equalize(typedExpected, typedActual, options);
    }

    public abstract IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options);

    protected virtual bool AllowNullExpected => false;
    protected virtual bool AllowNullActual => false;
}