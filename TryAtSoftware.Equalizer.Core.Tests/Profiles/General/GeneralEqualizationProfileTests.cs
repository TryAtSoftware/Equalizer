namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.General;

using System.Reflection;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Profiles.General;
using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;
using TryAtSoftware.Extensions.Reflection;
using Xunit;

public static class GeneralEqualizationProfileTests
{
    [Fact]
    public static void GeneralEqualityShouldBeSuccessfullyValidatedWithoutExplicitContext() => GeneralEqualityShouldBeSuccessfullyValidated();

    [Fact]
    public static void GeneralEqualityShouldBeSuccessfullyValidatedWithExplicitContext() => GeneralEqualityShouldBeSuccessfullyValidated(ConstructCustomEqualizationContext());

    [Fact]
    public static void GeneralInequalityShouldBeSuccessfullyValidatedWithoutExplicitContext() => GeneralInequalityShouldBeSuccessfullyValidated();

    [Fact]
    public static void GeneralInequalityShouldBeSuccessfullyValidatedWithExplicitContext() => GeneralInequalityShouldBeSuccessfullyValidated(ConstructCustomEqualizationContext());

    private static void GeneralEqualityShouldBeSuccessfullyValidated(IGeneralEqualizationContext<Shop>? context = null)
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shop1.Duplicate();

        var equalizer = PrepareEqualizer(context);
        equalizer.AssertEquality(shop1, shop2);
    }

    private static void GeneralInequalityShouldBeSuccessfullyValidated(IGeneralEqualizationContext<Shop>? context = null)
    {
        var shopRandomizer = new ShopRandomizer();
        var shop1 = shopRandomizer.PrepareRandomValue();
        var shop2 = shopRandomizer.PrepareRandomValue();

        var equalizer = PrepareEqualizer(context);
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertEquality(shop1, shop2));
    }

    private static Equalizer PrepareEqualizer(IGeneralEqualizationContext<Shop>? context)
    {
        var equalizer = new Equalizer();
        var shopGeneralEqualizationProfile = new GeneralEqualizationProfile<Shop>(context);

        var profilesProvider = new DedicatedProfileProvider();
        profilesProvider.AddProfile(shopGeneralEqualizationProfile);
        equalizer.AddProfileProvider(profilesProvider);

        return equalizer;
    }

    private static IGeneralEqualizationContext<Shop> ConstructCustomEqualizationContext()
    {
        var membersBinder = new MembersBinder<Shop>(x => x.Name == nameof(Shop.Name), BindingFlags.Public | BindingFlags.Instance);
        return new GeneralEqualizationContext<Shop>(membersBinder);
    }
}