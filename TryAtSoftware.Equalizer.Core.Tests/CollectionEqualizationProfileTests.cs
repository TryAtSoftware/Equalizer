namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using TryAtSoftware.Equalizer.Core.Profiles;
using TryAtSoftware.Equalizer.Core.Tests.Mocks;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class CollectionEqualizationProfileTests
{
    [Fact]
    public void NullCollectionsShouldNotBeEqualized()
    {
        var profile = InstantiateProfile();
        Assert.False(profile.CanExecuteFor(Array.Empty<int>(), null));
        Assert.False(profile.CanExecuteFor(null, null));
        Assert.False(profile.CanExecuteFor(null, Array.Empty<int>()));
    }

    [Fact]
    public void EmptyCollectionsShouldBeEqualizedSuccessfully()
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = EqualizationOptionMocks.GetNew();

        var equalizationResult = profile.Equalize(Array.Empty<int>(), Array.Empty<int>(), equalizationOptionsMock.Object);
        Assert.True(equalizationResult.IsSuccessful);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void CollectionWithDifferentLengthShouldBeEqualizedUnsuccessfully(int firstArrayLength, int secondArrayLength)
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = EqualizationOptionMocks.GetNew();

        var equalizationResult = profile.Equalize(new int[firstArrayLength], new int[secondArrayLength], equalizationOptionsMock.Object);
        Assert.False(equalizationResult.IsSuccessful);
    }

    [Fact]
    public void CollectionsShouldBeEqualizedSuccessfully()
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = EqualizationOptionMocks.GetNew();

        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        var firstArray = new int[elementsCount];
        var secondArray = new int[elementsCount];

        for (var i = 0; i < elementsCount; i++)
        {
            var element = RandomizationHelper.RandomInteger(int.MinValue, int.MaxValue);
            firstArray[i] = element;
            secondArray[i] = element;
        }

        var equalizationResult = profile.Equalize(firstArray, secondArray, equalizationOptionsMock.Object);
        Assert.True(equalizationResult.IsSuccessful);
    }

    private static CollectionEqualizationProfile InstantiateProfile() => new();
}