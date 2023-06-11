namespace TryAtSoftware.Equalizer.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Extensions;
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
        AssertCorrectEqualizationResult(equalizationResult);
    }

    public void AssertInequality(object? expected, object? actual)
    {
        var equalizationResult = this.Differentiate(expected, actual);
        AssertCorrectEqualizationResult(equalizationResult);
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

    private IEqualizationResult Equalize(object? expected, object? actual) => this.EqualizeInternally(expected, actual, GetValueType(expected), GetValueType(actual));

    private IEqualizationResult Differentiate(object? expected, object? actual) => this.DifferentiateInternally(expected, actual, GetValueType(expected), GetValueType(actual));

    private IEqualizationResult EqualizeInternally(object? expectedValue, object? actualValue, Type expectedType, Type actualType)
    {
        var profile = this.GetRequiredProfile(expectedValue, actualValue);
        return profile.Equalize(expectedValue, actualValue, this.GenerateEqualizationOptions(expectedType, actualType));
    }

    private IEqualizationResult DifferentiateInternally(object? expectedValue, object? actualValue, Type expectedType, Type actualType)
    {
        var profile = this.GetRequiredProfile(expectedValue, actualValue);
        var equalizationResult = profile.Equalize(expectedValue, actualValue, this.GenerateEqualizationOptions(expectedType, actualType));

        if (!equalizationResult.IsSuccessful) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(profile.UnsuccessfulDifferentiation(expectedValue, actualValue));
    }

    private IEqualizationProfile GetRequiredProfile(object? expected, object? actual)
    {
        var profile = this.GetProfile(expected, actual);
        Assert.NotNull(profile, nameof(profile));

        return profile;
    }

    private EqualizationOptions GenerateEqualizationOptions(Type expectedType, Type actualType) => new (expectedType, actualType, (x, y) => this.EqualizeInternally(x, y, expectedType, actualType), (x, y) => this.DifferentiateInternally(x, y, expectedType, actualType));

    private IEqualizationProfile? GetProfile(object? expected, object? actual)
        => this._providers.ConcatenateWith(this._internallyDefinedProviders).IgnoreNullValues().Select(provider => provider.GetProfile(expected, actual)).FirstOrDefault(profile => profile is not null);

    private static void AssertCorrectEqualizationResult(IEqualizationResult equalizationResult)
    {
        Assert.NotNull(equalizationResult, nameof(equalizationResult));
        Assert.True(equalizationResult.IsSuccessful, equalizationResult.Message);
    }
    
    private static Type GetValueType(object? value) => value?.GetType() ?? typeof(object);
}