namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.PartialValues;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for the partial general equalization between two values of the same type.
/// </summary>
/// <typeparam name="T">The concrete entity type for the general equalization process.</typeparam>
public sealed class PartialGeneralEqualizationProfile<T> : IEqualizationProfile
    where T : notnull
{
    private readonly IGeneralEqualizationContext<T> _generalEqualizationContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PartialGeneralEqualizationProfile{T}"/> class.
    /// </summary>
    /// <param name="generalEqualizationContext">An explicit <see cref="IGeneralEqualizationContext{T}"/> instance to use. This is an optional parameter and should be used cautiously.</param>
    public PartialGeneralEqualizationProfile(IGeneralEqualizationContext<T>? generalEqualizationContext = null)
    {
        this._generalEqualizationContext = generalEqualizationContext ?? GeneralEqualizationContext<T>.Instance;
    }

    /// <inheritdoc />
    public bool CanExecuteFor(object? expected, object? actual) => expected is IPartialValue { Value: T } && actual is T;

    /// <inheritdoc />
    public IEqualizationResult Equalize(object? expected, object? actual, IEqualizationOptions options)
    {
        Assert.NotNull(expected, nameof(expected));
        Assert.NotNull(expected, nameof(actual));

        var typedExpectedPartialValue = Assert.OfType<IPartialValue>(expected, nameof(expected));
        var typedExpectedValue = Assert.OfType<T>(typedExpectedPartialValue.Value, nameof(typedExpectedPartialValue.Value));
        var typedActualValue = Assert.OfType<T>(actual, nameof(actual));

        var wrappedPartialValue = new DynamicPartialValue<T>(typedExpectedValue, typedExpectedPartialValue.IncludesMember);
        return this.Equalize(wrappedPartialValue, typedActualValue, options, this._generalEqualizationContext);
    }
}