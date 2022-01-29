namespace TryAtSoftware.Equalizer.Core.Profiles;

using System.Collections.Generic;
using System.Linq;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class DictionaryEqualizationProfile : BaseTypedEqualizationProfile<IDictionary<object, object>, IDictionary<object, object>>
{
    public override IEqualizationResult Equalize(IDictionary<object, object> expected, IDictionary<object, object> actual, IEqualizationOptions options)
    {
        if (expected is null && actual is null) return new SuccessfulEqualizationResult();
        if (expected is null || actual is null) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual));

        var iteratedExpected = expected.ToList();
        var iteratedActual = actual.ToList();
        if (iteratedExpected.Count != iteratedActual.Count) return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual, "Counts do not match"));

        foreach (var (key, expectedValue) in expected)
        {
            if (!actual.TryGetValue(key, out var actualValue))
                return new UnsuccessfulEqualizationResult(this.UnsuccessfulEqualization(expected, actual, $"Key {key} not found"));

            var equalizeValues = options.Equalize(expectedValue, actualValue);
            if (equalizeValues.IsSuccessful) continue;

            var errorMessage = this.UnsuccessfulEqualization(expected, actual, $"Elements identified by the key: \"{key}\" do not match");
            return new UnsuccessfulEqualizationResult(errorMessage.With(equalizeValues));
        }

        return new SuccessfulEqualizationResult();
    }
}