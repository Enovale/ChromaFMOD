namespace ChromaFMOD.Exceptions;

public class FmodLockException : FmodException
{
    public FmodLockException()
    {
    }

    public FmodLockException(string? message) : base(message)
    {
    }

    public FmodLockException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}