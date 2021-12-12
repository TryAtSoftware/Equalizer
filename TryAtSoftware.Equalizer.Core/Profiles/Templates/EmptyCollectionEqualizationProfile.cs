namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

public class EmptyCollectionEqualizationProfile : BaseTypedEqualizationProfile<EmptyValueTemplate, IEnumerable<object>>
{
    public override IEqualizationResult Equalize(EmptyValueTemplate expected, IEnumerable<object> actual, IEqualizationOptions options)
    {
        if (actual is null || !actual.Any()) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));
    }

    protected override bool AllowNullSubordinate => true;
}