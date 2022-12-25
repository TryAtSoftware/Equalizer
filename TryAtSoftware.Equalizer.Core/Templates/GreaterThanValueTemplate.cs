namespace TryAtSoftware.Equalizer.Core.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;

/// <summary>
/// A "value template" interconnected with the `greater than a value` logical function.
/// </summary>
/// <remarks>In order to use this value template, the actual value should be implementing the <see cref="System.IComparable"/> interface.</remarks>
public class GreaterThanValueTemplate
{
    /// <summary>
    /// Gets the principal value for the subsequently executed comparison. The equality will be validated only if the `actual` value is greater than this one.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GreaterThanOrEqualValueTemplate"/> class.
    /// </summary>
    /// <param name="value">The value that should be set to the <see cref="Value"/> property.</param>
    public GreaterThanValueTemplate(object? value)
    {
        this.Value = value;
    }

    /// <inheritdoc />
    public override string ToString() => $"> {this.Value.ToNormalizedString()}";
}