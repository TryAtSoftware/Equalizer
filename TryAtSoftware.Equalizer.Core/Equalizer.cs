namespace TryAtSoftware.Equalizer.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles;
using TryAtSoftware.Equalizer.Core.Profiles.Templates;
using TryAtSoftware.Extensions.Collections;

/// <summary>
/// A default implementation of the <see cref="IEqualizer"/> interface.
/// </summary>
public class Equalizer : IEqualizer
{
    private readonly List<IEqualizationProfileProvider> _internallyDefinedProviders = new();
    private readonly List<IEqualizationProfileProvider> _providers = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Equalizer"/> class.
    /// </summary>
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

    /// <summary>
    /// Gets all externally registered <see cref="IEqualizationProfileProvider"/> instances.
    /// </summary>
    public IReadOnlyCollection<IEqualizationProfileProvider> CustomProfileProviders => this._providers.AsReadOnly();

    /// <inheritdoc />
    public void AssertEquality(object? expected, object? actual)
    {
        var equalizationResult = this.Equalize(expected, actual);
        Assert.True(equalizationResult.IsSuccessful, equalizationResult.Message);
    }

    /// <summary>
    /// Use this method to register an external <see cref="IEqualizationProfileProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="IEqualizationProfileProvider"/> instance to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="provider"/> is null.</exception>
    public void AddProfileProvider(IEqualizationProfileProvider provider)
    {
        if (provider is null) throw new ArgumentNullException(nameof(provider));
        this._providers.Add(provider);
    }

    private IEqualizationResult Equalize(object? expected, object? actual)
    {
        var principalType = expected?.GetType() ?? typeof(object);
        var subordinateType = actual?.GetType() ?? typeof(object);
        return EqualizeInternally(expected, actual);

        IEqualizationResult EqualizeInternally(object? expectedValue, object? actualValue)
        {
            var profile = this.GetProfile(expectedValue, actualValue);
            Assert.NotNull(profile, nameof(profile));
            var options = new EqualizationOptions(principalType, subordinateType, EqualizeInternally);
            return profile.Equalize(expectedValue, actualValue, options);
        }
    }

    private IEqualizationProfile? GetProfile(object? expected, object? actual)
        => this._providers.ConcatenateWith(this._internallyDefinedProviders).IgnoreNullValues().Select(provider => provider.GetProfile(expected, actual)).FirstOrDefault(profile => profile is not null);
}