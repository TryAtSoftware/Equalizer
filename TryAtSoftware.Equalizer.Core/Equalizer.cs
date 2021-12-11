namespace TryAtSoftware.Equalizer.Core;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles;

public class Equalizer : IEqualizer
{
    private readonly List<IEqualizationProfileProvider> _internallyDefinedProviders = new();
    private readonly List<IEqualizationProfileProvider> _providers = new();

    public Equalizer()
    {
        var standardProfileProvider = new DynamicProfileProvider((_, _) => new StandardEqualizationProfile());
        this._internallyDefinedProviders.Add(standardProfileProvider);
    }

    public void AssertEquality(object expected, object actual)
    {
        var profile = this.GetProfile(expected, actual);
        Assert.NotNull(profile, nameof(profile));

        var options = new EqualizationOptions(expected.GetType(), actual.GetType(), this.AssertEquality);
        profile.AssertEquality(expected, actual, options);
    }

    public bool AddProfileProvider(IEqualizationProfileProvider provider)
    {
        if (provider is null)
            return false;

        this._providers.Add(provider);
        return true;
    }

    private IEqualizationProfile GetProfile(object principal, object actual) => this._providers.ConcatenateWith(this._internallyDefinedProviders).IgnoreNullValues().Select(provider => provider.GetProfile(principal, actual)).FirstOrDefault(profile => profile is not null);
}