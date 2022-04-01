namespace ChromaFMOD.Exceptions;

public class FmodNotSupportedException : FmodException
{
    public FmodNotSupportedException()
    {
    }

    public FmodNotSupportedException(string? message) : base(message)
    {
    }

    public FmodNotSupportedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}