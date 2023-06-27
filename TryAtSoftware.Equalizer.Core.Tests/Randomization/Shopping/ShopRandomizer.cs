namespace TryAtSoftware.Equalizer.Core.Tests.Randomization.Shopping;

using TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;
using TryAtSoftware.Randomizer.Core;
using TryAtSoftware.Randomizer.Core.Primitives;

public class ShopRandomizer : ComplexRandomizer<Shop>
{
    public ShopRandomizer()
        : base(new GeneralInstanceBuilder<Shop>())
    {
        this.Randomize(s => s.Id, new NumberRandomizer());
        this.Randomize(s => s.Name, new StringRandomizer());
        this.Randomize(s => s.Address, new StringRandomizer());
        this.Randomize(s => s.Area, new NumberRandomizer());
    }
}