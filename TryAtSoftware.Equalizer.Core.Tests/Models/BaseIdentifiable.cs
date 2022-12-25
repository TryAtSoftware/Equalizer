namespace TryAtSoftware.Equalizer.Core.Tests.Models;

public abstract class BaseIdentifiable : IIdentifiable<int>
{
    public int Id { get; set; }
}