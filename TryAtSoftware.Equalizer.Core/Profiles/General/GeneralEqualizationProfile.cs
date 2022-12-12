namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Extensions.Collections;

public class GeneralEqualizationProfile<T> : BaseTypedEqualizationProfile<T, T>
{
    public override IEqualizationResult Equalize(T expected, T actual, IEqualizationOptions options)
    {
        foreach (var (memberName, valueSelector) in GeneralEqualizationMembersCache<T>.ValueAccessors.OrEmptyIfNull())
        {
            var expectedValue = valueSelector(expected);
            var actualValue = valueSelector(actual);

            var result = options.Equalize(expectedValue, actualValue);
            if (result.IsSuccessful) continue;

            var errorMessage = this.UnsuccessfulEqualization(expectedValue, actualValue, $"Values for the {memberName} member differ.");
            return new UnsuccessfulEqualizationResult(errorMessage.With(result));
        }

        return new SuccessfulEqualizationResult();
    }
}