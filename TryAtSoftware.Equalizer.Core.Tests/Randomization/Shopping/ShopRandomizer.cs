namespace TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;

using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Primitives;

public class ShopRandomizer : ComplexRandomizer<Shop>
{
    public ShopRandomizer()
        : base(new GeneralInstanceBuilder<Shop>())
    {
        this.AddRandomizationRule(s => s.Id, new NumberRandomizer());
        this.AddRandomizationRule(s => s.Name, new StringRandomizer());
        this.AddRandomizationRule(s => s.Address, new StringRandomizer());
        this.AddRandomizationRule(s => s.Area, new NumberRandomizer());
    }
}