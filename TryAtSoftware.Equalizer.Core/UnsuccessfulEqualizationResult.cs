namespace TryAtSoftware.Equalizer.Core;

using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationResult"/> interface used to denote that the equality between any two values has not been validated successfully.
/// </summary>
public class UnsuccessfulEqualizationResult : IEqualizationResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnsuccessfulEqualizationResult"/> class.
    /// </summary>
    /// <param name="message">The value that should be set to the <see cref="Message"/> property.</param>
    public UnsuccessfulEqualizationResult(string? message = null)
    {
        this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
    }

    /// <inheritdoc />
    public bool IsSuccessful => false;

    /// <inheritdoc />
    public string Message { get; }
}