namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
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
        var repositoryPrototype = PrepareRepositoryPrototype();
        var repository = new Repository();
        PrepareRepository(repository);

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(repositoryPrototype, repository);
    }

    [Theory]
    [MemberData(nameof(GetChanges))]
    public void EqualizationShouldBeExecutedSuccessfullyForLogicallyUnequalEntities(Action<Repository> change)
    {
        Assert.NotNull(change);
        var repositoryPrototype = PrepareRepositoryPrototype();
        var repository = new Repository();
        PrepareRepository(repository);

        change(repository);

        var equalizer = PrepareEqualizer();
        Assert.Throws<InvalidAssertException>(() => equalizer.AssertEquality(repositoryPrototype, repository));
    }

    [Fact]
    public void EqualizationShouldIgnoreMembersThatAreNotConfigured()
    {
        var repositoryPrototype = PrepareRepositoryPrototype();
        var extendedRepository = new ExtendedRepository();
        PrepareRepository(extendedRepository);
        extendedRepository.CreationTime = DateTime.Today;

        var equalizer = PrepareEqualizer();
        equalizer.AssertEquality(repositoryPrototype, extendedRepository);
    }

    public static IEnumerable<object[]> GetChanges()
    {
        yield return new object[] { new Action<Repository>(rp => rp.Id = 0) };
        yield return new object[] { new Action<Repository>(rp => rp.OrganizationId = 56) };
        yield return new object[] { new Action<Repository>(rp => rp.Name = "Different name") };
        yield return new object[] { new Action<Repository>(rp => rp.InternalName = "Test name") };
        yield return new object[] { new Action<Repository>(rp => rp.Description = "Different description") };
        yield return new object[] { new Action<Repository>(rp => rp.InitialCommits = new[] { "Different commits" }) };
        yield return new object[] { new Action<Repository>(rp => rp.SubsequentCommits = new[] { "Some commits" }) };
    }

    private static Equalizer PrepareEqualizer()
    {
        var equalizer = new Equalizer();
        var profilesProvider = new DedicatedProfileProvider();
        profilesProvider.AddProfile(new RepositoryEqualizationProfile());
        equalizer.AddProfileProvider(profilesProvider);
        return equalizer;
    }

    private static RepositoryPrototype PrepareRepositoryPrototype()
    {
        var repositoryPrototype = new RepositoryPrototype
        {
            Name = "Test name",
            Description = "Test description",
            CommitMessages = PrepareInitialCommits()
        };

        return repositoryPrototype;
    }

    private static void PrepareRepository(Repository repository)
    {
        repository.Id = 1;
        repository.InternalName = "internal_test_name";
        repository.Name = "Test name";
        repository.Description = "Test description";
        repository.OrganizationId = 5;
        repository.InitialCommits = PrepareInitialCommits();
        repository.SubsequentCommits = Enumerable.Empty<string>();
    }

    private static IEnumerable<string> PrepareInitialCommits() => new[] { "A", "B", "C", "merge A and B" };
}