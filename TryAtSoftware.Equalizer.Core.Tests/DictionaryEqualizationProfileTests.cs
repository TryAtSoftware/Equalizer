namespace TryAtSoftware.Equalizer.Core.Tests;

using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Interfaces;
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
        var equalizer = InstantiateEqualizer();
        equalizer.AssertEquality(new Dictionary<int, int>(), new Dictionary<int, int>());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void DictionariesWithDifferentLengthShouldBeEqualizedUnsuccessfully(int firstDictionaryLength, int secondDictionaryLength)
    {
        var equalizer = InstantiateEqualizer();

        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();
        for (var i = 0; i < firstDictionaryLength; i++) firstDictionary[i] = i;
        for (var i = 0; i < secondDictionaryLength; i++) firstDictionary[i] = i;

        equalizer.AssertInequality(firstDictionary, secondDictionary);
    }

    [Fact]
    public void DictionariesShouldBeEqualizedSuccessfully()
    {
        var equalizer = InstantiateEqualizer();

        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();

        for (var i = 0; i < elementsCount; i++)
        {
            var element = RandomizationHelper.RandomInteger(int.MinValue, int.MaxValue);
            firstDictionary[i] = element;
            secondDictionary[i] = element;
        }

        equalizer.AssertEquality(firstDictionary, secondDictionary);
    }

    [Fact]
    public void DictionariesWithDifferentElementsShouldBeEqualizedUnsuccessfully()
    {
        var equalizer = InstantiateEqualizer();

        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();

        for (var i = 0; i < elementsCount; i++)
        {
            var element = RandomizationHelper.RandomInteger(int.MinValue, int.MaxValue);
            firstDictionary[i] = element;
            secondDictionary[i] = element + 1;
        }

        equalizer.AssertInequality(firstDictionary, secondDictionary);
    }

    [Fact]
    public void DictionariesWithDifferentKeysShouldBeEqualizedUnsuccessfully()
    {
        var equalizer = InstantiateEqualizer();

        var elementsCount = RandomizationHelper.RandomInteger(3, 10);
        Dictionary<int, int> firstDictionary = new (), secondDictionary = new ();

        for (var i = 0; i < elementsCount; i++)
        {
            var element = RandomizationHelper.RandomInteger(int.MinValue, int.MaxValue);
            firstDictionary[i] = element;
            secondDictionary[i * 2] = element;
        }

        equalizer.AssertInequality(firstDictionary, secondDictionary);
    }

    [Fact]
    public void DictionariesWithFewerExpectedElementsShouldShowCorrectMessage()
    {
        var profile = InstantiateProfile();
        var equalizationOptions = TestsCompanion.MockEqualizationOptions((a, b) =>
        {
            // Make count comparisons fail when counts are different
            if (a is int && b is int && (int)a != (int)b)
                return new UnsuccessfulEqualizationResult("Counts differ");
            return new SuccessfulEqualizationResult();
        });

        var expected = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };
        var actual = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        var equalizationResult = profile.Equalize(expected, actual, equalizationOptions);

        Assert.False(equalizationResult.IsSuccessful);
        Assert.Contains("Expected count: 2", equalizationResult.Message);
        Assert.Contains("Actual count: > 2", equalizationResult.Message);
    }

    [Fact]
    public void DictionariesWithMoreExpectedElementsShouldShowCorrectMessage()
    {
        var profile = InstantiateProfile();
        var equalizationOptions = TestsCompanion.MockEqualizationOptions((a, b) =>
        {
            // Make count comparisons fail when counts are different
            if (a is int && b is int && (int)a != (int)b)
                return new UnsuccessfulEqualizationResult("Counts differ");
            return new SuccessfulEqualizationResult();
        });

        var expected = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };
        var actual = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };
        var equalizationResult = profile.Equalize(expected, actual, equalizationOptions);

        Assert.False(equalizationResult.IsSuccessful);
        Assert.Contains("Expected count: > 2", equalizationResult.Message);
        Assert.Contains("Actual count: 2", equalizationResult.Message);
    }

    private static IEqualizer InstantiateEqualizer() => new Equalizer();
    
    private static DictionaryEqualizationProfile InstantiateProfile() => new ();
}