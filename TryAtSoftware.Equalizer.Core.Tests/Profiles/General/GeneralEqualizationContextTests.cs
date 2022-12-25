namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.General;

using System;
using System.Reflection;
using TryAtSoftware.Equalizer.Core.Profiles.General;
using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;
using TryAtSoftware.Extensions.Reflection;
using Xunit;

public class GeneralEqualizationContextTests
{
    [Fact]
    public void GeneralEqualizationContextMustNotBeInstantiatedWithoutMembersBinder() => Assert.Throws<ArgumentNullException>(() => new GeneralEqualizationContext<Shop>(null!));

    [Fact]
    public void GeneralEqualizationContextMustNotBeInstantiatedWithInvalidMembersBinder()
    {
        var invalidMembersBinder = new MembersBinder<Product>(isValid: null, BindingFlags.Public | BindingFlags.Public);
        Assert.Throws<InvalidOperationException>(() => new GeneralEqualizationContext<Shop>(invalidMembersBinder));
    }

    [Fact]
    public void DefaultGeneralEqualizationContextShouldShouldIncludePropertiesFromBaseTypes()
    {
        var context = GeneralEqualizationContext<Shop>.Instance;
        Assert.NotNull(context);

        Assert.True(context.ValueAccessors.ContainsKey(nameof(Shop.Id)));
        Assert.True(context.ValueAccessors.ContainsKey(nameof(Shop.Address)));
        Assert.True(context.ValueAccessors.ContainsKey(nameof(Shop.Area)));
        Assert.True(context.ValueAccessors.ContainsKey(nameof(Shop.Name)));

        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();
        Assert.Equal(shop.Id, context.ValueAccessors[nameof(Shop.Id)](shop));
        Assert.Equal(shop.Address, context.ValueAccessors[nameof(Shop.Address)](shop));
        Assert.Equal(shop.Area, context.ValueAccessors[nameof(Shop.Area)](shop));
        Assert.Equal(shop.Name, context.ValueAccessors[nameof(Shop.Name)](shop));
    }
}