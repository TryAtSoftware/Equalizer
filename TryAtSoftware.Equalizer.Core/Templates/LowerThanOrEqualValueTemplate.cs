namespace TryAtSoftware.Equalizer.Core.Templates;

using TryAtSoftware.Equalizer.Core.Extensions;

public class LowerThanOrEqualValueTemplate
{
    public object? Value { get; }

    public LowerThanOrEqualValueTemplate(object? value)
    {
        this.Value = value;
    }

    public override string ToString() => $"<= {this.Value.ToNormalizedString()}";
}