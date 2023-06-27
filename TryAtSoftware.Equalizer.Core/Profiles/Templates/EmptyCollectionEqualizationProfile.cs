namespace TryAtSoftware.Equalizer.Core.Profiles.Templates;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Templates;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for defining the "is empty" logical function for collections.
/// </summary>
public class EmptyCollectionEqualizationProfile : BaseTypedEqualizationProfile<EmptyValueTemplate, IEnumerable<object>?>
{
    /// <inheritdoc />
    protected override IEqualizationResult Equalize(EmptyValueTemplate expected, IEnumerable<object>? actual, IEqualizationOptions options)
    {
        if (actual is null || !actual.Any()) return new SuccessfulEqualizationResult();
        return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));
    }

    /// <inheritdoc />
    protected override bool AllowNullActual => true;

    /// <inheritdoc />
    protected override bool IsInvariant => true;
}