namespace TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An interface defining the structure of a component responsible for the orchestration of any equalization process.
/// </summary>
public interface IEqualizer
{
    /// <summary>
    /// Use this method to assert the equality between the two values.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    void AssertEquality(object? expected, object? actual);

    /// <summary>
    /// Use this method to assert the inequality between the two values.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    void AssertInequality(object? expected, object? actual);
}