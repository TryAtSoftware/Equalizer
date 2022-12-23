namespace TryAtSoftware.Equalizer.Core.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;

public class GreaterThanValueTemplate
{
    public object? Value { get; }

    public GreaterThanValueTemplate(object? value)
    {
        this.Value = value;
    }

    public override string ToString() => $"> {this.Value.ToNormalizedString()}";
}