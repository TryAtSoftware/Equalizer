namespace TryAtSoftware.Equalizer.Core.Templates;

/// <summary>
/// A static class used to make the syntax when using "value templates" more readable.
/// </summary>
public static class Value
{
    /// <summary>
    /// Gets an instance of the <see cref="EmptyValueTemplate"/> class.
    /// </summary>
    public static EmptyValueTemplate Empty => new ();

    /// <summary>
    /// Use this method to construct a value template interconnected with the "greater than a value" logical function.
    /// </summary>
    /// <param name="lowerBound">The primary value of the subsequently executed comparison.</param>
    /// <typeparam name="T">The concrete type of the primary value.</typeparam>
    /// <returns>Returns a new instance of the <see cref="GreaterThanValueTemplate"/> class.</returns>
    public static GreaterThanValueTemplate GreaterThan<T>(T lowerBound) => new (lowerBound);

    /// <summary>
    /// Use this method to construct a value template interconnected with the "lower than a value" logical function.
    /// </summary>
    /// <param name="upperBound">The primary value of the subsequently executed comparison.</param>
    /// <typeparam name="T">The concrete type of the primary value.</typeparam>
    /// <returns>Returns a new instance of the <see cref="LowerThanValueTemplate"/> class.</returns>
    public static LowerThanValueTemplate LowerThan<T>(T upperBound) => new (upperBound);

    /// <summary>
    /// Use this method to construct a value template interconnected with the "greater than or equal to a value" logical function.
    /// </summary>
    /// <param name="lowerBound">The primary value of the subsequently executed comparison.</param>
    /// <typeparam name="T">The concrete type of the primary value.</typeparam>
    /// <returns>Returns a new instance of the <see cref="GreaterThanOrEqualValueTemplate"/> class.</returns>
    public static GreaterThanOrEqualValueTemplate GreaterThanOrEqual<T>(T lowerBound) => new (lowerBound);

    /// <summary>
    /// Use this method to construct a value template interconnected with the "lower than or equal to a value" logical function.
    /// </summary>
    /// <param name="upperBound">The primary value of the subsequently executed comparison.</param>
    /// <typeparam name="T">The concrete type of the primary value.</typeparam>
    /// <returns>Returns a new instance of the <see cref="LowerThanOrEqualValueTemplate"/> class.</returns>
    public static LowerThanOrEqualValueTemplate LowerThanOrEqual<T>(T upperBound) => new (upperBound);
}