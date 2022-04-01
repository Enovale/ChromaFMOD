namespace ChromaFMOD.Exceptions;

public class FmodStudioException : FmodSystemException
{
    public FmodStudioException()
    {
    }

    public FmodStudioException(string? message) : base(message)
    {
    }

    public FmodStudioException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}