namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfileProvider"/> interface providing a finite set of pre-registered <see cref="IEqualizationProfile"/> instances.
/// </summary>
public class DedicatedProfileProvider : IEqualizationProfileProvider
{
    private readonly List<IEqualizationProfile> _profiles = new();

    /// <inheritdoc />
    public IEqualizationProfile? GetProfile(object? expected, object? actual) => this._profiles.FirstExecutable(expected, actual);

    /// <summary>
    /// Use this method to register a new <see cref="IEqualizationProfile"/> instance.
    /// </summary>
    /// <param name="profile">The <see cref="IEqualizationProfile"/> instance to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="profile"/> is null.</exception>
    public void AddProfile(IEqualizationProfile profile)
    {
        if (profile is null) throw new ArgumentNullException(nameof(profile));
        this._profiles.Add(profile);
    }
}