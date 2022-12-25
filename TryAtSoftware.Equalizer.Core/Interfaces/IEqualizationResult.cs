namespace TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An interface defining the structure of a component responsible for exposing information about the result of the equality validating process.
/// </summary>
public interface IEqualizationResult
{
    /// <summary>
    /// Gets a value indicating whether or not the equalization process was executed successfully.
    /// </summary>
    bool IsSuccessful { get; }
    
    /// <summary>
    /// Gets an additional message that can contain additional debug information about the execution of the equality validation process.
    /// In case of unsuccessful equalization, there should be stored information about the difference(s) between the values.
    /// </summary>
    string Message { get; }
}