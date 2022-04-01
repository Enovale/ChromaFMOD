namespace ChromaFMOD.Exceptions;

public class FmodFileException : FmodException
{
    public FmodFileException()
    {
    }

    public FmodFileException(string? message) : base(message)
    {
    }

    public FmodFileException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}