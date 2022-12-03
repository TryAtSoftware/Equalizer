namespace TryAtSoftware.Equalizer.Core.Tests.Profiles.Complex.Rules;

using System;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;
using Xunit;

public class DifferentiationRuleTests
{
    [Fact]
    public void DifferentiationRuleShouldNotBeInstantiatedWithInvalidValues()
    {
        Assert.Throws<ArgumentNullException>(() => new DifferentiationRule<int, int>(x => x, null!));
        Assert.Throws<ArgumentNullException>(() => new DifferentiationRule<int, int>(null!, x => x));
    }
}