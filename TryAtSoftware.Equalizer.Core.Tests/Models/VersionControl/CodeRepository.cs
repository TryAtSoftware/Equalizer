namespace TryAtSoftware.Equalizer.Core.Tests.Models.VersionControl;

using System.Collections.Generic;

#nullable disable
public class CodeRepository : BaseIdentifiable
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string InternalName { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> InitialCommits { get; set; }
    public IEnumerable<string> SubsequentCommits { get; set; }
    public int Likes { get; set; }
}
#nullable restore