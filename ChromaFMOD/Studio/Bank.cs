using Native = FMOD.Studio.Native;

namespace ChromaFMOD.Studio;

public class Bank : DisposableResource
{
    public IntPtr Handle { get; set; }

    public bool IsValid => Handle != IntPtr.Zero;

    private EventDescription[]? _events;

    public EventDescription[] Events
    {
        get
        {
            EnsureHandleValid();
            if (_events == null)
            {
                CallAndThrow(Native.FMOD_Studio_Bank_GetEventCount(Handle, out var count));
                var arr = new IntPtr[count];
                _events = new EventDescription[count];
                CallAndThrow(Native.FMOD_Studio_Bank_GetEventList(Handle, arr, count, out _));
                for (var i = 0; i < arr.Length; i++)
                {
                    _events[i] = new EventDescription(arr[i]);
                }
            }

            return _events;
        }
    }

    public GUID ID
    {
        get
        {
            EnsureHandleValid();
            CallAndThrow(Native.FMOD_Studio_Bank_GetID(Handle, out var guid));
            return guid;
        }
    }

    public Bank(IntPtr handle)
    {
        Handle = handle;
    }

    public void LoadSampleData()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_Bank_LoadSampleData(Handle));
    }
    
    protected void EnsureHandleValid()
    {
        if (!IsValid)
            throw new FmodBankException("This Bank source handle is not valid.");
    }

    protected override void FreeNativeResources()
    {
        if (IsValid)
        {
            Native.FMOD_Studio_Bank_Unload(Handle);
            Handle = IntPtr.Zero;
        }
    }
}