namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

/// <summary>
/// An interface defining the structure of a component responsible for equalizing complex values.
/// </summary>
/// <typeparam name="TExpected">The type of the expected value.</typeparam>
/// <typeparam name="TActual">The type of the actual value.</typeparam>
public interface IComplexEqualizationProfile<in TExpected, in TActual>
{
    /// <summary>
    /// Gets a read-only collection containing all registered <see cref="IComplexEqualizationRule{TExpected,TActual}"/> instances.
    /// </summary>
    IReadOnlyCollection<IComplexEqualizationRule<TExpected, TActual>> Rules { get; }
}