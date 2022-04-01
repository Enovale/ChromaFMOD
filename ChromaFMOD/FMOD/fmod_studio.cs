/* ======================================================================================== */
/* FMOD Studio API - C# wrapper.                                                            */
/* Copyright (c), Firelight Technologies Pty, Ltd. 2004-2021.                               */
/*                                                                                          */
/* For more detail visit:                                                                   */
/* https://fmod.com/resources/documentation-api?version=2.0&page=page=studio-api.html       */
/* ======================================================================================== */

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
    public partial class STUDIO_VERSION
    {
#if !UNITY_2019_4_OR_NEWER
        public const string dll     = "fmodstudio";
#endif
    }

    public enum STOP_MODE : int
    {
        ALLOWFADEOUT,
        IMMEDIATE,
    }

    public enum LOADING_STATE : int
    {
        UNLOADING,
        UNLOADED,
        LOADING,
        LOADED,
        ERROR,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROGRAMMER_SOUND_PROPERTIES
    {
        public StringWrapper name;
        public IntPtr sound;
        public int subsoundIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TIMELINE_MARKER_PROPERTIES
    {
        public StringWrapper name;
        public int position;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TIMELINE_BEAT_PROPERTIES
    {
        public int bar;
        public int beat;
        public int position;
        public float tempo;
        public int timesignatureupper;
        public int timesignaturelower;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TIMELINE_NESTED_BEAT_PROPERTIES
    {
        public GUID eventid;
        public TIMELINE_BEAT_PROPERTIES properties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ADVANCEDSETTINGS
    {
        public int cbsize;
        public int commandqueuesize;
        public int handleinitialsize;
        public int studioupdateperiod;
        public int idlesampledatapoolsize;
        public int streamingscheduledelay;
        public IntPtr encryptionkey;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CPU_USAGE
    {
        public float update;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BUFFER_INFO
    {
        public int currentusage;
        public int peakusage;
        public int capacity;
        public int stallcount;
        public float stalltime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BUFFER_USAGE
    {
        public BUFFER_INFO studiocommandqueue;
        public BUFFER_INFO studiohandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BANK_INFO
    {
        public int size;
        public IntPtr userdata;
        public int userdatalength;
        public FILE_OPEN_CALLBACK opencallback;
        public FILE_CLOSE_CALLBACK closecallback;
        public FILE_READ_CALLBACK readcallback;
        public FILE_SEEK_CALLBACK seekcallback;
    }

    [Flags]
    public enum SYSTEM_CALLBACK_TYPE : uint
    {
        PREUPDATE = 0x00000001,
        POSTUPDATE = 0x00000002,
        BANK_UNLOAD = 0x00000004,
        LIVEUPDATE_CONNECTED = 0x00000008,
        LIVEUPDATE_DISCONNECTED = 0x00000010,
        ALL = 0xFFFFFFFF,
    }

    public delegate RESULT SYSTEM_CALLBACK(IntPtr system, SYSTEM_CALLBACK_TYPE type, IntPtr commanddata, IntPtr userdata);

    public enum PARAMETER_TYPE : int
    {
        GAME_CONTROLLED,
        AUTOMATIC_DISTANCE,
        AUTOMATIC_EVENT_CONE_ANGLE,
        AUTOMATIC_EVENT_ORIENTATION,
        AUTOMATIC_DIRECTION,
        AUTOMATIC_ELEVATION,
        AUTOMATIC_LISTENER_ORIENTATION,
        AUTOMATIC_SPEED,
        AUTOMATIC_SPEED_ABSOLUTE,
        AUTOMATIC_DISTANCE_NORMALIZED,
        MAX
    }

    [Flags]
    public enum PARAMETER_FLAGS : uint
    {
        READONLY      = 0x00000001,
        AUTOMATIC     = 0x00000002,
        GLOBAL        = 0x00000004,
        DISCRETE      = 0x00000008,
        LABELED       = 0x00000010,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PARAMETER_ID
    {
        public uint data1;
        public uint data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PARAMETER_DESCRIPTION
    {
        public StringWrapper name;
        public PARAMETER_ID id;
        public float minimum;
        public float maximum;
        public float defaultvalue;
        public PARAMETER_TYPE type;
        public PARAMETER_FLAGS flags;
        public GUID guid;
    }

    // This is only need for loading memory and given our C# wrapper LOAD_MEMORY_POINT isn't feasible anyway
    public enum LOAD_MEMORY_MODE : int
    {
        LOAD_MEMORY,
        LOAD_MEMORY_POINT,
    }

    public enum LOAD_MEMORY_ALIGNMENT : int
    {
        VALUE = 32
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SOUND_INFO
    {
        public IntPtr name_or_data;
        public MODE mode;
        public CREATESOUNDEXINFO exinfo;
        public int subsoundindex;

        public string name
        {
            get
            {
                using (StringHelper.ThreadSafeEncoding encoding = StringHelper.GetFreeHelper())
                {
                    return ((mode & (MODE.OPENMEMORY | MODE.OPENMEMORY_POINT)) == 0) ? encoding.stringFromNative(name_or_data) : String.Empty;
                }
            }
        }
    }

    public enum USER_PROPERTY_TYPE : int
    {
        INTEGER,
        BOOLEAN,
        FLOAT,
        STRING,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct USER_PROPERTY
    {
        public StringWrapper name;
        public USER_PROPERTY_TYPE type;
        private Union_IntBoolFloatString value;

        public int intValue() => type == USER_PROPERTY_TYPE.INTEGER ? value.intvalue : -1;
        public bool boolValue() => type == USER_PROPERTY_TYPE.BOOLEAN && value.boolvalue;
        public float floatValue() => type == USER_PROPERTY_TYPE.FLOAT   ? value.floatvalue : -1;
        public string stringValue() => type == USER_PROPERTY_TYPE.STRING  ? value.stringvalue : "";
    };

    [StructLayout(LayoutKind.Explicit)]
    struct Union_IntBoolFloatString
    {
        [FieldOffset(0)]
        public int intvalue;
        [FieldOffset(0)]
        public bool boolvalue;
        [FieldOffset(0)]
        public float floatvalue;
        [FieldOffset(0)]
        public StringWrapper stringvalue;
    }

    [Flags]
    public enum INITFLAGS : uint
    {
        NORMAL                  = 0x00000000,
        LIVEUPDATE              = 0x00000001,
        ALLOW_MISSING_PLUGINS   = 0x00000002,
        SYNCHRONOUS_UPDATE      = 0x00000004,
        DEFERRED_CALLBACKS      = 0x00000008,
        LOAD_FROM_UPDATE        = 0x00000010,
        MEMORY_TRACKING         = 0x00000020,
    }

    [Flags]
    public enum LOAD_BANK_FLAGS : uint
    {
        NORMAL                  = 0x00000000,
        NONBLOCKING             = 0x00000001,
        DECOMPRESS_SAMPLES      = 0x00000002,
        UNENCRYPTED             = 0x00000004,
    }

    [Flags]
    public enum COMMANDCAPTURE_FLAGS : uint
    {
        NORMAL                  = 0x00000000,
        FILEFLUSH               = 0x00000001,
        SKIP_INITIAL_STATE      = 0x00000002,
    }

    [Flags]
    public enum COMMANDREPLAY_FLAGS : uint
    {
        NORMAL                  = 0x00000000,
        SKIP_CLEANUP            = 0x00000001,
        FAST_FORWARD            = 0x00000002,
        SKIP_BANK_LOAD          = 0x00000004,
    }

    public enum PLAYBACK_STATE : int
    {
        PLAYING,
        SUSTAINING,
        STOPPED,
        STARTING,
        STOPPING,
    }

    public enum EVENT_PROPERTY : int
    {
        CHANNELPRIORITY,
        SCHEDULE_DELAY,
        SCHEDULE_LOOKAHEAD,
        MINIMUM_DISTANCE,
        MAXIMUM_DISTANCE,
        COOLDOWN,
        MAX
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PLUGIN_INSTANCE_PROPERTIES
    {
        public IntPtr name;
        public IntPtr dsp;
    }

    [Flags]
    public enum EVENT_CALLBACK_TYPE : uint
    {
        CREATED                  = 0x00000001,
        DESTROYED                = 0x00000002,
        STARTING                 = 0x00000004,
        STARTED                  = 0x00000008,
        RESTARTED                = 0x00000010,
        STOPPED                  = 0x00000020,
        START_FAILED             = 0x00000040,
        CREATE_PROGRAMMER_SOUND  = 0x00000080,
        DESTROY_PROGRAMMER_SOUND = 0x00000100,
        PLUGIN_CREATED           = 0x00000200,
        PLUGIN_DESTROYED         = 0x00000400,
        TIMELINE_MARKER          = 0x00000800,
        TIMELINE_BEAT            = 0x00001000,
        SOUND_PLAYED             = 0x00002000,
        SOUND_STOPPED            = 0x00004000,
        REAL_TO_VIRTUAL          = 0x00008000,
        VIRTUAL_TO_REAL          = 0x00010000,
        START_EVENT_COMMAND      = 0x00020000,
        NESTED_TIMELINE_BEAT     = 0x00040000,

        ALL                      = 0xFFFFFFFF,
    }

    public delegate RESULT EVENT_CALLBACK(EVENT_CALLBACK_TYPE type, IntPtr _event, IntPtr parameters);

    public delegate RESULT COMMANDREPLAY_FRAME_CALLBACK(IntPtr replay, int commandindex, float currenttime, IntPtr userdata);
    public delegate RESULT COMMANDREPLAY_LOAD_BANK_CALLBACK(IntPtr replay, int commandindex, GUID bankguid, IntPtr bankfilename, LOAD_BANK_FLAGS flags, out IntPtr bank, IntPtr userdata);
    public delegate RESULT COMMANDREPLAY_CREATE_INSTANCE_CALLBACK(IntPtr replay, int commandindex, IntPtr eventdescription, out IntPtr instance, IntPtr userdata);

    public enum INSTANCETYPE : int
    {
        NONE,
        SYSTEM,
        EVENTDESCRIPTION,
        EVENTINSTANCE,
        PARAMETERINSTANCE,
        BUS,
        VCA,
        BANK,
        COMMANDREPLAY,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMMAND_INFO
    {
        public StringWrapper commandname;
        public int parentcommandindex;
        public int framenumber;
        public float frametime;
        public INSTANCETYPE instancetype;
        public INSTANCETYPE outputtype;
        public UInt32 instancehandle;
        public UInt32 outputhandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_USAGE
    {
        public int exclusive;
        public int inclusive;
        public int sampledata;
    }

    public static class Native
    {
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_ParseID(byte[] idString, out GUID id);
        
        #region System
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_Create                  (out IntPtr system, uint headerversion);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool   FMOD_Studio_System_IsValid                 (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetAdvancedSettings     (IntPtr system, ref ADVANCEDSETTINGS settings);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetAdvancedSettings     (IntPtr system, out ADVANCEDSETTINGS settings);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_Initialize              (IntPtr system, int maxchannels, INITFLAGS studioflags, FMOD.INITFLAGS flags, IntPtr extradriverdata);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_Release                 (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_Update                  (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetCoreSystem           (IntPtr system, out IntPtr coresystem);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetEvent                (IntPtr system, byte[] path, out IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBus                  (IntPtr system, byte[] path, out IntPtr bus);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetVCA                  (IntPtr system, byte[] path, out IntPtr vca);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBank                 (IntPtr system, byte[] path, out IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetEventByID            (IntPtr system, ref GUID id, out IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBusByID              (IntPtr system, ref GUID id, out IntPtr bus);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetVCAByID              (IntPtr system, ref GUID id, out IntPtr vca);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBankByID             (IntPtr system, ref GUID id, out IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetSoundInfo            (IntPtr system, byte[] key, out SOUND_INFO info);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterDescriptionByName(IntPtr system, byte[] name, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterDescriptionByID(IntPtr system, PARAMETER_ID id, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterLabelByName (IntPtr system, byte[] name, int labelindex, IntPtr label, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterLabelByID   (IntPtr system, PARAMETER_ID id, int labelindex, IntPtr label, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterByID        (IntPtr system, PARAMETER_ID id, out float value, out float finalvalue);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetParameterByID        (IntPtr system, PARAMETER_ID id, float value, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetParameterByIDWithLabel   (IntPtr system, PARAMETER_ID id, string label, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetParametersByIDs      (IntPtr system, PARAMETER_ID[] ids, float[] values, int count, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterByName      (IntPtr system, string name, out float value, out float finalvalue);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetParameterByName      (IntPtr system, string name, float value, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetParameterByNameWithLabel (IntPtr system, string name, string label, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_LookupID                (IntPtr system, string path, out GUID id);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_LookupPath              (IntPtr system, ref GUID id, out string path, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetNumListeners         (IntPtr system, out int numlisteners);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetNumListeners         (IntPtr system, int numlisteners);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetListenerAttributes   (IntPtr system, int listener, out ATTRIBUTES_3D attributes, IntPtr zero);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetListenerAttributes   (IntPtr system, int listener, out ATTRIBUTES_3D attributes, out Vector3 attenuationposition);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetListenerAttributes   (IntPtr system, int listener, ref ATTRIBUTES_3D attributes, IntPtr zero);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetListenerAttributes   (IntPtr system, int listener, ref ATTRIBUTES_3D attributes, ref Vector3 attenuationposition);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetListenerWeight       (IntPtr system, int listener, out float weight);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetListenerWeight       (IntPtr system, int listener, float weight);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_LoadBankFile            (IntPtr system, string filename, LOAD_BANK_FLAGS flags, out IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_LoadBankMemory          (IntPtr system, IntPtr buffer, int length, LOAD_MEMORY_MODE mode, LOAD_BANK_FLAGS flags, out IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_LoadBankCustom          (IntPtr system, ref BANK_INFO info, LOAD_BANK_FLAGS flags, out IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_UnloadAll               (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_FlushCommands           (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_FlushSampleLoading      (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_StartCommandCapture     (IntPtr system, string filename, COMMANDCAPTURE_FLAGS flags);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_StopCommandCapture      (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_LoadCommandReplay       (IntPtr system, string filename, COMMANDREPLAY_FLAGS flags, out IntPtr replay);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBankCount            (IntPtr system, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBankList             (IntPtr system, IntPtr[] array, int capacity, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterDescriptionCount(IntPtr system, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetParameterDescriptionList(IntPtr system, [Out] PARAMETER_DESCRIPTION[] array, int capacity, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetCPUUsage             (IntPtr system, out CPU_USAGE usage, out FMOD.CPU_USAGE usage_core);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetBufferUsage          (IntPtr system, out BUFFER_USAGE usage);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_ResetBufferUsage        (IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetCallback             (IntPtr system, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetUserData             (IntPtr system, out IntPtr userdata);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_SetUserData             (IntPtr system, IntPtr userdata);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_System_GetMemoryUsage          (IntPtr system, out MEMORY_USAGE memoryusage);
        #endregion
        
        #region EventDescription
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool FMOD_Studio_EventDescription_IsValid                 (IntPtr eventdescription);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetID                 (IntPtr eventdescription, out GUID id);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetPath               (IntPtr eventdescription, out string path, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionCount(IntPtr eventdescription, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionByIndex(IntPtr eventdescription, int index, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionByName(IntPtr eventdescription, string name, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterDescriptionByID(IntPtr eventdescription, PARAMETER_ID id, out PARAMETER_DESCRIPTION parameter);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterLabelByIndex(IntPtr eventdescription, int index, int labelindex, IntPtr label, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterLabelByName(IntPtr eventdescription, string name, int labelindex, IntPtr label, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetParameterLabelByID (IntPtr eventdescription, PARAMETER_ID id, int labelindex, IntPtr label, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyCount  (IntPtr eventdescription, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyByIndex(IntPtr eventdescription, int index, out USER_PROPERTY property);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetUserProperty       (IntPtr eventdescription, string name, out USER_PROPERTY property);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetLength             (IntPtr eventdescription, out int length);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetMinMaxDistance     (IntPtr eventdescription, out float min, out float max);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetSoundSize          (IntPtr eventdescription, out float size);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_IsSnapshot            (IntPtr eventdescription, out bool snapshot);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_IsOneshot             (IntPtr eventdescription, out bool oneshot);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_IsStream              (IntPtr eventdescription, out bool isStream);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_Is3D                  (IntPtr eventdescription, out bool is3D);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_IsDopplerEnabled      (IntPtr eventdescription, out bool doppler);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_HasSustainPoint       (IntPtr eventdescription, out bool sustainPoint);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_CreateInstance        (IntPtr eventdescription, out IntPtr instance);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetInstanceCount      (IntPtr eventdescription, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetInstanceList       (IntPtr eventdescription, IntPtr[] array, int capacity, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_LoadSampleData        (IntPtr eventdescription);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_UnloadSampleData      (IntPtr eventdescription);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetSampleLoadingState (IntPtr eventdescription, out LOADING_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_ReleaseAllInstances   (IntPtr eventdescription);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_SetCallback           (IntPtr eventdescription, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_GetUserData           (IntPtr eventdescription, out IntPtr userdata);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventDescription_SetUserData           (IntPtr eventdescription, IntPtr userdata);
        #endregion
        
        #region EventInstance
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool   FMOD_Studio_EventInstance_IsValid                     (IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetDescription              (IntPtr _event, out IntPtr description);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetVolume                   (IntPtr _event, out float volume, IntPtr zero);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetVolume                   (IntPtr _event, out float volume, out float finalvolume);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetVolume                   (IntPtr _event, float volume);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetPitch                    (IntPtr _event, out float pitch, IntPtr zero);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetPitch                    (IntPtr _event, out float pitch, out float finalpitch);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetPitch                    (IntPtr _event, float pitch);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_Get3DAttributes             (IntPtr _event, out ATTRIBUTES_3D attributes);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_Set3DAttributes             (IntPtr _event, ref ATTRIBUTES_3D attributes);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetListenerMask             (IntPtr _event, out uint mask);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetListenerMask             (IntPtr _event, uint mask);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetProperty                 (IntPtr _event, EVENT_PROPERTY index, out float value);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetProperty                 (IntPtr _event, EVENT_PROPERTY index, float value);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetReverbLevel              (IntPtr _event, int index, out float level);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetReverbLevel              (IntPtr _event, int index, float level);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetPaused                   (IntPtr _event, out bool paused);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetPaused                   (IntPtr _event, bool paused);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_Start                       (IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_Stop                        (IntPtr _event, STOP_MODE mode);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetTimelinePosition         (IntPtr _event, out int position);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetTimelinePosition         (IntPtr _event, int position);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetPlaybackState            (IntPtr _event, out PLAYBACK_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetChannelGroup             (IntPtr _event, out IntPtr group);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetMinMaxDistance           (IntPtr _event, out float min, out float max);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_Release                     (IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_IsVirtual                   (IntPtr _event, out bool virtualstate);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetParameterByName          (IntPtr _event, string name, out float value, out float finalvalue);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetParameterByName          (IntPtr _event, string name, float value, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetParameterByNameWithLabel (IntPtr _event, string name, string label, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetParameterByID            (IntPtr _event, PARAMETER_ID id, out float value, out float finalvalue);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetParameterByID            (IntPtr _event, PARAMETER_ID id, float value, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetParameterByIDWithLabel   (IntPtr _event, PARAMETER_ID id, string label, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetParametersByIDs          (IntPtr _event, PARAMETER_ID[] ids, float[] values, int count, bool ignoreseekspeed);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_KeyOff                      (IntPtr _event);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetCallback                 (IntPtr _event, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);
        [DllImport (STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetUserData                 (IntPtr _event, out IntPtr userdata);
        [DllImport (STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_SetUserData                 (IntPtr _event, IntPtr userdata);
        [DllImport (STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetCPUUsage                 (IntPtr _event, out uint exclusive, out uint inclusive);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_EventInstance_GetMemoryUsage              (IntPtr _event, out MEMORY_USAGE memoryusage);
        #endregion

        #region Bus
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool   FMOD_Studio_Bus_IsValid              (IntPtr bus);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetID                (IntPtr bus, out GUID id);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetPath              (IntPtr bus, out string path, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetVolume            (IntPtr bus, out float volume, out float finalvolume);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_SetVolume            (IntPtr bus, float volume);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetPaused            (IntPtr bus, out bool paused);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_SetPaused            (IntPtr bus, bool paused);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetMute              (IntPtr bus, out bool mute);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_SetMute              (IntPtr bus, bool mute);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_StopAllEvents        (IntPtr bus, STOP_MODE mode);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_LockChannelGroup     (IntPtr bus);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_UnlockChannelGroup   (IntPtr bus);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetChannelGroup      (IntPtr bus, out IntPtr group);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetCPUUsage          (IntPtr bus, out uint exclusive, out uint inclusive);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetMemoryUsage       (IntPtr bus, out MEMORY_USAGE memoryusage);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_GetPortIndex         (IntPtr bus, out ulong index);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bus_SetPortIndex         (IntPtr bus, ulong index);
        #endregion
        
        #region VCA
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool   FMOD_Studio_VCA_IsValid       (IntPtr vca);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_VCA_GetID         (IntPtr vca, out GUID id);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_VCA_GetPath       (IntPtr vca, out string path, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_VCA_GetVolume     (IntPtr vca, out float volume, out float finalvolume);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_VCA_SetVolume     (IntPtr vca, float volume);
        #endregion
        
        #region Bank
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool   FMOD_Studio_Bank_IsValid                   (IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetID                     (IntPtr bank, out GUID id);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetPath                   (IntPtr bank, out string path, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_Unload                    (IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_LoadSampleData            (IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_UnloadSampleData          (IntPtr bank);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetLoadingState           (IntPtr bank, out LOADING_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetSampleLoadingState     (IntPtr bank, out LOADING_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetStringCount            (IntPtr bank, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetStringInfo             (IntPtr bank, int index, out GUID id, out string path, int size, out int retrieved);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetEventCount             (IntPtr bank, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetEventList              (IntPtr bank, IntPtr[] array, int capacity, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetBusCount               (IntPtr bank, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetBusList                (IntPtr bank, IntPtr[] array, int capacity, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetVCACount               (IntPtr bank, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetVCAList                (IntPtr bank, IntPtr[] array, int capacity, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_GetUserData               (IntPtr bank, out IntPtr userdata);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_Bank_SetUserData               (IntPtr bank, IntPtr userdata);
        #endregion

        #region CommandReplay
        [DllImport(STUDIO_VERSION.dll)]
        public static extern bool FMOD_Studio_CommandReplay_IsValid                    (IntPtr replay);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetSystem                (IntPtr replay, out IntPtr system);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetLength                (IntPtr replay, out float length);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetCommandCount          (IntPtr replay, out int count);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetCommandInfo           (IntPtr replay, int commandindex, out COMMAND_INFO info);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetCommandString         (IntPtr replay, int commandIndex, IntPtr buffer, int length);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetCommandAtTime         (IntPtr replay, float time, out int commandIndex);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SetBankPath              (IntPtr replay, string bankPath);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_Start                    (IntPtr replay);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_Stop                     (IntPtr replay);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SeekToTime               (IntPtr replay, float time);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SeekToCommand            (IntPtr replay, int commandIndex);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetPaused                (IntPtr replay, out bool paused);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SetPaused                (IntPtr replay, bool paused);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetPlaybackState         (IntPtr replay, out PLAYBACK_STATE state);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetCurrentCommand        (IntPtr replay, out int commandIndex, out float currentTime);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_Release                  (IntPtr replay);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SetFrameCallback         (IntPtr replay, COMMANDREPLAY_FRAME_CALLBACK callback);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SetLoadBankCallback      (IntPtr replay, COMMANDREPLAY_LOAD_BANK_CALLBACK callback);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SetCreateInstanceCallback(IntPtr replay, COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_GetUserData              (IntPtr replay, out IntPtr userdata);
        [DllImport(STUDIO_VERSION.dll)]
        public static extern RESULT FMOD_Studio_CommandReplay_SetUserData              (IntPtr replay, IntPtr userdata);
        #endregion
    }

} // FMOD
