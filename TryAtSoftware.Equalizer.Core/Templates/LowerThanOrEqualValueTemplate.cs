namespace TryAtSoftware.Equalizer.Core.Templates;

public class LowerThanOrEqualValueTemplate
{
    public object Value { get; }

    public LowerThanOrEqualValueTemplate(object value)
    {
        this.Value = value;
    }

    public override string ToString() => $"<= {this.Value}";
}