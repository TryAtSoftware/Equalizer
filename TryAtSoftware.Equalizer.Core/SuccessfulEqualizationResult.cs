namespace TryAtSoftware.Equalizer.Core;

using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationResult"/> interface used to denote that the equality between any two values has been validated successfully.
/// </summary>
public class SuccessfulEqualizationResult : IEqualizationResult
{
    /// <inheritdoc />
    public bool IsSuccessful => true;

    /// <inheritdoc />
    public string Message => string.Empty;
}