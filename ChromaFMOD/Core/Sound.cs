namespace ChromaFMOD.Core;

public class Sound
{
    public IntPtr Handle { get; set; }

    public bool IsValid => Handle != IntPtr.Zero;

    public Sound(IntPtr instance)
    {
        Handle = instance;
    }
}