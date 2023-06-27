namespace TryAtSoftware.Equalizer.Core.Tests.Profiles;

using TryAtSoftware.Equalizer.Core.Profiles.Complex;
using TryAtSoftware.Equalizer.Core.Tests.Models;

public class CommonIdentifiableEqualizationProfile<TKey> : ComplexEqualizationProfile<object, IIdentifiable<TKey>>
{
    public CommonIdentifiableEqualizationProfile()
    {
        this.Differentiate<TKey>(default, i => i.Id);
    }
}