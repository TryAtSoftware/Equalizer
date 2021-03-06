namespace TryAtSoftware.Equalizer.Core;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles;
using TryAtSoftware.Equalizer.Core.Profiles.Templates;
using TryAtSoftware.Extensions.Collections;

public class Equalizer : IEqualizer
{
    private readonly List<IEqualizationProfileProvider> _internallyDefinedProviders = new();
    private readonly List<IEqualizationProfileProvider> _providers = new();

    public Equalizer()
    {
        var dedicatedProfileProvider = new DedicatedProfileProvider();
        dedicatedProfileProvider.AddProfile(new LowerThanEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new LowerThanOrEqualEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new GreaterThanEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new GreaterThanOrEqualEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new EmptyCollectionEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new EmptyTextEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new CollectionEqualizationProfile());
        dedicatedProfileProvider.AddProfile(new StandardEqualizationProfile());
        this._internallyDefinedProviders.Add(dedicatedProfileProvider);
    }

    public void AssertEquality(object expected, object actual)
    {
        var equalizationResult = this.Equalize(expected, actual);
        Assert.True(equalizationResult.IsSuccessful, equalizationResult.Message);
    }

    private IEqualizationResult Equalize(object expected, object actual)
    {
        var principalType = expected.GetType();
        var subordinateType = actual.GetType();
        return EqualizeInternally(expected, actual);

        IEqualizationResult EqualizeInternally(object expectedValue, object actualValue)
        {
            var profile = this.GetProfile(expectedValue, actualValue);
            Assert.NotNull(profile, nameof(profile));
            var options = new EqualizationOptions(principalType, subordinateType, EqualizeInternally);
            return profile.Equalize(expectedValue, actualValue, options);
        }
    }

    public bool AddProfileProvider(IEqualizationProfileProvider provider)
    {
        if (provider is null) return false;

        this._providers.Add(provider);
        return true;
    }

    private IEqualizationProfile GetProfile(object expected, object actual)
        => this._providers.ConcatenateWith(this._internallyDefinedProviders).IgnoreNullValues().Select(provider => provider.GetProfile(expected, actual)).FirstOrDefault(profile => profile is not null);
}