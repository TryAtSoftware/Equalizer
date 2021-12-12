namespace TryAtSoftware.Equalizer.Core.Tests.Profiles;

using System.Linq;
using TryAtSoftware.Equalizer.Core.Profiles.Complex;
using TryAtSoftware.Equalizer.Core.Tests.Models;

public class RepositoryEqualizationProfile : ComplexEqualizationProfile<RepositoryPrototype, Repository>
{
    public RepositoryEqualizationProfile()
    {
        this.Equalize(rp => rp.Name, r => r.Name);
        this.Equalize(rp => rp.Description, r => r.Description);
        this.Equalize(5, r => r.OrganizationId);
        this.Differentiate(rp => rp.Name, r => r.InternalName);
        this.Differentiate<int>(default, r => r.Id);
        this.Equalize(rp => rp.CommitMessages, r => r.InitialCommits);
        this.Equalize(Enumerable.Empty<string>(), r => r.SubsequentCommits);
    }
}