namespace TryAtSoftware.Equalizer.Core.Tests.PartialValues;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.PartialValues;
using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;
using Xunit;

public class PartialValueTests
{
    [Fact]
    public void UnlimitedPartialValueShouldNotBeInstantiatedIfTheProvidedInstanceIsNull() => Assert.Throws<ArgumentNullException>(() => new UnlimitedPartialValue<object>(null!));

    [Fact]
    public void AsPartialValueShouldThrowExceptionIfTheExtendedInstanceIsNull()
    {
        object nullObject = null!;
        Assert.Throws<ArgumentNullException>(() => nullObject.AsPartialValue());
    }

    [Fact]
    public void AsPartialValueShouldInstantiateAnUnlimitedPartialValueSuccessfully()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();

        var partialShop = shop.AsPartialValue();
        Assert.IsType<UnlimitedPartialValue<Shop>>(partialShop);
        Assert.Same(shop, partialShop.Value);
    }

    [Fact]
    public void UnlimitedPartialValuesShouldIncludeAllMembersOfTheUnderlyingInstance()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();

        var unlimitedPartialValueOfShop = shop.AsPartialValue();
        var properties = typeof(Shop).GetProperties();
        foreach (var property in properties) Assert.True(unlimitedPartialValueOfShop.IncludesMember(property.Name));
    }

    [Fact]
    public void ExclusivePartialValueShouldNotBeInstantiatedIfTheProvidedInstanceIsNull() => Assert.Throws<ArgumentNullException>(() => new ExclusivePartialValue<object>(null!));

    [Fact]
    public void ExcludeShouldThrowExceptionIfTheExtendedInstanceIsNull()
    {
        object nullObject = null!;
        Assert.Throws<ArgumentNullException>(() => nullObject.Exclude("memberKey"));
    }

    [Fact]
    public void ExcludeShouldInstantiateAnExclusivePartialValueSuccessfully()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();

        var partialShop = shop.Exclude(nameof(Shop.Address));
        Assert.IsType<ExclusivePartialValue<Shop>>(partialShop);
        Assert.Same(shop, partialShop.Value);
    }

    [Fact]
    public void ExclusivePartialValueShouldNotIncludeAllSpecifiedMembers()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();

        var membersToExclude = new HashSet<string> { nameof(Shop.Name), nameof(Shop.Address) };
        var exclusivePartialValueOfShop = shop.Exclude(membersToExclude.ToArray());

        var properties = typeof(Shop).GetProperties();
        foreach (var property in properties.Where(p => !membersToExclude.Contains(p.Name))) Assert.True(exclusivePartialValueOfShop.IncludesMember(property.Name));
        foreach (var excludedMember in membersToExclude) Assert.False(exclusivePartialValueOfShop.IncludesMember(excludedMember));
    }

    [Fact]
    public void InclusivePartialValueShouldNotBeInstantiatedIfTheProvidedInstanceIsNull() => Assert.Throws<ArgumentNullException>(() => new InclusivePartialValue<object>(null!));

    [Fact]
    public void InclusivePartialValueShouldIncludeOnlyTheSpecifiedMembers()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();

        var membersToInclude = new HashSet<string> { nameof(Shop.Id), nameof(Shop.Address) };
        var inclusivePartialValueOfShop = shop.Include(membersToInclude.ToArray());

        var properties = typeof(Shop).GetProperties();
        foreach (var property in properties.Where(p => !membersToInclude.Contains(p.Name))) Assert.False(inclusivePartialValueOfShop.IncludesMember(property.Name));
        foreach (var excludedMember in membersToInclude) Assert.True(inclusivePartialValueOfShop.IncludesMember(excludedMember));
    }
    
    [Fact]
    public void IncludeShouldThrowExceptionIfTheExtendedInstanceIsNull()
    {
        object nullObject = null!;
        Assert.Throws<ArgumentNullException>(() => nullObject.Include("memberKey"));
    }

    [Fact]
    public void IncludeShouldInstantiateAnInclusivePartialValueSuccessfully()
    {
        var shopRandomizer = new ShopRandomizer();
        var shop = shopRandomizer.PrepareRandomValue();

        var partialShop = shop.Include(nameof(Shop.Address));
        Assert.IsType<InclusivePartialValue<Shop>>(partialShop);
        Assert.Same(shop, partialShop.Value);
    }

    [Fact]
    public void DynamicPartialValueShouldNotBeInstantiatedIfTheProvidedArgumentsAreInvalid()
    {
        Assert.Throws<ArgumentNullException>(() => new DynamicPartialValue<object>(null!, null!));
        Assert.Throws<ArgumentNullException>(() => new DynamicPartialValue<object>(new object(), null!));
        Assert.Throws<ArgumentNullException>(() => new DynamicPartialValue<object>(null!, _ => true));
    }
}