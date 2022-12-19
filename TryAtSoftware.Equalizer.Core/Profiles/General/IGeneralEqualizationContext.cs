namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System;
using System.Collections.Generic;

public interface IGeneralEqualizationContext<T>
{
    IReadOnlyDictionary<string, Func<T, object>> ValueAccessors { get; }
}