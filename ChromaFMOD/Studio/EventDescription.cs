using Native = FMOD.Studio.Native;
using EventCallback = ChromaFMOD.Studio.Events.EventCallback;

namespace ChromaFMOD.Studio;

public class EventDescription : DisposableResource
{
    public IntPtr Handle { get; set; }

    public bool IsValid => Handle != IntPtr.Zero;

    public string Path
    {
        get
        {
            EnsureHandleValid();
            CallAndThrow(Native.FMOD_Studio_EventDescription_GetPath(Handle, out var path, 0, out var retrieved));
            return path;
        }
    }

    public EventDescription(IntPtr instance)
    {
        Handle = instance;
    }

    public void LoadSampleData(bool blocking = false)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventDescription_LoadSampleData(Handle));
    }

    public EventInstance NewInstance()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventDescription_CreateInstance(Handle, out var instance));
        return new EventInstance(instance);
    }

    public void ReleaseInstances()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventDescription_ReleaseAllInstances(Handle));
    }

    public void SetCallback(EventCallback callback, EVENT_CALLBACK_TYPE type)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventDescription_SetCallback(Handle, Events.ManagedCallbackToNative(callback), type));
    }
    
    protected void EnsureHandleValid()
    {
        if (!IsValid)
            throw new FmodEventException("This Event Description handle is not valid.");
    }
    
    protected override void FreeNativeResources()
    {
        ReleaseInstances();
    }
}