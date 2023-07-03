namespace TryAtSoftware.Equalizer.Core.Profiles;

using System.Collections;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for equalizing two dictionaries.
/// </summary>
public class DictionaryEqualizationProfile : BaseTypedEqualizationProfile<IDictionary, IDictionary>
{
    /// <inheritdoc />
    protected override IEqualizationResult Equalize(IDictionary expected, IDictionary actual, IEqualizationOptions options)
    {
        var countEqualizationResult = options.Equalize(expected.Count, actual.Count);
        if (!countEqualizationResult.IsSuccessful)
        {
            var errorMessage = this.UnsuccessfulEqualization(expected, actual, "Counts do not match.");
            return new UnsuccessfulEqualizationResult(errorMessage.WithInner(countEqualizationResult));
        }

        foreach (var key in expected.Keys)
        {
            if (!actual.Contains(key)) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual, $"An entry for key {key} does not exist."));

            var equalizationResult = options.Equalize(expected[key], actual[key]);
            if (!equalizationResult.IsSuccessful)
            {
                var errorMessage = this.UnsuccessfulEqualization(expected, actual, $"Values for key {key} do not match");
                return new UnsuccessfulEqualizationResult(errorMessage.WithInner(equalizationResult));
            }
        }

        return new SuccessfulEqualizationResult();
    }
}