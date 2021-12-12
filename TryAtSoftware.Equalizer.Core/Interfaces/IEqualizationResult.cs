namespace TryAtSoftware.Equalizer.Core.Interfaces;

public interface IEqualizationResult
{
    bool IsSuccessful { get; }
    string Message { get; }
}