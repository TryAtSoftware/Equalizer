namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.General;

using System;
using System.Reflection;
using TryAtSoftware.Equalizer.Core.Profiles.General;
using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Extensions.Reflection;
using Xunit;

public class GeneralEqualizationContextTests
{
    [Fact]
    public void GeneralEqualizationContextMustNotBeInstantiatedWithoutMembersBinder() => Assert.Throws<ArgumentNullException>(() => new GeneralEqualizationContext<Shop>(null!));

    [Fact]
    public void GeneralEqualizationContextMustNotBeInstantiatedWithInvalidMembersBinder()
    {
        var invalidMembersBinder = new MembersBinder<Product>(isValid: null, BindingFlags.Public | BindingFlags.Public);
        Assert.Throws<InvalidOperationException>(() => new GeneralEqualizationContext<Shop>(invalidMembersBinder));
    }
}