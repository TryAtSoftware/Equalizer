namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System;
using System.Collections.Generic;
using System.Linq;
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

    public IEqualizationProfile GetProfile(object principal, object actual) => this._getProfiles().FirstOrDefault(p => p is not null && p.CanExecuteFor(principal, actual));
}