namespace TryAtSoftware.Equalizer.Core.Tests.ProfileProviders;

using System;
using System.Linq;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using Xunit;

public class DedicatedProfileProviderTests
{
    [Fact]
    public void DedicatedProfileProviderShouldReturnCorrectEqualizationProfile()
    {
        var (allEqualizationProfiles, executableProfile) = TestsCompanion.PrepareEqualizationProfileMultitude();

        var dedicatedProfileProvider = new DedicatedProfileProvider();
        foreach (var profile in allEqualizationProfiles) dedicatedProfileProvider.AddProfile(profile);

        var actualProfile = dedicatedProfileProvider.GetProfile("expected_value", "actual_value");
        Assert.Same(executableProfile, actualProfile);
    }

    [Fact]
    public void DedicatedProfileProviderShouldNotReturnAnyEqualizationProfileIfNoneIsExecutableForTheProvidedInput()
    {
        var (allEqualizationProfiles, executableProfile) = TestsCompanion.PrepareEqualizationProfileMultitude();

        var dedicatedProfileProvider = new DedicatedProfileProvider();
        foreach (var profile in allEqualizationProfiles.Where(x => x != executableProfile)) dedicatedProfileProvider.AddProfile(profile);

        var actualProfile = dedicatedProfileProvider.GetProfile("expected_value", "actual_value");
        Assert.Null(actualProfile);
    }

    [Fact]
    public void InvalidEqualizationProfileShouldNotBeAddedSuccessfully()
    {
        var dedicatedProfileProvider = new DedicatedProfileProvider();

        Assert.Throws<ArgumentNullException>(() => dedicatedProfileProvider.AddProfile(null!));
    }

    [Fact]
    public void ValidEqualizationProfileShouldBeAddedSuccessfully()
    {
        var dedicatedProfileProvider = new DedicatedProfileProvider();

        var profile = TestsCompanion.MockEqualizationProfile(isExecutable: true);
        dedicatedProfileProvider.AddProfile(profile);

        var retrievedProfile = dedicatedProfileProvider.GetProfile(null, null);
        Assert.Same(profile, retrievedProfile);
    }
}