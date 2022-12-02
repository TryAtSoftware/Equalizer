namespace TryAtSoftware.Equalizer.Core.Tests.ProfileProviders;

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
        
        var addResult = dedicatedProfileProvider.AddProfile(null);
        Assert.False(addResult);
    }

    [Fact]
    public void ValidEqualizationProfileShouldBeAddedSuccessfully()
    {
        var dedicatedProfileProvider = new DedicatedProfileProvider();

        var profile = TestsCompanion.MockEqualizationProfile();
        var addResult = dedicatedProfileProvider.AddProfile(profile);
        Assert.True(addResult);
    }
}