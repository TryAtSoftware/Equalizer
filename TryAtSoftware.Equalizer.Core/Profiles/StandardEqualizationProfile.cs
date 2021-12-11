namespace TryAtSoftware.Equalizer.Core.Profiles;

using System.Text;
using TryAtSoftware.Equalizer.Core.Assertions;
using TryAtSoftware.Equalizer.Core.Interfaces;

public class StandardEqualizationProfile : IEqualizationProfile
{
    public bool CanExecuteFor(object a, object b) => true;

    public void AssertEquality(object expected, object actual, IEqualizationOptions options)
        => Assert.True(Equals(expected, actual), () => GetErrorMessage(expected, actual));

    private static string GetErrorMessage(object expected, object actual)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{nameof(StandardEqualizationProfile)} could not assert the equality between the two values.");
        stringBuilder.AppendLine($"Expected: {expected}");
        stringBuilder.AppendLine($"Actual: {actual}");

        return stringBuilder.ToString();
    }
}