namespace TryAtSoftware.Equalizer.Core;

using TryAtSoftware.Equalizer.Core.Interfaces;

public class SuccessfulEqualizationResult : IEqualizationResult
{
    public bool IsSuccessful => true;
    public string Message => string.Empty;
}