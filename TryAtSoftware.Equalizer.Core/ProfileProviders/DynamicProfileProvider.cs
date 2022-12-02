namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DynamicProfileProvider : IEqualizationProfileProvider
{
    [NotNull]
    private readonly Func<IEnumerable<IEqualizationProfile>> _getProfiles;

    public DynamicProfileProvider([NotNull] Func<IEnumerable<IEqualizationProfile>> getProfiles)
    {
        this._getProfiles = getProfiles ?? throw new ArgumentNullException(nameof(getProfiles));
    }

    public IEqualizationProfile GetProfile(object expected, object actual) => this._getProfiles().FirstExecutable(expected, actual);
}