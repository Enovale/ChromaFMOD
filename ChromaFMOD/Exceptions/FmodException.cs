namespace ChromaFMOD.Exceptions;

public class FmodException : Exception
{
    public FmodException()
    {
    }

    public FmodException(string? message) : base(message)
    {
    }

    public FmodException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}