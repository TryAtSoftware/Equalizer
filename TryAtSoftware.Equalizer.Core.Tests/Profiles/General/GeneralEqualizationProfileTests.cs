namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.General;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles.General;
using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;
using Xunit;

public static class GeneralEqualizationProfileTests
{
    [Fact]
    public static void GeneralEqualityShouldBeSuccessfullyValidated()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shop1.Duplicate();

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(shop1, shop2);
    }
    
    [Fact]
    public static void GeneralInequalityShouldBeSuccessfullyValidated()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shopRandomizer.PrepareRandomValue();

        var equalizer = PrepareEqualizer();
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertEquality(shop1, shop2));
    }

    private static Equalizer PrepareEqualizer()
    {
        var equalizer = new Equalizer();
        var profilesProvider = new DedicatedProfileProvider();
        profilesProvider.AddProfile(new GeneralEqualizationProfile<Shop>());
        equalizer.AddProfileProvider(profilesProvider);
        return equalizer;
    }
}