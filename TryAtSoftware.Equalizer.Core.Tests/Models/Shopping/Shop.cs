﻿namespace TryAtSoftware.Equalizer.Core.Tests.Models.Shopping;

#nullable disable
public class Shop : BaseIdentifiable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Area { get; set; }

    public Shop Duplicate() => new() { Id = this.Id, Name = this.Name, Address = this.Address, Area = this.Area };
}
#nullable restore