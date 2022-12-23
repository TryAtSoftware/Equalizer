namespace TryAtSoftware.Equalizer.Core.Tests.Profiles;

using TryAtSoftware.Equalizer.Core.Profiles.Complex;
using TryAtSoftware.Equalizer.Core.Tests.Models;

public class CommonIdentifiableEqualizationProfile<TExpected, TActual, TKey> : ComplexEqualizationProfile<TExpected, TActual>
    where TActual : IIdentifiable<TKey>
{
    public CommonIdentifiableEqualizationProfile()
    {
        this.Differentiate<TKey>(default, i => i.Id);
    }
}