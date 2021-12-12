namespace TryAtSoftware.Equalizer.Core.Extensions;

using System;
using System.Text;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Equalizer.Core.Profiles.Complex.Rules;

public static class ErrorMessages
{
    public static string UnsuccessfulEqualization(this IEqualizationProfile equalizationProfile, object expected, object actual)
    {
        if (equalizationProfile is null)
            throw new ArgumentNullException(nameof(equalizationProfile));

        return UnsuccessfulEqualization(equalizationProfile.GetType().Name, expected, actual);
    }

    public static string UnsuccessfulEqualization<TPrincipal, TSubordinate>(this IEqualizationRule<TPrincipal, TSubordinate> equalizationRule, object expected, object actual)
    {
        if (equalizationRule is null)
            throw new ArgumentNullException(nameof(equalizationRule));

        return UnsuccessfulEqualization(equalizationRule.GetType().Name, expected, actual);
    }

    public static string UnsuccessfulDifferentiation<TPrincipal, TSubordinate>(this IEqualizationRule<TPrincipal, TSubordinate> differentiationRule, object expected, object actual)
    {
        if (differentiationRule is null)
            throw new ArgumentNullException(nameof(differentiationRule));

        return UnsuccessfulDifferentiation(differentiationRule.GetType().Name, expected, actual);
    }

    private static string UnsuccessfulEqualization(string typeName, object expected, object actual)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{typeName} could not assert the equality between the two values.");
        stringBuilder.AppendLine($"Expected: {expected}");
        stringBuilder.AppendLine($"Actual: {actual}");

        return stringBuilder.ToString();
    }

    private static string UnsuccessfulDifferentiation(string typeName, object expected, object actual)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{typeName} could not assert the inequality between the two values.");
        stringBuilder.AppendLine($"Expected: {expected}");
        stringBuilder.AppendLine($"Actual: {actual}");

        return stringBuilder.ToString();
    }
}