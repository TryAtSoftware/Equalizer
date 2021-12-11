namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System;
using JetBrains.Annotations;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DynamicProfileProvider : IEqualizationProfileProvider
{
    [NotNull]
    private readonly Func<object, object, IEqualizationProfile> _func;

    public DynamicProfileProvider([NotNull] Func<object, object, IEqualizationProfile> func)
    {
        this._func = func ?? throw new ArgumentNullException(nameof(func));
    }

    public IEqualizationProfile GetProfile(object principal, object actual) => this._func(principal, actual);
}