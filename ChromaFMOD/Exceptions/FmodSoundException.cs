namespace ChromaFMOD.Exceptions;

public class FmodSoundException : FmodException
{
    public FmodSoundException()
    {
    }

    public FmodSoundException(string? message) : base(message)
    {
    }

    public FmodSoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}