namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.General;

using System;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.PartialValues;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles.General;
using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class PartialGeneralEqualizationProfileTests
{
    [Fact]
    public void PartialGeneralEqualityShouldBeValidatedSuccessfullyWithExcludedMembers()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shop1.Duplicate();

        var partialShop1 = shop1.Exclude(nameof(Shop.Address));
        shop2.Address = RandomizationHelper.GetRandomString();

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(partialShop1, shop2);
    }

    [Fact]
    public void PartialGeneralEqualityShouldBeValidatedSuccessfullyWithIncludedMembers()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shop1.Duplicate();

        var partialShop1 = shop1.Include(nameof(Shop.Id), nameof(Shop.Name), nameof(Shop.Area));
        shop2.Address = RandomizationHelper.GetRandomString();

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(partialShop1, shop2);
    }

    [Fact]
    public void PartialGeneralInequalityShouldBeValidatedSuccessfullyWithExcludedMembers() => ValidatePartialGeneralInequality(x => x.Exclude(nameof(Shop.Address)));
    
    [Fact]
    public void PartialGeneralInequalityShouldBeValidatedSuccessfullyWithIncludedMembers() => ValidatePartialGeneralInequality(x => x.Include(nameof(Shop.Id), nameof(Shop.Name)));

    private static void ValidatePartialGeneralInequality(Func<Shop, IPartialValue<Shop>> partialValueGenerator)
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shopRandomizer.PrepareRandomValue();

        var partialShop1 = partialValueGenerator(shop1);

        var equalizer = PrepareEqualizer();
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertEquality(partialShop1, shop2));
    }
    
    private static Equalizer PrepareEqualizer()
    {
        var equalizer = new Equalizer();
        var shopGeneralEqualizationProfile = new GeneralEqualizationProfile<Shop>();
        var shopPartialGeneralEqualizationProfile = new PartialGeneralEqualizationProfile<Shop>();

        var profilesProvider = new DedicatedProfileProvider();
        profilesProvider.AddProfile(shopGeneralEqualizationProfile);
        profilesProvider.AddProfile(shopPartialGeneralEqualizationProfile);
        
        equalizer.AddProfileProvider(profilesProvider);
        return equalizer;
    }
}