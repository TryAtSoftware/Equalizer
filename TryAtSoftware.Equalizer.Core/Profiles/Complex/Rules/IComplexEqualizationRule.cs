namespace TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An interface defining the structure of a component responsible for equalizing a specific segment of a complex object.
/// </summary>
/// <typeparam name="TExpected">The type of the expected value.</typeparam>
/// <typeparam name="TActual">The type of the actual value.</typeparam>
public interface IComplexEqualizationRule<in TExpected, in TActual>
{
    /// <summary>
    /// Use this method to equalize the <paramref name="expected"/> and <paramref name="actual"/> values.
    /// </summary>
    /// <param name="expected">The expected <typeparamref name="TExpected"/> instance.</param>
    /// <param name="actual">The actual <typeparamref name="TActual"/> instance.</param>
    /// <param name="options">An <see cref="IEqualizationOptions"/> instance exposing additional information about the equalization process.</param>
    /// <returns>Returns a subsequently built <see cref="IEqualizationResult"/> instance containing information about the additionally executed equalization.</returns>
    IEqualizationResult Equalize(TExpected expected, TActual actual, IEqualizationOptions options);
}