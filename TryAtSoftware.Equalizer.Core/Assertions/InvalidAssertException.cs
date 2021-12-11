namespace TryAtSoftware.Equalizer.Core.Assertions;

using System;

public class InvalidAssertException : Exception
{
    public InvalidAssertException()
    {
    }

    public InvalidAssertException(string message) : base(message)
    {
    }

    public InvalidAssertException(string message, Exception innerException) : base(message, innerException)
    {
    }
}