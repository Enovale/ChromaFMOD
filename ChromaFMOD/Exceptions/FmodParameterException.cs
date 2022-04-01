namespace ChromaFMOD.Exceptions;

public class FmodParameterException : FmodException
{
    public FmodParameterException()
    {
    }

    public FmodParameterException(string? message) : base(message)
    {
    }

    public FmodParameterException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}