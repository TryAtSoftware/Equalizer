namespace TryAtSoftware.Equalizer.Core.PartialValues;

/// <summary>
/// An interface defining the structure of a component responsible for the partial representation of a value.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public interface IPartialValue<out T>
    where T : notnull
{
    /// <summary>
    /// Gets the originally represented value. 
    /// </summary>
    T Value { get; }
    
    /// <summary>
    /// Use this method to determine whether or not a member (identified by the provided <paramref name="memberKey"/> key) is included within this partial representation of the underlying value.
    /// </summary>
    /// <param name="memberKey">The key of the member to check.</param>
    /// <returns>Returns a value indicating whether or not the requested member is included within this partial representation of the underlying value.</returns>
    bool IncludesMember(string memberKey);
}