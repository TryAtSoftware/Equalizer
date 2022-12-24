namespace TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;

public class Product : IIdentifiable<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public double Price { get; set; }
}