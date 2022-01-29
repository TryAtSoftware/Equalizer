namespace TryAtSoftware.Equalizer.Core.Tests.Profiles;

using TryAtSoftware.Equalizer.Core.Profiles.Complex;
using TryAtSoftware.Equalizer.Core.Tests.Models;

public class CommonIdentifiableEqualizationProfile<TPrincipal, TIdentifiable, TKey> : ComplexEqualizationProfile<TPrincipal, TIdentifiable>
    where TIdentifiable : IIdentifiable<TKey>
{
    public CommonIdentifiableEqualizationProfile()
    {
        this.Differentiate<TKey>(default, i => i.Id);
    }
}