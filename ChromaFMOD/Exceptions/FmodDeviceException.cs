namespace ChromaFMOD.Exceptions;

public class FmodDeviceException : FmodSoundException
{
    public FmodDeviceException()
    {
    }

    public FmodDeviceException(string? message) : base(message)
    {
    }

    public FmodDeviceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}