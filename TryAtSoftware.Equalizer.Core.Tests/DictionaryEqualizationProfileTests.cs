namespace TryAtSoftware.Equalizer.Core.Tests;

using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Profiles;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class DictionaryEqualizationProfileTests
{
    [Fact]
    public void NullDictionariesShouldNotBeEqualized()
    {
        var profile = InstantiateProfile();
        Assert.False(profile.CanExecuteFor(new Dictionary<int, int>(), null));
        Assert.False(profile.CanExecuteFor(null, null));
        Assert.False(profile.CanExecuteFor(null, new Dictionary<int, int>()));
    }

    [Fact]
    public void EmptyDictionariesShouldBeEqualizedSuccessfully()
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

        var equalizationResult = profile.Equalize(new Dictionary<int, int>(), new Dictionary<int, int>(), equalizationOptionsMock.Object);
        Assert.True(equalizationResult.IsSuccessful);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void DictionariesWithDifferentLengthShouldBeEqualizedUnsuccessfully(int firstDictionaryLength, int secondDictionaryLength)
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();
        for (var i = 0; i < firstDictionaryLength; i++) firstDictionary[i] = i;
        for (var i = 0; i < secondDictionaryLength; i++) firstDictionary[i] = i;

        var equalizationResult = profile.Equalize(firstDictionary, secondDictionary, equalizationOptionsMock.Object);
        Assert.False(equalizationResult.IsSuccessful);
    }

    [Fact]
    public void DictionariesShouldBeEqualizedSuccessfully()
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();

        for (var i = 0; i < elementsCount; i++)
        {
            var element = RandomizationHelper.RandomInteger(int.MinValue, int.MaxValue);
            firstDictionary[i] = element;
            secondDictionary[i] = element;
        }

        var equalizationResult = profile.Equalize(firstDictionary, secondDictionary, equalizationOptionsMock.Object);
        Assert.True(equalizationResult.IsSuccessful);
    }

    [Fact]
    public void DictionariesWithDifferentElementsShouldBeEqualizedUnsuccessfully()
    {
        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        var indexOfFailure = RandomizationHelper.RandomInteger(0, elementsCount);

        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions((a, b) => (int)a == indexOfFailure && (int)b == indexOfFailure ? new UnsuccessfulEqualizationResult("Simulated failure") : new SuccessfulEqualizationResult());

        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();
        for (var i = 0; i < elementsCount; i++)
        {
            firstDictionary[i] = i;
            secondDictionary[i] = i;
        }

        var equalizationResult = profile.Equalize(firstDictionary, secondDictionary, equalizationOptionsMock.Object);

        Assert.False(equalizationResult.IsSuccessful);
        Assert.False(string.IsNullOrWhiteSpace(equalizationResult.Message));
    }

    [Fact]
    public void DictionariesWithDifferentKeysShouldBeEqualizedUnsuccessfully()
    {
        var profile = InstantiateProfile();
        var equalizationOptionsMock = TestsCompanion.MockEqualizationOptions();

        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();

        for (var i = 0; i < elementsCount; i++)
        {
            var element = RandomizationHelper.RandomInteger(int.MinValue, int.MaxValue);
            firstDictionary[i] = element;
            secondDictionary[i * 2] = element;
        }

        var equalizationResult = profile.Equalize(firstDictionary, secondDictionary, equalizationOptionsMock.Object);
        Assert.False(equalizationResult.IsSuccessful);
        Assert.False(string.IsNullOrWhiteSpace(equalizationResult.Message));
    }

    private static DictionaryEqualizationProfile InstantiateProfile() => new ();
}