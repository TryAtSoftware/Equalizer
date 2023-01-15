namespace TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;

#nullable disable
public class Product : BaseIdentifiable
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public double Price { get; set; }
}
#nullable restore