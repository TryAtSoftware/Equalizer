namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using System.Linq;
using TryAtSoftware.Equalizer.Core.Extensions;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.PartialValues;
using TryAtSoftware.Extensions.Collections;

internal static class GeneralEqualizationExtensions
{
    internal static IEqualizationResult Equalize<TProfile, TEntity>(this TProfile equalizationProfile, TEntity expected, TEntity actual, IEqualizationOptions options, IGeneralEqualizationContext<TEntity> generalEqualizationContext)
        where TEntity : notnull
        => equalizationProfile.Equalize(expected.AsPartialValue(), actual, options, generalEqualizationContext);

    internal static IEqualizationResult Equalize<TProfile, TEntity>(this TProfile equalizationProfile, IPartialValue<TEntity> expected, TEntity actual, IEqualizationOptions options, IGeneralEqualizationContext<TEntity> generalEqualizationContext)
        where TEntity : notnull
    {
        foreach (var (memberName, valueSelector) in generalEqualizationContext.ValueAccessors.OrEmptyIfNull().Where(x => expected.IncludesMember(x.Key)))
        {
            var expectedValue = valueSelector(expected.Value);
            var actualValue = valueSelector(actual);

            var result = options.Equalize(expectedValue, actualValue);
            if (result.IsSuccessful) continue;

            var errorMessage = equalizationProfile.UnsuccessfulEqualization(expectedValue, actualValue, $"Values for the {memberName} member differ.");
            return new UnsuccessfulEqualizationResult(errorMessage.WithInner(result));
        }

        return new SuccessfulEqualizationResult();
    }
}