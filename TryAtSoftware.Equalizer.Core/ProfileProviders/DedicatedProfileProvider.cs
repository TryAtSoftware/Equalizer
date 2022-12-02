namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DedicatedProfileProvider : IEqualizationProfileProvider
{
    private readonly List<IEqualizationProfile> _profiles = new();

    public IEqualizationProfile GetProfile(object expected, object actual) => this._profiles.FirstExecutable(expected, actual);

    public bool AddProfile(IEqualizationProfile profile)
    {
        if (profile is null)
            return false;

        this._profiles.Add(profile);
        return true;
    }
}