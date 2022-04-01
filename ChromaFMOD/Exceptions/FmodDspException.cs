namespace ChromaFMOD.Exceptions;

public class FmodDspException : FmodException
{
    public FmodDspException()
    {
    }

    public FmodDspException(string? message) : base(message)
    {
    }

    public FmodDspException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}