namespace TryAtSoftware.Equalizer.Core.Tests.Models;

public interface IIdentifiable<out TKey>
{
    TKey Id { get; }
}