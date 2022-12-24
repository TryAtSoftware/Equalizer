namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public abstract class BaseTypedEqualizationProfile<TExpected, TActual> : IEqualizationProfile
{
    protected virtual bool AllowNullExpected => false;
    protected virtual bool AllowNullActual => false;

    /// <inheritdoc />
    public bool CanExecuteFor(object? expected, object? actual)
    {
        if (expected is not TExpected && (expected is not null || !this.AllowNullExpected)) return false;
        if (actual is not TActual && (actual is not null || !this.AllowNullActual)) return false;

        return true;
    }

    /// <inheritdoc />
    public IEqualizationResult Equalize(object? expected, object? actual, IEqualizationOptions options)
    {
        var typedExpected = this.AllowNullExpected && expected is null ? (TExpected)expected! : Assert.OfType<TExpected>(expected, nameof(expected));
        var typedActual = this.AllowNullActual && actual is null ? (TActual)actual! : Assert.OfType<TActual>(actual, nameof(actual));
        Assert.NotNull(options, nameof(options));

        return this.Equalize(typedExpected, typedActual, options);
    }

    protected abstract IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options);
}