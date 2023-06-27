namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for defining the "is empty" logical function for text.
/// </summary>
public class EmptyTextEqualizationProfile : BaseTypedEqualizationProfile<EmptyValueTemplate, string?>
{
    /// <inheritdoc />
    protected override IEqualizationResult Equalize(EmptyValueTemplate expected, string? actual, IEqualizationOptions options)
    {
        if (string.IsNullOrWhiteSpace(actual)) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));
    }

    /// <inheritdoc />
    protected override bool AllowNullActual => true;

    /// <inheritdoc />
    protected override bool IsInvariant => true;
}