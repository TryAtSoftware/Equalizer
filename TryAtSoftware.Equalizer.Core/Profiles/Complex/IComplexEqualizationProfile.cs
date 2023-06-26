namespace TryAtSoftware.Equalizer.Core.Profiles.Complex;

using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

public interface IComplexEqualizationProfile<in TExpected, in TActual>
{
    IReadOnlyCollection<IComplexEqualizationRule<TExpected, TActual>> Rules { get; }
}