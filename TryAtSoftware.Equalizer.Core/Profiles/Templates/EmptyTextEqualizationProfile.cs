namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

public class EmptyTextEqualizationProfile : BaseTypedEqualizationProfile<EmptyValueTemplate, string?>
{
    protected override IEqualizationResult Equalize(EmptyValueTemplate expected, string? actual, IEqualizationOptions options)
    {
        if (string.IsNullOrWhiteSpace(actual)) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));
    }

    protected override bool AllowNullActual => true;
}