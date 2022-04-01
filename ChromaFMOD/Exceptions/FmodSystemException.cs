namespace ChromaFMOD.Exceptions;

public class FmodSystemException : FmodException
{
    public FmodSystemException()
    {
    }

    public FmodSystemException(string? message) : base(message)
    {
    }

    public FmodSystemException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}