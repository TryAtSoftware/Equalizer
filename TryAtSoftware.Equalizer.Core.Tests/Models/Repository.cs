namespace TryAtSoftware.Equalizer.Core.Tests.Models;

using System.Collections.Generic;

public class Repository
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string InternalName { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> InitialCommits { get; set; }
    public IEnumerable<string> SubsequentCommits { get; set; }
    public int Likes { get; set; }
}