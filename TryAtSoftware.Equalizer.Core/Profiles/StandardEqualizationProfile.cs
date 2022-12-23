namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class StandardEqualizationProfile : BaseEqualizationProfile
{
    public override bool CanExecuteFor(object? a, object? b) => true;

    protected override IEqualizationResult EqualizeInternally(object? expected, object? actual, IEqualizationOptions options)
    {
        if (Equals(expected, actual))
            return new SuccessfulEqualizationResult();

        return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));
    }
}