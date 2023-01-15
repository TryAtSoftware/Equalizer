namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.PartialValues;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for the partial general equalization between two values of the same type.
/// </summary>
/// <typeparam name="T">The concrete entity type for the general equalization process.</typeparam>
public sealed class PartialGeneralEqualizationProfile<T> : BaseTypedEqualizationProfile<IPartialValue<T>, T>
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
    protected override IEqualizationResult Equalize(IPartialValue<T> expected, T actual, IEqualizationOptions options) => this.Equalize(expected, actual, options, this._generalEqualizationContext);
}