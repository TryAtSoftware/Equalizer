namespace TryAtSoftware.Equalizer.Core.Tests.ProfileProviders;

using System;
using System.Linq;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using Xunit;

public class DynamicProfileProviderTests
{
    [Fact]
    public void DynamicProfileProviderShouldNotBeInstantiatedSuccessfullyWithInvalidParameters() 
        => Assert.Throws<ArgumentNullException>(() => new DynamicProfileProvider(getProfiles: null!));

    [Fact]
    public void DynamicProfileProviderShouldReturnCorrectEqualizationProfile()
    {
        var (allEqualizationProfiles, executableProfile) = ProfileProviderTestsCompanion.PrepareEqualizationProfileMultitude();

        var dynamicProfileProvider = new DynamicProfileProvider(() => allEqualizationProfiles);
        
        var actualProfile = dynamicProfileProvider.GetProfile("expected_value", "actual_value");
        Assert.Same(executableProfile, actualProfile);
    }

    [Fact]
    public void DynamicProfileProviderShouldNotReturnAnyEqualizationProfileIfNoneIsExecutableForTheProvidedInput()
    {
        var (allEqualizationProfiles, executableProfile) = ProfileProviderTestsCompanion.PrepareEqualizationProfileMultitude();

        var dynamicProfileProvider = new DynamicProfileProvider(() => allEqualizationProfiles.Where(x => x != executableProfile));
        
        var actualProfile = dynamicProfileProvider.GetProfile("expected_value", "actual_value");
        Assert.Null(actualProfile);
    }
}
