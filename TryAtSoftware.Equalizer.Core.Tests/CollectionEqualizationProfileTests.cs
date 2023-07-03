namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using Moq;
using TryAtSoftware.Equalizer.Core.Profiles;
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
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

        var equalizationResult = profile.Equalize(Array.Empty<int>(), Array.Empty<int>(), equalizationOptionsMock.Object);
        Assert.True(equalizationResult.IsSuccessful);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void CollectionsWithDifferentLengthShouldBeEqualizedUnsuccessfully(int firstArrayLength, int secondArrayLength)
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

        var equalizationResult = profile.Equalize(new int[firstArrayLength], new int[secondArrayLength], equalizationOptionsMock.Object);
        Assert.False(equalizationResult.IsSuccessful);
    }

    [Fact]
    public void CollectionsShouldBeEqualizedSuccessfully()
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

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

    [Fact]
    public void CollectionsWithDifferentElementsShouldBeEqualizedUnsuccessfully()
    {
        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        var indexOfFailure = RandomizationHelper.RandomInteger(0, elementsCount);

        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions((a, b) => (int)a == indexOfFailure && (int)b == indexOfFailure ? new UnsuccessfulEqualizationResult("Simulated failure") : new SuccessfulEqualizationResult());

        int[] firstArray = new int[elementsCount], secondArray = new int[elementsCount];
        for (var i = 0; i < elementsCount; i++) (firstArray[i], secondArray[i]) = (i, i);

        var equalizationResult = profile.Equalize(firstArray, secondArray, equalizationOptionsMock.Object);

        Assert.False(equalizationResult.IsSuccessful);
        Assert.False(string.IsNullOrWhiteSpace(equalizationResult.Message));

        for (var i = 0; i < elementsCount; i++)
        {
            var currentParameterValue = i;
            var expectedTimes = i <= indexOfFailure ? Times.Once() : Times.Never();
            equalizationOptionsMock.Verify(x => x.Equalize(currentParameterValue, currentParameterValue), expectedTimes);
        }
    }

    private static CollectionEqualizationProfile InstantiateProfile() => new ();
}