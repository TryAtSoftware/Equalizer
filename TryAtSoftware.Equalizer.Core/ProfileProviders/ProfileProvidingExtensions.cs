namespace TryAtSoftware.Equalizer.Core.ProfileProviders;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Extensions.Collections;

public static class ProfileProvidingExtensions
{
    public static IEqualizationProfile FirstExecutable(this IEnumerable<IEqualizationProfile> profiles, object expected, object actual) => profiles.OrEmptyIfNull().IgnoreNullValues().FirstOrDefault(x => x.CanExecuteFor(expected, actual));
}