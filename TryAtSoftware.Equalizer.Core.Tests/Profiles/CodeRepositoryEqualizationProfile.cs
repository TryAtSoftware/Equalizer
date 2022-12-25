﻿namespace TryAtSoftware.Equalizer.Core.Tests.Profiles;

using TryAtSoftware.Equalizer.Core.Profiles.Complex;
using TryAtSoftware.Equalizer.Core.Templates;
using TryAtSoftware.Equalizer.Core.Tests.Models.VersionControl;

public class CodeRepositoryEqualizationProfile : ComplexEqualizationProfile<CodeRepositoryPrototype, CodeRepository>
{
    public CodeRepositoryEqualizationProfile()
    {
        this.Extend(new CommonIdentifiableEqualizationProfile<CodeRepositoryPrototype, CodeRepository, int>());
        this.Equalize(rp => rp.Name, r => r.Name);
        this.Equalize(rp => rp.Description, r => r.Description);
        this.Equalize(5, r => r.OrganizationId);
        this.Differentiate(rp => rp.Name, r => r.InternalName);
        this.Differentiate(Value.Empty, r => r.InternalName);
        this.Equalize(rp => rp.CommitMessages, r => r.InitialCommits);
        this.Equalize(Value.Empty, r => r.SubsequentCommits);
        this.Equalize(Value.LowerThan(100), r => r.Likes);
        this.Equalize(Value.GreaterThanOrEqual(3), r => r.Likes);
    }
}