namespace TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;

public class Shop
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Area { get; set; }

    public Shop Duplicate() => new() { Name = this.Name, Address = this.Address, Area = this.Area };
}