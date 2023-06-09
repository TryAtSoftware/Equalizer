namespace TryAtSoftware.Equalizer.Core.Interfaces;

using System;

/// <summary>
/// An interface defining the structure of a component responsible for exposing additional information about the equalization process.
/// </summary>
public interface IEqualizationOptions
{
    /// <summary>
    /// Gets or sets the type of the `expected` value from the top-level equalization.
    /// </summary>
    Type ExpectedType { get; }
    
    /// <summary>
    /// Gets or sets the type of the `actual` value from the top-level equalization.
    /// </summary>
    Type ActualType { get; }

    /// <summary>
    /// Use this method to validate the equality of two other values as part of the current equalization process.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    /// <returns>Returns a subsequently built <see cref="IEqualizationResult"/> instance containing information about the additionally executed equalization.</returns>
    IEqualizationResult Equalize(object? expected, object? actual);

    /// <summary>
    /// Use this method to validate the inequality of two other values as part of the current equalization process.
    /// </summary>
    /// <param name="expected">The expected object instance.</param>
    /// <param name="actual">The actual object instance.</param>
    /// <returns>Returns a subsequently built <see cref="IEqualizationResult"/> instance containing information about the additionally executed equalization.</returns>
    IEqualizationResult Differentiate(object? expected, object? actual);
}