namespace TryAtSoftware.Equalizer.Core.Tests.Mocks;

using System;
using Moq;
using TryAtSoftware.Equalizer.Core.Interfaces;

public static class EqualizationOptionMocks
{
    public static Mock<IEqualizationOptions> GetNew() => GetNew((_, _) => new SuccessfulEqualizationResult());

    public static Mock<IEqualizationOptions> GetNew(Func<object, object, IEqualizationResult> internalEqualization)
    {
        var equalizationOptionsMock = new Mock<IEqualizationOptions>();
        equalizationOptionsMock.Setup(eo => eo.Equalize(It.IsAny<object>(), It.IsAny<object>())).Returns(internalEqualization);
        equalizationOptionsMock.Setup(eo => eo.ExpectedType).Returns(typeof(object));
        equalizationOptionsMock.Setup(eo => eo.ActualType).Returns(typeof(object));
        return equalizationOptionsMock;
    }
}