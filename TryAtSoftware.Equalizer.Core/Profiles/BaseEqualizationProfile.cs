namespace TryAtSoftware.Equalizer.Core.Profiles;

using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public abstract class BaseEqualizationProfile<T1, T2> : IEqualizationProfile<T1, T2>
{
    public bool CanExecuteFor(object a, object b) => a is T1 && b is T2;

    public void AssertEquality(object expected, object actual, IEqualizationOptions options)
    {
        Assert.True(this.CanExecuteFor(expected, actual), $"The equalization profile {this.GetType().Name} cannot be executed for the given objects.");

        var typedExpected = Assert.OfType<T1>(expected, nameof(expected));
        var typedActual = Assert.OfType<T2>(actual, nameof(actual));
        Assert.NotNull(options, nameof(options));

        this.AssertEquality(typedExpected, typedActual, options);
    }

    public abstract void AssertEquality(T1 expected, T2 actual, IEqualizationOptions options);
}