namespace TryAtSoftware.Equalizer.Core.Tests.Models.VersionControl;

using System.Collections.Generic;

public class CodeRepositoryPrototype
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> CommitMessages { get; set; }
}