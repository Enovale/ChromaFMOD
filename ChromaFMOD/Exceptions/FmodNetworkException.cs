namespace ChromaFMOD.Exceptions;

public class FmodNetworkException : FmodException
{
    public FmodNetworkException()
    {
    }

    public FmodNetworkException(string? message) : base(message)
    {
    }

    public FmodNetworkException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}