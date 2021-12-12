namespace TryAtSoftware.Equalizer.Core.Tests.Models;

public class Repository
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string InternalName { get; set; }
    public string Description { get; set; }
}