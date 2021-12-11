namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using JetBrains.Annotations;

public interface IEqualizationRule<in T1, [UsedImplicitly] T2>
{
    [NotNull]
    string MemberName { get; }

    [NotNull]
    IValueProvider<T1> ValueProvider { get; }
}