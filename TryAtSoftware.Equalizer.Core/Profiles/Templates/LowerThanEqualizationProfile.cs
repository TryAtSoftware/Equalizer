namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using System;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for defining the "lower than a value" logical function.
/// </summary>
public class LowerThanEqualizationProfile : BaseTypedEqualizationProfile<LowerThanValueTemplate, IComparable>
{
    /// <inheritdoc />
    protected override IEqualizationResult Equalize(LowerThanValueTemplate expected, IComparable actual, IEqualizationOptions options)
    {
        if (actual.CompareTo(expected.Value) >= 0)
            return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        return new SuccessfulEqualizationResult();
    }

    /// <inheritdoc />
    protected override bool IsInvariant => true;
}