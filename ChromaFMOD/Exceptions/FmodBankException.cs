namespace ChromaFMOD.Exceptions;

public class FmodBankException : FmodException
{
    public FmodBankException()
    {
    }

    public FmodBankException(string? message) : base(message)
    {
    }

    public FmodBankException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}