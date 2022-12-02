namespace TryAtSoftware.Equalizer.Core.Tests.ProfileProviders;

using System.Collections.Generic;
using Moq;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Randomizer.Core.Helpers;

public static class ProfileProviderTestsCompanion
{
    public static (IEnumerable<IEqualizationProfile> All, IEqualizationProfile Executable) PrepareEqualizationProfileMultitude()
    {
        var allEqualizationProfiles = new List<IEqualizationProfile>();
        IEqualizationProfile executableProfile = null;
        
        var profilesCount = RandomizationHelper.RandomInteger(2, 10);
        var executableProfileIndex = RandomizationHelper.RandomInteger(0, profilesCount);
        for (var i = 0; i < profilesCount; i++)
        {
            var isExecutable = i == executableProfileIndex;
            var profileMock = new Mock<IEqualizationProfile>();
            profileMock.Setup(x => x.CanExecuteFor(It.IsAny<object>(), It.IsAny<object>())).Returns(isExecutable);

            var profileInstance = profileMock.Object;
            allEqualizationProfiles.Add(profileInstance);

            if (isExecutable) executableProfile = profileInstance;
        }

        return (allEqualizationProfiles, executableProfile);
    }
}