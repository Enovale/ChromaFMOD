namespace ChromaFMOD.Exceptions;

public class FmodPluginException : FmodException
{
    public FmodPluginException()
    {
    }

    public FmodPluginException(string? message) : base(message)
    {
    }

    public FmodPluginException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}