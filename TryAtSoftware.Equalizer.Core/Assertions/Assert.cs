namespace TryAtSoftware.Equalizer.Core.Assertions;

using System;
using System.Diagnostics.CodeAnalysis;

internal static class Assert
{
    public static void NotNull([NotNull] object? value, string variableName)
    {
        if (value is null)
            throw new InvalidAssertException($"The variable {variableName} was not expected to be null.");
    }

    public static void True([DoesNotReturnIf(false)] bool value, string message)
    {
        if (!value)
            throw new InvalidAssertException(message);
    }

    public static void True(bool value, Func<string> getErrorMessage)
    {
        if (value)
            return;

        var errorMessage = getErrorMessage();
        throw new InvalidAssertException(errorMessage);
    }

    public static T OfType<T>(object? value, string variableName)
    {
        if (value is not T typedValue)
            throw new InvalidAssertException($"The variable {variableName} is not of the expected type.");

        return typedValue;
    }
}