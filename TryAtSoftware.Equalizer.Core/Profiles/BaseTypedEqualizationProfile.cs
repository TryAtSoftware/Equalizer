namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An abstract class that should be inherited by any class implementing the <see cref="IEqualizationProfile"/> interface for a concrete pair of the types <typeparamref name="TExpected"/> and <typeparamref name="TActual"/>.
/// </summary>
/// <typeparam name="TExpected">The type of the expected value.</typeparam>
/// <typeparam name="TActual">The type of the actual value.</typeparam>
public abstract class BaseTypedEqualizationProfile<TExpected, TActual> : IEqualizationProfile
{
    /// <summary>
    /// Gets a value indicating whether or not the expected value can be null.
    /// </summary>
    /// <remarks>
    /// If the value of this property should be true, we suggest using nullable reference types when this is possible.
    /// </remarks>
    protected virtual bool AllowNullExpected => false;
    
    /// <summary>
    /// Gets a value indicating whether or not the actual value can be null.
    /// </summary>
    /// <remarks>
    /// If the value of this property should be true, we suggest using nullable reference types when this is possible.
    /// </remarks>
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

    /// <summary>
    /// Use this method to equalize the <paramref name="expected"/> and <paramref name="actual"/> values.
    /// </summary>
    /// <param name="expected">The expected <typeparamref name="TExpected"/> instance.</param>
    /// <param name="actual">The actual <typeparamref name="TActual"/> instance.</param>
    /// <param name="options">An <see cref="IEqualizationOptions"/> instance exposing additional information about the equalization process.</param>
    /// <returns>Returns a subsequently built <see cref="IEqualizationResult"/> instance containing information about the additionally executed equalization.</returns>
    protected abstract IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options);
}