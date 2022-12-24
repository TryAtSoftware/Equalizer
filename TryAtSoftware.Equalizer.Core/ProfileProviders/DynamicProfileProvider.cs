namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfileProvider"/> interface providing a dynamically retrieved set <see cref="IEqualizationProfileProvider"/>. 
/// </summary>
public class DynamicProfileProvider : IEqualizationProfileProvider
{
    private readonly Func<IEnumerable<IEqualizationProfile>> _getProfiles;

    /// <summary>
    /// Initializes a new instance of the <see cref="DedicatedProfileProvider"/> class.
    /// </summary>
    /// <param name="getProfiles">A function that will be called dynamically whenever access to the dynamic set of <see cref="IEqualizationProfile"/> instances is required.</param>
    public DynamicProfileProvider(Func<IEnumerable<IEqualizationProfile>> getProfiles)
    {
        this._getProfiles = getProfiles ?? throw new ArgumentNullException(nameof(getProfiles));
    }

    /// <inheritdoc />
    public IEqualizationProfile? GetProfile(object? expected, object? actual) => this._getProfiles().FirstExecutable(expected, actual);
}