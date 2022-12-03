namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.Complex.Rules;

using System;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;
using Xunit;

public class EqualizationRuleTests
{
    [Fact]
    public void EqualizationRuleShouldNotBeInstantiatedWithInvalidValues()
    {
        Assert.Throws<ArgumentNullException>(() => new EqualizationRule<int, int>(x => x, null!));
        Assert.Throws<ArgumentNullException>(() => new EqualizationRule<int, int>(null!, x => x));
    }
}