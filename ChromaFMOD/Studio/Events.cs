using System.Runtime.InteropServices;
using ChromaFMOD.Core;

namespace ChromaFMOD.Studio;

public static class Events
{
    public delegate RESULT EventCallback(EVENT_CALLBACK_TYPE type, EventInstance eventInstance, object? parameter);

    public static EVENT_CALLBACK ManagedCallbackToNative(EventCallback callback)
    {
        return (e, i, p) => callback(e, new EventInstance(i), e switch
        {
            EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND => Marshal.PtrToStructure<PROGRAMMER_SOUND_PROPERTIES>(p),
            EVENT_CALLBACK_TYPE.DESTROY_PROGRAMMER_SOUND => Marshal.PtrToStructure<PROGRAMMER_SOUND_PROPERTIES>(p),
            EVENT_CALLBACK_TYPE.PLUGIN_CREATED => Marshal.PtrToStructure<PLUGIN_INSTANCE_PROPERTIES>(p),
            EVENT_CALLBACK_TYPE.PLUGIN_DESTROYED => Marshal.PtrToStructure<PLUGIN_INSTANCE_PROPERTIES>(p),
            EVENT_CALLBACK_TYPE.TIMELINE_MARKER => Marshal.PtrToStructure<TIMELINE_MARKER_PROPERTIES>(p),
            EVENT_CALLBACK_TYPE.TIMELINE_BEAT => Marshal.PtrToStructure<TIMELINE_BEAT_PROPERTIES>(p),
            EVENT_CALLBACK_TYPE.SOUND_PLAYED => new Sound(p),
            EVENT_CALLBACK_TYPE.SOUND_STOPPED => new Sound(p),
            EVENT_CALLBACK_TYPE.START_EVENT_COMMAND => new EventInstance(p),
            EVENT_CALLBACK_TYPE.NESTED_TIMELINE_BEAT => Marshal.PtrToStructure<TIMELINE_NESTED_BEAT_PROPERTIES>(p),
            _ => null
        });
    }
}