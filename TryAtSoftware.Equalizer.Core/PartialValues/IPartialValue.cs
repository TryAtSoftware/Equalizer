namespace TryAtSoftware.Equalizer.Core.PartialValues;

public interface IPartialValue<out T>
    where T : notnull
{
    T Value { get; }
    bool IncludesMember(string memberKey);
}