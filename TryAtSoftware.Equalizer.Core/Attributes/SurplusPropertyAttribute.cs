namespace TryAtSoftware.Equalizer.Core.Attributes;

using System;

/// <summary>
/// An attribute that should be used to mark any property as 'surplus' meaning that its value is not a part of the primary information model.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SurplusPropertyAttribute : Attribute
{
}