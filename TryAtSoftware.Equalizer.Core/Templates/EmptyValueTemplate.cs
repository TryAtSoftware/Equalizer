namespace TryAtSoftware.Equalizer.Core.Templates;

/// <summary>
/// A "value template" interconnected with the `is empty` logical function.
/// </summary>
/// <remarks>
/// There are two equalization profiles handling this "value template" - one working with strings and one working with collections.
/// </remarks>
public class EmptyValueTemplate
{
    /// <inheritdoc />
    public override string ToString() => "<empty>";
}