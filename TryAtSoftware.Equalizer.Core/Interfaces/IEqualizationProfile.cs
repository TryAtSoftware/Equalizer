namespace TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationProfile
{
    bool CanExecuteFor(object a, object b);
    void AssertEquality(object expected, object actual, IEqualizationOptions options);
}

public interface IEqualizationProfile<in TPrincipal, in TActual> : IEqualizationProfile
{
    void AssertEquality(TPrincipal expected, TActual actual, IEqualizationOptions options);
}