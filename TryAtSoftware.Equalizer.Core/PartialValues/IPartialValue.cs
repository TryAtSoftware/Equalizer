namespace TryAtSoftware.Equalizer.Core.PartialValues;

public interface IPartialValue
{
    /// <summary>
    /// Gets the originally represented value. 
    /// </summary>
    object Value { get; }
    
    /// <summary>
    /// Use this method to determine whether or not a member (identified by the provided <paramref name="memberKey"/> key) is included within this partial representation of the underlying value.
    /// </summary>
    /// <param name="memberKey">The key of the member to check.</param>
    /// <returns>Returns a value indicating whether or not the requested member is included within this partial representation of the underlying value.</returns>
    bool IncludesMember(string memberKey);
}

/// <summary>
/// An interface defining the structure of a component responsible for the partial representation of a value.
/// </summary>
/// <typeparam name="T">The type of the represented value.</typeparam>
public interface IPartialValue<out T> : IPartialValue
    where T : notnull
{
    /// <summary>
    /// Gets the originally represented value. 
    /// </summary>
    new T Value { get; }
}