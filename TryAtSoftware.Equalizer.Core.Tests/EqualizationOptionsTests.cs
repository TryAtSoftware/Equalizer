namespace TryAtSoftware.Equalizer.Core.Tests;

using System;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Interfaces;
using Xunit;

public class EqualizationOptionsTests
{
    [Fact]
    public void EqualizationOptionsShouldNotBeInstantiatedSuccessfullyWithInvalidParameters()
    {
        Assert.Throws<ArgumentNullException>(() => _ = new EqualizationOptions(null!, typeof(string), (_, _) => null!, (_, _) => null!));
        Assert.Throws<ArgumentNullException>(() => _ = new EqualizationOptions(typeof(string), null!, (_, _) => null!, (_, _) => null!));
        Assert.Throws<ArgumentNullException>(() => _ = new EqualizationOptions(typeof(string), typeof(string), null!, (_, _) => null!));
        Assert.Throws<ArgumentNullException>(() => _ = new EqualizationOptions(typeof(string), typeof(string), (_, _) => null!, null!));
    }

    [Fact]
    public void EqualizationOptionsShouldBeInstantiatedSuccessfullyWithValidParameters()
    {
        Type expectedType = typeof(string), actualType = typeof(List<string>);
        int equalizeInvocationsCount = 0, differentiateInvocationsCount = 0;

        var options = new EqualizationOptions(expectedType, actualType, EqualizeFunc, DifferentiateFunc);
        Assert.Same(expectedType, options.ExpectedType);
        Assert.Same(actualType, options.ActualType);

        options.Equalize("expected", "actual");
        options.Differentiate("expected", "actual");
        Assert.Equal(1, equalizeInvocationsCount);
        Assert.Equal(1, differentiateInvocationsCount);
        
        IEqualizationResult EqualizeFunc(object? expected, object? actual)
        {
            equalizeInvocationsCount++;
            return null!;
        }

        IEqualizationResult DifferentiateFunc(object? expected, object? actual)
        {
            differentiateInvocationsCount++;
            return null!;
        }
    }
}