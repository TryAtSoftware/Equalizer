namespace TryAtSoftware.Equalizer.Core.Extensions;

using System;
using System.Text;
using TryAtSoftware.Equalizer.Core.Interfaces;
using TryAtSoftware.Extensions.Reflection;

/// <summary>
/// A static class containing helper extension methods for easier formatting of error messages.
/// </summary>
public static class ErrorMessagesExtensions
{
    /// <summary>
    /// Use this method to construct an error message describing the unsuccessful equalization state.
    /// </summary>
    /// <param name="source">The instance that validates the equality. In most cases this would be the equalization profile itself or another helper component.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    /// <param name="description">A description that will be appended to the error message. It is optional.</param>
    /// <typeparam name="T">The type of <paramref name="source"/>.</typeparam>
    /// <returns>Returns the subsequently generated error message.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the extended <paramref name="source"/> is null.</exception>
    public static string UnsuccessfulEqualization<T>(this T source, object? expected, object? actual, string? description = null)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        return UnsuccessfulEqualization(TypeNames.Get(source.GetType()), expected, actual, description);
    }

    /// <summary>
    /// Use this method to construct an error message describing the unsuccessful differentiation state.
    /// </summary>
    /// <param name="source">The instance that validates the inequality. In most cases this would be the equalization profile itself or another helper component.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value.</param>
    /// <param name="description">A description that will be appended to the error message. It is optional.</param>
    /// <typeparam name="T">The type of <paramref name="source"/>.</typeparam>
    /// <returns>Returns the subsequently generated error message.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the extended <paramref name="source"/> is null.</exception>
    public static string UnsuccessfulDifferentiation<T>(this T source, object? expected, object? actual, string? description = null)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        return UnsuccessfulDifferentiation(TypeNames.Get(source.GetType()), expected, actual, description);
    }

    /// <summary>
    /// Use this method to enhance an error message with the error message of another equalization result.
    /// </summary>
    /// <param name="errorMessage">The extended error message to enhance.</param>
    /// <param name="equalizationResult">The inner equalization result.</param>
    /// <returns>Returns the subsequently generated error message.</returns>
    public static string WithInner(this string? errorMessage, IEqualizationResult? equalizationResult)
    {
        var innerMessage = equalizationResult?.Message ?? string.Empty;
        if (string.IsNullOrWhiteSpace(errorMessage) && string.IsNullOrWhiteSpace(innerMessage)) return string.Empty;
        if (string.IsNullOrWhiteSpace(errorMessage)) return innerMessage;
        if (string.IsNullOrWhiteSpace(innerMessage)) return errorMessage;

        var stringBuilder = new StringBuilder(errorMessage.Length + innerMessage.Length + 15);
        stringBuilder.AppendLine(errorMessage);

        stringBuilder.AppendLine("Inner message:");
        stringBuilder.Append(innerMessage);

        return stringBuilder.ToString();
    }

    private static string UnsuccessfulEqualization(string typeName, object? expected, object? actual, string? description = null) => GenerateErrorMessage($"{typeName} could not assert the equality between the two values.", description, expected, actual);

    private static string UnsuccessfulDifferentiation(string typeName, object? expected, object? actual, string? description = null) => GenerateErrorMessage($"{typeName} could not assert the inequality between the two values.", description, expected, actual);

    private static string GenerateErrorMessage(string? header, string? description, object? expected, object? actual)
    {
        var stringBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(header)) stringBuilder.AppendLine(header);
        if (!string.IsNullOrWhiteSpace(description)) stringBuilder.AppendLine(description);

        stringBuilder.AppendLine($"Expected: {expected.ToNormalizedString()}");
        stringBuilder.AppendLine($"Actual: {actual.ToNormalizedString()}");

        return stringBuilder.ToString();
    }
}