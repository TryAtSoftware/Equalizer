namespace TryAtSoftware.Equalizer.Core.Tests.Templates;

using TryAtSoftware.Equalizer.Core.Templates;
using TryAtSoftware.Randomizer.Core.Helpers;
using Xunit;

public class ValueTemplateTests
{
    [Fact]
    public void EmptyShouldNotBeNull() => Assert.NotNull(Value.Empty);
    
    [Fact]
    public void GreaterThanShouldReturnCorrectValues()
    {
        var number = RandomizationHelper.RandomInteger(0, 100);
        var greaterThanTemplate = Value.GreaterThan(number);

        Assert.NotNull(greaterThanTemplate);
        Assert.Equal(greaterThanTemplate.Value, number);
    }
    
    [Fact]
    public void GreaterThanOrEqualShouldReturnCorrectValues()
    {
        var number = RandomizationHelper.RandomInteger(0, 100);
        var greaterThanOrEqualTemplate = Value.GreaterThanOrEqual(number);

        Assert.NotNull(greaterThanOrEqualTemplate);
        Assert.Equal(greaterThanOrEqualTemplate.Value, number);
    }
    
    
    [Fact]
    public void LowerThanShouldReturnCorrectValues()
    {
        var number = RandomizationHelper.RandomInteger(0, 100);
        var lowerThanTemplate = Value.LowerThan(number);

        Assert.NotNull(lowerThanTemplate);
        Assert.Equal(lowerThanTemplate.Value, number);
    }
    
    [Fact]
    public void LowerThanOrEqualShouldReturnCorrectValues()
    {
        var number = RandomizationHelper.RandomInteger(0, 100);
        var lowerThanOrEqualTemplate = Value.LowerThanOrEqual(number);

        Assert.NotNull(lowerThanOrEqualTemplate);
        Assert.Equal(lowerThanOrEqualTemplate.Value, number);
    }
}
