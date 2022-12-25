namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System;
using System.Collections.Generic;

/// <summary>
/// An interface defining the structure of a component exposing contextual information about the general equalization process.
/// </summary>
/// <typeparam name="T">The concrete entity type for the general equalization process.</typeparam>
public interface IGeneralEqualizationContext<T>
{
    /// <summary>
    /// Gets a mapping between a key and a function selecting a segment that should be equalized as a part of the general equalization process for the containing <typeparamref name="T"/> instance.
    /// </summary>
    IReadOnlyDictionary<string, Func<T, object>> ValueAccessors { get; }
}