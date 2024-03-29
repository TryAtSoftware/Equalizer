﻿namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Tests.Models.VersionControl;
using TryAtSoftware.Equalizer.Core.Tests.Profiles;
using Xunit;

public class EqualizationTests
{
    [Fact]
    public void EqualizerShouldBeInstantiatedSuccessfully()
    {
        var equalizer = PrepareEqualizer();
        Assert.NotNull(equalizer);
    }

    [Fact]
    public void ProfileProviderShouldBeAddedSuccessfully()
    {
        var equalizer = new Equalizer();
        var profileProvider = TestsCompanion.MockEqualizationProfileProvider();
        equalizer.AddProfileProvider(profileProvider);

        var registeredProfileProvider = Assert.Single(equalizer.CustomProfileProviders);
        Assert.Same(profileProvider, registeredProfileProvider);
    }

    [Fact]
    public void InvalidProfileProviderShouldNotBeAddedSuccessfully()
    {
        var equalizer = new Equalizer();
        Assert.Throws<ArgumentNullException>(() => equalizer.AddProfileProvider(null!));
    }

    [Fact]
    public void EqualityShouldBeAssertedSuccessfullyForLogicallyEqualEntities()
    {
        var repositoryPrototype = PrepareRepositoryPrototype();
        var repository = new CodeRepository();
        PrepareRepository(repository);

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(repositoryPrototype, repository);
    }

    [Theory]
    [MemberData(nameof(GetChanges))]
    public void EqualityShouldBeAssertedSuccessfullyForLogicallyUnequalEntities(Action<CodeRepository> change)
    {
        Assert.NotNull(change);
        var repositoryPrototype = PrepareRepositoryPrototype();
        var repository = new CodeRepository();
        PrepareRepository(repository);

        change(repository);

        var equalizer = PrepareEqualizer();
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertEquality(repositoryPrototype, repository));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(10, 10)]
    public void EqualityShouldBeAssertedSuccessfullyForKnownValues(object? expected, object? actual)
    {
        var equalizer = new Equalizer();
        equalizer.AssertEquality(expected, actual);
    }
    
    [Fact]
    public void InequalityShouldBeAssertedSuccessfullyForLogicallyEqualEntities()
    {
        var repositoryPrototype = PrepareRepositoryPrototype();
        var repository = new CodeRepository();
        PrepareRepository(repository);

        var equalizer = PrepareEqualizer();
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertInequality(repositoryPrototype, repository));
    }

    [Theory]
    [MemberData(nameof(GetChanges))]
    public void InequalityShouldBeAssertedSuccessfullyForLogicallyUnequalEntities(Action<CodeRepository> change)
    {
        Assert.NotNull(change);
        var repositoryPrototype = PrepareRepositoryPrototype();
        var repository = new CodeRepository();
        PrepareRepository(repository);

        change(repository);

        var equalizer = PrepareEqualizer();
        equalizer.AssertInequality(repositoryPrototype, repository);
    }

    [Theory]
    [InlineData(null, "val")]
    [InlineData("val", null)]
    [InlineData(10, 10.0)]
    public void InequalityShouldBeAssertedSuccessfullyForKnownValues(object? expected, object? actual)
    {
        var equalizer = new Equalizer();
        equalizer.AssertInequality(expected, actual);
    }

    [Fact]
    public void EqualizationShouldIgnoreMembersThatAreNotConfigured()
    {
        var repositoryPrototype = PrepareRepositoryPrototype();
        var extendedRepository = new ExtendedCodeRepository();
        PrepareRepository(extendedRepository);
        extendedRepository.CreationTime = DateTime.Today;

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(repositoryPrototype, extendedRepository);
    }

    public static IEnumerable<object[]> GetChanges()
    {
        yield return new object[] { new Action<CodeRepository>(rp => rp.Id = 0) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.OrganizationId = 56) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Name = "Different name") };
        yield return new object[] { new Action<CodeRepository>(rp => rp.InternalName = "Test name") };
        yield return new object[] { new Action<CodeRepository>(rp => rp.InternalName = null) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.InternalName = string.Empty) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.InternalName = "   ") };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Description = "Different description") };
        yield return new object[] { new Action<CodeRepository>(rp => rp.InitialCommits = new[] { "Different commits" }) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.SubsequentCommits = new[] { "Some commits" }) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Likes = -1) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Likes = 0) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Likes = 1) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Likes = 2) };
        yield return new object[] { new Action<CodeRepository>(rp => rp.Likes = 1200) };
    }

    private static Equalizer PrepareEqualizer()
    {
        var equalizer = new Equalizer();
        var profilesProvider = new DedicatedProfileProvider();
        profilesProvider.AddProfile(new CodeRepositoryEqualizationProfile());
        equalizer.AddProfileProvider(profilesProvider);
        return equalizer;
    }

    private static CodeRepositoryPrototype PrepareRepositoryPrototype()
    {
        var repositoryPrototype = new CodeRepositoryPrototype
        {
            Name = "Test name",
            Description = "Test description",
            CommitMessages = PrepareInitialCommits()
        };

        return repositoryPrototype;
    }

    private static void PrepareRepository(CodeRepository codeRepository)
    {
        codeRepository.Id = 1;
        codeRepository.InternalName = "internal_test_name";
        codeRepository.Name = "Test name";
        codeRepository.Description = "Test description";
        codeRepository.OrganizationId = 5;
        codeRepository.InitialCommits = PrepareInitialCommits();
        codeRepository.SubsequentCommits = Enumerable.Empty<string>();
        codeRepository.Likes = 6;
    }

    private static IEnumerable<string> PrepareInitialCommits() => new[] { "A", "B", "C", "merge A and B" };
}