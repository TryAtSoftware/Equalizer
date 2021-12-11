namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

public interface IValueProvider<in T>
{
    object GetValue(T instance);
}