using Native = FMOD.Studio.Native;

namespace ChromaFMOD.Studio;

public class EventInstance : DisposableResource
{
    protected IntPtr Handle { get; set; }

    public bool IsValid => Handle != IntPtr.Zero;

    public VolumeInfo Volume
    {
        get
        {
            EnsureHandleValid();
            CallAndThrow(Native.FMOD_Studio_EventInstance_GetVolume(Handle, out var vol, out var finalVol));
            return new(vol, finalVol);
        }
        set
        {
            EnsureHandleValid();
            CallAndThrow(Native.FMOD_Studio_EventInstance_SetVolume(Handle, value.rawValue));
        }
    }

    public EventInstance(IntPtr instance)
    {
        Handle = instance;
    }

    public EventInstance(EventDescription description)
    {
        if (!description.IsValid)
            throw new FmodEventException("The referenced Event Description handle is not valid.");

        CallAndThrow(Native.FMOD_Studio_EventDescription_CreateInstance(description.Handle, out var newInstance));
        Handle = newInstance;
        EnsureHandleValid();
    }

    public void Start()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_Start(Handle));
    }

    public void Stop(STOP_MODE mode = STOP_MODE.IMMEDIATE, bool dispose = false)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_Stop(Handle, mode));
        if(dispose)
            Dispose();
    }

    public void SetParameter(string name, float value, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_SetParameterByName(Handle, name, value, ignoreSeekSpeed));
    }

    public void SetParameter(PARAMETER_ID id, float value, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_SetParameterByID(Handle, id, value, ignoreSeekSpeed));
    }

    public void SetParameterWithLabel(string name, string label, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_SetParameterByNameWithLabel(Handle, name, label, ignoreSeekSpeed));
    }

    public void SetParameterWithLabel(PARAMETER_ID id, string label, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_SetParameterByIDWithLabel(Handle, id, label, ignoreSeekSpeed));
    }

    public void SetCallback(Events.EventCallback callback, EVENT_CALLBACK_TYPE type)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_EventInstance_SetCallback(Handle, Events.ManagedCallbackToNative(callback), type));
    }
    
    protected void EnsureHandleValid()
    {
        if (!IsValid)
            throw new FmodEventException("This event instance handle is not valid, it may have been destroyed.");
    }
    
    protected override void FreeNativeResources()
    {
        CallAndThrow(Native.FMOD_Studio_EventInstance_Release(Handle));
    }
}