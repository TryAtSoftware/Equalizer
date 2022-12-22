namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System.Linq;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Extensions.Collections;

public class GeneralEqualizationProfile<T> : BaseTypedEqualizationProfile<T, T>
{
    private readonly IGeneralEqualizationContext<T> _generalEqualizationContext;

    public GeneralEqualizationProfile(IGeneralEqualizationContext<T>? generalEqualizationContext = null)
    {
        this._generalEqualizationContext = generalEqualizationContext ?? GeneralEqualizationContext<T>.Instance;
    }

    public override IEqualizationResult Equalize(T expected, T actual, IEqualizationOptions options)
    {
        foreach (var (memberName, valueSelector) in this._generalEqualizationContext.ValueAccessors.OrEmptyIfNull().Where(x => x.Value is not null))
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