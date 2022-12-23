namespace TryAtSoftware.Equalizer.Core.Extensions;

internal static class FormattingExtensions
{
    internal static string ToNormalizedString(this object? obj)
    {
        if (obj is null) return "(null)";
        return obj.ToString();
    }
}