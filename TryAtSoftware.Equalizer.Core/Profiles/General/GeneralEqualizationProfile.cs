namespace TryAtSoftware.Equalizer.Core.Profiles.General;

using TryAtSoftware.Equalizer.Core;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Profiles;
using TryAtSoftware.Extensions.Collections;
using TryAtSoftware.Extensions.Reflection;

public class GeneralEqualizationProfile<T> : BaseTypedEqualizationProfile<T, T>
{
    public override IEqualizationResult Equalize(T expected, T actual, IEqualizationOptions options)
    {
        var membersBinder = GeneralEqualizationMembersCache<T>.Binder;

        foreach (var (memberName, member) in membersBinder.MemberInfos.OrEmptyIfNull())
        {
            var expectedValue = member.GetValue(expected);
            var actualValue = member.GetValue(actual);

            var result = options.Equalize(expectedValue, actualValue);
            if (!result.IsSuccessful) return new UnsuccessfulEqualizationResult($"{memberName}: {result.Message}");
        }

        return new SuccessfulEqualizationResult();
    }
}