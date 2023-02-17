namespace TryAtSoftware.Equalizer.Core.Profiles;

using System.Collections;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

/// <summary>
/// An implementation of the <see cref="IEqualizationProfile"/> interface responsible for equalizing two collections.
/// </summary>
public class CollectionEqualizationProfile : BaseTypedEqualizationProfile<IEnumerable, IEnumerable>
{
    /// <inheritdoc />
    protected override IEqualizationResult Equalize(IEnumerable expected, IEnumerable actual, IEqualizationOptions options)
    {
        var iteratedValues = new List<(object Expected, object Actual)>();

        var expectedEnumerator = expected.GetEnumerator();
        var actualEnumerator = actual.GetEnumerator();

        var canContinue = true;
        while (canContinue)
        {
            var hasMoreExpected = expectedEnumerator.MoveNext();
            var hasMoreActual = actualEnumerator.MoveNext();

            if (!hasMoreExpected && !hasMoreActual) canContinue = false;
            else if (hasMoreExpected != hasMoreActual) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual, "Counts do not match"));
            else
                iteratedValues.Add((expectedEnumerator.Current, actualEnumerator.Current));
        }

        var index = 0;
        foreach (var (expectedElement, actualElement) in iteratedValues)
        {
            var equalizationResult = options.Equalize(expectedElement, actualElement);
            if (!equalizationResult.IsSuccessful)
            {
                var errorMessage = this.UnsuccessfulEqualization(expected, actual, $"Element at index {index} do not match");
                return new UnsuccessfulEqualizationResult(errorMessage.WithInner(equalizationResult));
            }

            index++;
        }

        return new SuccessfulEqualizationResult();
    }
}