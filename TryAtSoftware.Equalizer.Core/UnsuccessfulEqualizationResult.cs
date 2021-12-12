namespace TryAtSoftware.Equalizer.Core;

using TryAtSoftware.Equalizer.Core.Interfaces;

public class UnsuccessfulEqualizationResult : IEqualizationResult
{
    public UnsuccessfulEqualizationResult()
        : this(string.Empty)
    {
    }

    public UnsuccessfulEqualizationResult(string message)
    {
        this.Message = message;
    }

    public bool IsSuccessful => false;
    public string Message { get; }
}