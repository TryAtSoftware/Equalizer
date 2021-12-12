namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using System;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

public class GreaterThanOrEqualEqualizationProfile : BaseTypedEqualizationProfile<GreaterThanOrEqualValueTemplate, IComparable>
{
    public override IEqualizationResult Equalize(GreaterThanOrEqualValueTemplate expected, IComparable actual, IEqualizationOptions options)
    {
        if (actual.CompareTo(expected.Value) < 0)
            return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        return new SuccessfulEqualizationResult();
    }
}