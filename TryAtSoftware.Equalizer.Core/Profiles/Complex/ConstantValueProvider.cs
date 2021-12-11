namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

public class ConstantValueProvider<T, TValue> : IValueProvider<T>
{
    private readonly TValue _value;

    public ConstantValueProvider(TValue value)
    {
        this._value = value;
    }

    public object GetValue(T instance) => this._value;
}