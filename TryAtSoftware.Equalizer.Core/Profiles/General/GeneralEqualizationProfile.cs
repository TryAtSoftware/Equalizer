namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for the general equalization between two values of the same type.
/// </summary>
/// <typeparam name="T">The concrete entity type for the general equalization process.</typeparam>
public class GeneralEqualizationProfile<T> : BaseTypedEqualizationProfile<T, T>
{
    private readonly IGeneralEqualizationContext<T> _generalEqualizationContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeneralEqualizationProfile{T}"/> class.
    /// </summary>
    /// <param name="generalEqualizationContext">An explicit <see cref="IGeneralEqualizationContext{T}"/> instance to use. This is an optional parameter and should be used cautiously.</param>
    public GeneralEqualizationProfile(IGeneralEqualizationContext<T>? generalEqualizationContext = null)
    {
        this._generalEqualizationContext = generalEqualizationContext ?? GeneralEqualizationContext<T>.Instance;
    }

    /// <inheritdoc />
    protected override IEqualizationResult Equalize(T expected, T actual, IEqualizationOptions options) => this.Equalize(expected, actual, options, this._generalEqualizationContext);
}