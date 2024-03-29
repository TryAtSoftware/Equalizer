﻿namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using System.Collections.Generic;
using Moq;
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
    {
        var profileProviderMock = new Mock<IEqualizationProfileProvider>();
        return profileProviderMock.Object;
    }

    public static IEqualizationProfile MockEqualizationProfile(bool isExecutable = false)
    {
        var profileMock = new Mock<IEqualizationProfile>();
        profileMock.Setup(x => x.CanExecuteFor(It.IsAny<object>(), It.IsAny<object>())).Returns(isExecutable);

        return profileMock.Object;
    }
    
    public static Mock<IEqualizationOptions> MockEqualizationOptions() => MockEqualizationOptions((_, _) => new SuccessfulEqualizationResult());

    public static Mock<IEqualizationOptions> MockEqualizationOptions(Func<object, object, IEqualizationResult> internalEqualization)
    {
        var equalizationOptionsMock = new Mock<IEqualizationOptions>();
        equalizationOptionsMock.Setup(eo => eo.Equalize(It.IsAny<object>(), It.IsAny<object>())).Returns(internalEqualization);
        equalizationOptionsMock.Setup(eo => eo.ExpectedType).Returns(typeof(object));
        equalizationOptionsMock.Setup(eo => eo.ActualType).Returns(typeof(object));
        return equalizationOptionsMock;
    }
}