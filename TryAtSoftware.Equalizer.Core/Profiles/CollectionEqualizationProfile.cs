namespace TryAtSoftware.Equalizer.Core.Profiles;

using System.Collections;
using System.Collections.Generic;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class CollectionEqualizationProfile : BaseTypedEqualizationProfile<IEnumerable, IEnumerable>
{
    public override IEqualizationResult Equalize(IEnumerable expected, IEnumerable actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return new SuccessfulEqualizationResult();
        if (expected is null || actual is null) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

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
                return new UnsuccessfulEqualizationResult(errorMessage.With(equalizationResult));
            }

            index++;
        }

        return new SuccessfulEqualizationResult();
    }
}