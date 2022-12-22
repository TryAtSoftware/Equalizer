namespace TryAtSoftware.Equalizer.Core.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;

public class GreaterThanOrEqualValueTemplate
{
    public object? Value { get; }

    public GreaterThanOrEqualValueTemplate(object? value)
    {
        this.Value = value;
    }

    public override string ToString() => $">= {this.Value.ToNormalizedString()}";
}