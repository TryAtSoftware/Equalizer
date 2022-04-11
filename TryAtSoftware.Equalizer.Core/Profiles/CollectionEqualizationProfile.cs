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

        var iteratedExpected = new List<object>();
        var iteratedActual = new List<object>();

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
            {
                iteratedExpected.Add(expectedEnumerator.Current);
                iteratedActual.Add(actualEnumerator.Current);
            }
        }

        for (var i = 0; i < iteratedExpected.Count; i++)
        {
            var expectedElement = iteratedExpected[i];
            var actualElement = iteratedActual[i];

            var equalizationResult = options.Equalize(expectedElement, actualElement);
            if (equalizationResult.IsSuccessful) continue;

            var errorMessage = this.UnsuccessfulEqualization(expected, actual, $"Element at index {i} do not match");
            return new UnsuccessfulEqualizationResult(errorMessage.With(equalizationResult));
        }

        return new SuccessfulEqualizationResult();
    }
}