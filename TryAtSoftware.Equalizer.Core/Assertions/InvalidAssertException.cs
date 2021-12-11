namespace TryAtSoftware.Equalizer.Core.Assertions;

using System;
using System.Runtime.Serialization;

[Serializable]
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

    protected InvalidAssertException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}