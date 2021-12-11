namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DedicatedProfileProvider : IEqualizationProfileProvider
{
    private readonly List<IEqualizationProfile> _profiles = new();

    public IEqualizationProfile GetProfile(object principal, object actual) => this._profiles.FirstOrDefault(p => p.CanExecuteFor(principal, actual));

    public bool AddProfile(IEqualizationProfile profile)
    {
        if (profile is null)
            return false;

        this._profiles.Add(profile);
        return true;
    }
}