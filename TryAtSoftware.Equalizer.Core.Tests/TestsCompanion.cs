namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using System.Collections.Generic;
using NSubstitute;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public static class TestsCompanion
{
    public static (IEnumerable<IEqualizationProfile> All, IEqualizationProfile Executable) PrepareEqualizationProfileMultitude()
    {
        var allEqualizationProfiles = new List<IEqualizationProfile>();
        IEqualizationProfile? executableProfile = null;
        
        var profilesCount = RandomizationHelper.RandomInteger(2, 10);
        var executableProfileIndex = RandomizationHelper.RandomInteger(0, profilesCount);
        for (var i = 0; i < profilesCount; i++)
        {
            var isExecutable = i == executableProfileIndex;
            var profileInstance = MockEqualizationProfile(isExecutable);

            allEqualizationProfiles.Add(profileInstance);
            if (isExecutable) executableProfile = profileInstance;
        }

        Assert.NotNull(executableProfile);
        return (allEqualizationProfiles, executableProfile);
    }

    public static IEqualizationProfileProvider MockEqualizationProfileProvider()
        => Substitute.For<IEqualizationProfileProvider>();

    public static IEqualizationProfile MockEqualizationProfile(bool isExecutable = false)
    {
        var profile = Substitute.For<IEqualizationProfile>();
        profile.CanExecuteFor(Arg.Any<object>(), Arg.Any<object>()).Returns(isExecutable);

        return profile;
    }
    
    public static IEqualizationOptions MockEqualizationOptions() => MockEqualizationOptions((_, _) => new SuccessfulEqualizationResult());

    public static IEqualizationOptions MockEqualizationOptions(Func<object, object, IEqualizationResult> internalEqualization)
    {
        var equalizationOptions = Substitute.For<IEqualizationOptions>();
        equalizationOptions.Equalize(Arg.Any<object>(), Arg.Any<object>()).Returns(x => internalEqualization(x[0], x[1]));
        equalizationOptions.ExpectedType.Returns(typeof(object));
        equalizationOptions.ActualType.Returns(typeof(object));
        
        return equalizationOptions;
    }
}