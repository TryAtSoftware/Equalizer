namespace TryAtSoftware.Equalizer.Core.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;

public class LowerThanValueTemplate
{
    public object? Value { get; }

    public LowerThanValueTemplate(object? value)
    {
        this.Value = value;
    }

    public override string ToString() => $"< {this.Value.ToNormalizedString()}";
}