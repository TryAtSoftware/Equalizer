namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.PartialValues;

public class PartialGeneralEqualizationProfile<T> : BaseTypedEqualizationProfile<IPartialValue<T>, T>
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