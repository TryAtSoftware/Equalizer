namespace TryAtSoftware.Equalizer.Core.Tests.Profiles;

using TryAtSoftware.Equalizer.Core.Profiles.Complex;
using TryAtSoftware.Equalizer.Core.Templates;
using TryAtSoftware.Equalizer.Core.Tests.Models.VersionControl;

public class CodeRepositoryEqualizationProfile : ComplexEqualizationProfile<CodeRepositoryPrototype, CodeRepository>
{
    public CodeRepositoryEqualizationProfile()
    {
        this.Extend(new CommonIdentifiableEqualizationProfile<int>());

#pragma warning disable CS0618 // This directive should be removed soon. See issue #51
        this.Extend(new CodeRepositoryEqualizationProfilePart1());
        this.Extend(new CodeRepositoryEqualizationProfilePart2());
#pragma warning restore CS0618

        this.Equalize(rp => rp.Description, r => r.Description);
        this.Equalize(5, r => r.OrganizationId);
        this.Equalize(rp => rp.CommitMessages, r => r.InitialCommits);
        this.Equalize(Value.Empty, r => r.SubsequentCommits);
    }
}

file class CodeRepositoryEqualizationProfilePart1 : ComplexEqualizationProfile<CodeRepositoryPrototype, CodeRepository>
{
    public CodeRepositoryEqualizationProfilePart1()
    {
        this.Equalize(rp => rp.Name, r => r.Name);
        this.Differentiate(rp => rp.Name, r => r.InternalName);
        this.Differentiate(Value.Empty, r => r.InternalName);
    }
}

file class CodeRepositoryEqualizationProfilePart2 : ComplexEqualizationProfile<CodeRepositoryPrototype, CodeRepository>
{
    public CodeRepositoryEqualizationProfilePart2()
    {
        this.Equalize(Value.LowerThan(100), r => r.Likes);
        this.Equalize(Value.GreaterThanOrEqual(3), r => r.Likes);
    }
}