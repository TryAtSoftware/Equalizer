namespace TryAtSoftware.Equalizer.Core.Extensions;

using System;
using System.Text;
using TryAtSoftware.Equalizer.Core.Interfaces;

internal static class ErrorMessages
{
    internal static string UnsuccessfulEqualization<T>(this T source, object expected, object actual, string description = null)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        return UnsuccessfulEqualization(source.GetType().Name, expected, actual, description);
    }

    public static string UnsuccessfulDifferentiation<T>(this T source, object expected, object actual, string description = null)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        return UnsuccessfulDifferentiation(source.GetType().Name, expected, actual, description);
    }

    internal static string With(this string errorMessage, IEqualizationResult equalizationResult)
    {
        var innerMessage = equalizationResult?.Message;
        if (string.IsNullOrWhiteSpace(errorMessage) && string.IsNullOrWhiteSpace(innerMessage)) return string.Empty;
        if (string.IsNullOrWhiteSpace(errorMessage)) return innerMessage;
        if (string.IsNullOrWhiteSpace(innerMessage)) return errorMessage;

        var stringBuilder = new StringBuilder(errorMessage.Length + innerMessage.Length + 15);
        stringBuilder.AppendLine(errorMessage);

        stringBuilder.AppendLine("Inner message:");
        stringBuilder.Append(innerMessage);

        return stringBuilder.ToString();
    }

    private static string UnsuccessfulEqualization(string typeName, object expected, object actual, string description = null) => GenerateErrorMessage($"{typeName} could not assert the equality between the two values.", description, expected, actual);

    private static string UnsuccessfulDifferentiation(string typeName, object expected, object actual, string description = null) => GenerateErrorMessage($"{typeName} could not assert the inequality between the two values.", description, expected, actual);

    private static string GenerateErrorMessage(string header, string description, object expected, object actual)
    {
        var stringBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(header)) stringBuilder.AppendLine(header);
        if (!string.IsNullOrWhiteSpace(description)) stringBuilder.AppendLine(description);

        stringBuilder.AppendLine($"Expected: {expected}");
        stringBuilder.AppendLine($"Actual: {actual}");

        return stringBuilder.ToString();
    }
}