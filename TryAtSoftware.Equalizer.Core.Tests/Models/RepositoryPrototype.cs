namespace TryAtSoftware.Equalizer.Core.Tests.Models;

using System.Collections.Generic;

public class RepositoryPrototype
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> CommitMessages { get; set; }
}