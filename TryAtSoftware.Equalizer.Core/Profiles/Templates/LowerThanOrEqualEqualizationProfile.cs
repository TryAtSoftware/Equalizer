namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using System;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for defining the "lower than or equal to a value" logical function.
/// </summary>
public class LowerThanOrEqualEqualizationProfile : BaseTypedEqualizationProfile<LowerThanOrEqualValueTemplate, IComparable>
{
    /// <inheritdoc />
    protected override IEqualizationResult Equalize(LowerThanOrEqualValueTemplate expected, IComparable actual, IEqualizationOptions options)
    {
        if (actual.CompareTo(expected.Value) > 0)
            return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        return new SuccessfulEqualizationResult();
    }

    /// <inheritdoc />
    protected override bool IsInvariant => true;
}