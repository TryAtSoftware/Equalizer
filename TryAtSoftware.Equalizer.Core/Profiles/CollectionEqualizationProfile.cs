namespace TryAtSoftware.Equalizer.Core.Profiles;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class CollectionEqualizationProfile : BaseTypedEqualizationProfile<IEnumerable<object>, IEnumerable<object>>
{
    public override IEqualizationResult Equalize(IEnumerable<object> expected, IEnumerable<object> actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return new SuccessfulEqualizationResult();
        if (expected is null || actual is null) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        var iteratedExpected = expected.ToList();
        var iteratedActual = actual.ToList();
        if (iteratedExpected.Count != iteratedActual.Count) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual, "Counts do not match"));

        for (var i = 0; i < iteratedExpected.Count; i++)
        {
            var expectedElement = iteratedExpected[i];
            var actualElement = iteratedActual[i];

            var equalizationResult = options.Equalize(expectedElement, actualElement);
            if (equalizationResult.IsSuccessful) continue;

            var errorMessage = this.UnsuccessfulEqualization(expectedElement, actualElement, $"Element at index {i} do not match");
            return new UnsuccessfulEqualizationResult(errorMessage.With(equalizationResult));
        }

        return new SuccessfulEqualizationResult();
    }
}