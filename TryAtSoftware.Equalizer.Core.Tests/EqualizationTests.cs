namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.ProfileProviders;
using TryAtSoftware.Equalizer.Core.Tests.Models;
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
    public void EqualizationShouldBeExecutedSuccessfullyForLogicallyEqualEntities()
    {
        var repository = new Repository
        {
            Name = "Test name",
            Description = "Test description",
            OrganizationId = 5
        };

        var repositoryPrototype = new RepositoryPrototype
        {
            Name = "Test name",
            Description = "Test description",
        };

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(repositoryPrototype, repository);
    }

    [Fact]
    public void EqualizationShouldBeExecutedSuccessfullyForLogicallyUnequalEntities()
    {
        var repository = new Repository
        {
            Name = "Test name",
            Description = "Test description",
            OrganizationId = 5
        };

        var repositoryPrototype = new RepositoryPrototype
        {
            Name = "Different test name",
            Description = "Different test description",
        };

        var equalizer = PrepareEqualizer();
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertEquality(repositoryPrototype, repository));
    }

    [Fact]
    public void EqualizationShouldIgnoreMembersThatAreNotConfigured()
    {

        var extendedRepository = new ExtendedRepository
        {
            Name = "Test name",
            Description = "Test description",
            OrganizationId = 5,
            CreationTime = DateTime.Today
        };

        var repositoryPrototype = new RepositoryPrototype
        {
            Name = "Test name",
            Description = "Test description",
        };

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(repositoryPrototype, extendedRepository);
    }

    private static Equalizer PrepareEqualizer()
    {
        var equalizer = new Equalizer();
        var profilesProvider = new DedicatedProfileProvider();
        profilesProvider.AddProfile(new RepositoryEqualizationProfile());
        equalizer.AddProfileProvider(profilesProvider);
        return equalizer;
    }
}