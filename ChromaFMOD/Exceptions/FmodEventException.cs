namespace ChromaFMOD.Exceptions;

public class FmodEventException : FmodException
{
    public FmodEventException()
    {
    }

    public FmodEventException(string? message) : base(message)
    {
    }

    public FmodEventException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}