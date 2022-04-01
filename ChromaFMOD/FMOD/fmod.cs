/* ======================================================================================== */
/* FMOD Core API - C# wrapper.                                                              */
/* Copyright (c), Firelight Technologies Pty, Ltd. 2004-2021.                               */
/*                                                                                          */
/* For more detail visit:                                                                   */
/* https://fmod.com/resources/documentation-api?version=2.0&page=core-api.html              */
/* ======================================================================================== */

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Numerics;
using Chroma.Natives;

namespace FMOD
{
    /*
        FMOD version number.  Check this against FMOD::System::getVersion / System_GetVersion
        0xaaaabbcc -> aaaa = major version number.  bb = minor version number.  cc = development version number.
    */
    public partial class VERSION
    {
        public const int    number = 0x00020205;
#if !UNITY_2019_4_OR_NEWER
        public const string dll    = "fmod";
#endif
    }

    public class CONSTANTS
    {
        public const int MAX_CHANNEL_WIDTH = 32;
        public const int MAX_LISTENERS = 8;
        public const int REVERB_MAXINSTANCES = 4;
        public const int MAX_SYSTEMS = 8;
    }

    /*
        FMOD core types
    */
    public enum RESULT : int
    {
        OK,
        ERR_BADCOMMAND,
        ERR_CHANNEL_ALLOC,
        ERR_CHANNEL_STOLEN,
        ERR_DMA,
        ERR_DSP_CONNECTION,
        ERR_DSP_DONTPROCESS,
        ERR_DSP_FORMAT,
        ERR_DSP_INUSE,
        ERR_DSP_NOTFOUND,
        ERR_DSP_RESERVED,
        ERR_DSP_SILENCE,
        ERR_DSP_TYPE,
        ERR_FILE_BAD,
        ERR_FILE_COULDNOTSEEK,
        ERR_FILE_DISKEJECTED,
        ERR_FILE_EOF,
        ERR_FILE_ENDOFDATA,
        ERR_FILE_NOTFOUND,
        ERR_FORMAT,
        ERR_HEADER_MISMATCH,
        ERR_HTTP,
        ERR_HTTP_ACCESS,
        ERR_HTTP_PROXY_AUTH,
        ERR_HTTP_SERVER_ERROR,
        ERR_HTTP_TIMEOUT,
        ERR_INITIALIZATION,
        ERR_INITIALIZED,
        ERR_INTERNAL,
        ERR_INVALID_FLOAT,
        ERR_INVALID_HANDLE,
        ERR_INVALID_PARAM,
        ERR_INVALID_POSITION,
        ERR_INVALID_SPEAKER,
        ERR_INVALID_SYNCPOINT,
        ERR_INVALID_THREAD,
        ERR_INVALID_VECTOR,
        ERR_MAXAUDIBLE,
        ERR_MEMORY,
        ERR_MEMORY_CANTPOINT,
        ERR_NEEDS3D,
        ERR_NEEDSHARDWARE,
        ERR_NET_CONNECT,
        ERR_NET_SOCKET_ERROR,
        ERR_NET_URL,
        ERR_NET_WOULD_BLOCK,
        ERR_NOTREADY,
        ERR_OUTPUT_ALLOCATED,
        ERR_OUTPUT_CREATEBUFFER,
        ERR_OUTPUT_DRIVERCALL,
        ERR_OUTPUT_FORMAT,
        ERR_OUTPUT_INIT,
        ERR_OUTPUT_NODRIVERS,
        ERR_PLUGIN,
        ERR_PLUGIN_MISSING,
        ERR_PLUGIN_RESOURCE,
        ERR_PLUGIN_VERSION,
        ERR_RECORD,
        ERR_REVERB_CHANNELGROUP,
        ERR_REVERB_INSTANCE,
        ERR_SUBSOUNDS,
        ERR_SUBSOUND_ALLOCATED,
        ERR_SUBSOUND_CANTMOVE,
        ERR_TAGNOTFOUND,
        ERR_TOOMANYCHANNELS,
        ERR_TRUNCATED,
        ERR_UNIMPLEMENTED,
        ERR_UNINITIALIZED,
        ERR_UNSUPPORTED,
        ERR_VERSION,
        ERR_EVENT_ALREADY_LOADED,
        ERR_EVENT_LIVEUPDATE_BUSY,
        ERR_EVENT_LIVEUPDATE_MISMATCH,
        ERR_EVENT_LIVEUPDATE_TIMEOUT,
        ERR_EVENT_NOTFOUND,
        ERR_STUDIO_UNINITIALIZED,
        ERR_STUDIO_NOT_LOADED,
        ERR_INVALID_STRING,
        ERR_ALREADY_LOCKED,
        ERR_NOT_LOCKED,
        ERR_RECORD_DISCONNECTED,
        ERR_TOOMANYSAMPLES,
    }

    public enum CHANNELCONTROL_TYPE : int
    {
        CHANNEL,
        CHANNELGROUP,
        MAX
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ATTRIBUTES_3D
    {
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 forward;
        public Vector3 up;
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct GUID
    {
        public int Data1;
        public int Data2;
        public int Data3;
        public int Data4;

        public static GUID FromString(string id)
        {
            var res = Studio.Native.FMOD_Studio_ParseID(Encoding.UTF8.GetBytes(id), out var guid);
            if (res != RESULT.OK)
                throw new FmodException(Error.String(res));
            return guid;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ASYNCREADINFO
    {
        public IntPtr   handle;
        public uint     offset;
        public uint     sizebytes;
        public int      priority;

        public IntPtr   userdata;
        public IntPtr   buffer;
        public uint     bytesread;
        public FILE_ASYNCDONE_FUNC done;
    }

    public enum OUTPUTTYPE : int
    {
        AUTODETECT,

        UNKNOWN,
        NOSOUND,
        WAVWRITER,
        NOSOUND_NRT,
        WAVWRITER_NRT,

        WASAPI,
        ASIO,
        PULSEAUDIO,
        ALSA,
        COREAUDIO,
        AUDIOTRACK,
        OPENSL,
        AUDIOOUT,
        AUDIO3D,
        WEBAUDIO,
        NNAUDIO,
        WINSONIC,
        AAUDIO,
        AUDIOWORKLET,

        MAX,
    }

    public enum PORT_TYPE : int
    {
        MUSIC,
        COPYRIGHT_MUSIC,
        VOICE,
        CONTROLLER,
        PERSONAL,
        VIBRATION,
        AUX,

        MAX
    }

    public enum DEBUG_MODE : int
    {
        TTY,
        FILE,
        CALLBACK,
    }

    [Flags]
    public enum DEBUG_FLAGS : uint
    {
        NONE                    = 0x00000000,
        ERROR                   = 0x00000001,
        WARNING                 = 0x00000002,
        LOG                     = 0x00000004,

        TYPE_MEMORY             = 0x00000100,
        TYPE_FILE               = 0x00000200,
        TYPE_CODEC              = 0x00000400,
        TYPE_TRACE              = 0x00000800,

        DISPLAY_TIMESTAMPS      = 0x00010000,
        DISPLAY_LINENUMBERS     = 0x00020000,
        DISPLAY_THREAD          = 0x00040000,
    }

    [Flags]
    public enum MEMORY_TYPE : uint
    {
        NORMAL                  = 0x00000000,
        STREAM_FILE             = 0x00000001,
        STREAM_DECODE           = 0x00000002,
        SAMPLEDATA              = 0x00000004,
        DSP_BUFFER              = 0x00000008,
        PLUGIN                  = 0x00000010,
        PERSISTENT              = 0x00200000,
        ALL                     = 0xFFFFFFFF
    }

    public enum SPEAKERMODE : int
    {
        DEFAULT,
        RAW,
        MONO,
        STEREO,
        QUAD,
        SURROUND,
        _5POINT1,
        _7POINT1,
        _7POINT1POINT4,

        MAX,
    }
     
    public enum SPEAKER : int
    {
        NONE = -1,
        FRONT_LEFT,
        FRONT_RIGHT,
        FRONT_CENTER,
        LOW_FREQUENCY,
        SURROUND_LEFT,
        SURROUND_RIGHT,
        BACK_LEFT,
        BACK_RIGHT,
        TOP_FRONT_LEFT,
        TOP_FRONT_RIGHT,
        TOP_BACK_LEFT,
        TOP_BACK_RIGHT,

        MAX,
    }

    [Flags]
    public enum CHANNELMASK : uint
    {
        FRONT_LEFT             = 0x00000001,
        FRONT_RIGHT            = 0x00000002,
        FRONT_CENTER           = 0x00000004,
        LOW_FREQUENCY          = 0x00000008,
        SURROUND_LEFT          = 0x00000010,
        SURROUND_RIGHT         = 0x00000020,
        BACK_LEFT              = 0x00000040,
        BACK_RIGHT             = 0x00000080,
        BACK_CENTER            = 0x00000100,

        MONO                   = (FRONT_LEFT),
        STEREO                 = (FRONT_LEFT | FRONT_RIGHT),
        LRC                    = (FRONT_LEFT | FRONT_RIGHT | FRONT_CENTER),
        QUAD                   = (FRONT_LEFT | FRONT_RIGHT | SURROUND_LEFT | SURROUND_RIGHT),
        SURROUND               = (FRONT_LEFT | FRONT_RIGHT | FRONT_CENTER | SURROUND_LEFT | SURROUND_RIGHT),
        _5POINT1               = (FRONT_LEFT | FRONT_RIGHT | FRONT_CENTER | LOW_FREQUENCY | SURROUND_LEFT | SURROUND_RIGHT),
        _5POINT1_REARS         = (FRONT_LEFT | FRONT_RIGHT | FRONT_CENTER | LOW_FREQUENCY | BACK_LEFT | BACK_RIGHT),
        _7POINT0               = (FRONT_LEFT | FRONT_RIGHT | FRONT_CENTER | SURROUND_LEFT | SURROUND_RIGHT | BACK_LEFT | BACK_RIGHT),
        _7POINT1               = (FRONT_LEFT | FRONT_RIGHT | FRONT_CENTER | LOW_FREQUENCY | SURROUND_LEFT | SURROUND_RIGHT | BACK_LEFT | BACK_RIGHT)
    }

    public enum CHANNELORDER : int
    {
        DEFAULT,
        WAVEFORMAT,
        PROTOOLS,
        ALLMONO,
        ALLSTEREO,
        ALSA,

        MAX,
    }

    public enum PLUGINTYPE : int
    {
        OUTPUT,
        CODEC,
        DSP,

        MAX,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PLUGINLIST
    {
        PLUGINTYPE type;
        IntPtr description;
    }

    [Flags]
    public enum INITFLAGS : uint
    {
        NORMAL                     = 0x00000000,
        STREAM_FROM_UPDATE         = 0x00000001,
        MIX_FROM_UPDATE            = 0x00000002,
        _3D_RIGHTHANDED            = 0x00000004,
        CHANNEL_LOWPASS            = 0x00000100,
        CHANNEL_DISTANCEFILTER     = 0x00000200,
        PROFILE_ENABLE             = 0x00010000,
        VOL0_BECOMES_VIRTUAL       = 0x00020000,
        GEOMETRY_USECLOSEST        = 0x00040000,
        PREFER_DOLBY_DOWNMIX       = 0x00080000,
        THREAD_UNSAFE              = 0x00100000,
        PROFILE_METER_ALL          = 0x00200000,
        MEMORY_TRACKING            = 0x00400000,
    }

    public enum SOUND_TYPE : int
    {
        UNKNOWN,
        AIFF,
        ASF,
        DLS,
        FLAC,
        FSB,
        IT,
        MIDI,
        MOD,
        MPEG,
        OGGVORBIS,
        PLAYLIST,
        RAW,
        S3M,
        USER,
        WAV,
        XM,
        XMA,
        AUDIOQUEUE,
        AT9,
        VORBIS,
        MEDIA_FOUNDATION,
        MEDIACODEC,
        FADPCM,
        OPUS,

        MAX,
    }

    public enum SOUND_FORMAT : int
    {
        NONE,
        PCM8,
        PCM16,
        PCM24,
        PCM32,
        PCMFLOAT,
        BITSTREAM,

        MAX
    }

    [Flags]
    public enum MODE : uint
    {
        DEFAULT                     = 0x00000000,
        LOOP_OFF                    = 0x00000001,
        LOOP_NORMAL                 = 0x00000002,
        LOOP_BIDI                   = 0x00000004,
        _2D                         = 0x00000008,
        _3D                         = 0x00000010,
        CREATESTREAM                = 0x00000080,
        CREATESAMPLE                = 0x00000100,
        CREATECOMPRESSEDSAMPLE      = 0x00000200,
        OPENUSER                    = 0x00000400,
        OPENMEMORY                  = 0x00000800,
        OPENMEMORY_POINT            = 0x10000000,
        OPENRAW                     = 0x00001000,
        OPENONLY                    = 0x00002000,
        ACCURATETIME                = 0x00004000,
        MPEGSEARCH                  = 0x00008000,
        NONBLOCKING                 = 0x00010000,
        UNIQUE                      = 0x00020000,
        _3D_HEADRELATIVE            = 0x00040000,
        _3D_WORLDRELATIVE           = 0x00080000,
        _3D_INVERSEROLLOFF          = 0x00100000,
        _3D_LINEARROLLOFF           = 0x00200000,
        _3D_LINEARSQUAREROLLOFF     = 0x00400000,
        _3D_INVERSETAPEREDROLLOFF   = 0x00800000,
        _3D_CUSTOMROLLOFF           = 0x04000000,
        _3D_IGNOREGEOMETRY          = 0x40000000,
        IGNORETAGS                  = 0x02000000,
        LOWMEM                      = 0x08000000,
        VIRTUAL_PLAYFROMSTART       = 0x80000000
    }

    public enum OPENSTATE : int
    {
        READY = 0,
        LOADING,
        ERROR,
        CONNECTING,
        BUFFERING,
        SEEKING,
        PLAYING,
        SETPOSITION,

        MAX,
    }

    public enum SOUNDGROUP_BEHAVIOR : int
    {
        BEHAVIOR_FAIL,
        BEHAVIOR_MUTE,
        BEHAVIOR_STEALLOWEST,

        MAX,
    }

    public enum CHANNELCONTROL_CALLBACK_TYPE : int
    {
        END,
        VIRTUALVOICE,
        SYNCPOINT,
        OCCLUSION,

        MAX,
    }

    public struct CHANNELCONTROL_DSP_INDEX
    {
        public const int HEAD    = -1;
        public const int FADER   = -2;
        public const int TAIL    = -3;
    }

    public enum ERRORCALLBACK_INSTANCETYPE : int
    {
        NONE,
        SYSTEM,
        CHANNEL,
        CHANNELGROUP,
        CHANNELCONTROL,
        SOUND,
        SOUNDGROUP,
        DSP,
        DSPCONNECTION,
        GEOMETRY,
        REVERB3D,
        STUDIO_SYSTEM,
        STUDIO_EVENTDESCRIPTION,
        STUDIO_EVENTINSTANCE,
        STUDIO_PARAMETERINSTANCE,
        STUDIO_BUS,
        STUDIO_VCA,
        STUDIO_BANK,
        STUDIO_COMMANDREPLAY
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ERRORCALLBACK_INFO
    {
        public  RESULT                      result;
        public  ERRORCALLBACK_INSTANCETYPE  instancetype;
        public  IntPtr                      instance;
        public  StringWrapper               functionname;
        public  StringWrapper               functionparams;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CPU_USAGE
    {
        public float    dsp;                    /* DSP mixing CPU usage. */
        public float    stream;                 /* Streaming engine CPU usage. */
        public float    geometry;               /* Geometry engine CPU usage. */
        public float    update;                 /* System::update CPU usage. */
        public float    convolution1;           /* Convolution reverb processing thread #1 CPU usage */
        public float    convolution2;           /* Convolution reverb processing thread #2 CPU usage */ 
    }

    [Flags]
    public enum SYSTEM_CALLBACK_TYPE : uint
    {
        DEVICELISTCHANGED      = 0x00000001,
        DEVICELOST             = 0x00000002,
        MEMORYALLOCATIONFAILED = 0x00000004,
        THREADCREATED          = 0x00000008,
        BADDSPCONNECTION       = 0x00000010,
        PREMIX                 = 0x00000020,
        POSTMIX                = 0x00000040,
        ERROR                  = 0x00000080,
        MIDMIX                 = 0x00000100,
        THREADDESTROYED        = 0x00000200,
        PREUPDATE              = 0x00000400,
        POSTUPDATE             = 0x00000800,
        RECORDLISTCHANGED      = 0x00001000,
        BUFFEREDNOMIX          = 0x00002000,
        DEVICEREINITIALIZE     = 0x00004000,
        OUTPUTUNDERRUN         = 0x00008000,
        ALL                    = 0xFFFFFFFF,
    }

    /*
        FMOD Callbacks
    */
    public delegate RESULT DEBUG_CALLBACK           (DEBUG_FLAGS flags, IntPtr file, int line, IntPtr func, IntPtr message);
    public delegate RESULT SYSTEM_CALLBACK          (IntPtr system, SYSTEM_CALLBACK_TYPE type, IntPtr commanddata1, IntPtr commanddata2, IntPtr userdata);
    public delegate RESULT CHANNELCONTROL_CALLBACK  (IntPtr channelcontrol, CHANNELCONTROL_TYPE controltype, CHANNELCONTROL_CALLBACK_TYPE callbacktype, IntPtr commanddata1, IntPtr commanddata2);
    public delegate RESULT SOUND_NONBLOCK_CALLBACK  (IntPtr sound, RESULT result);
    public delegate RESULT SOUND_PCMREAD_CALLBACK   (IntPtr sound, IntPtr data, uint datalen);
    public delegate RESULT SOUND_PCMSETPOS_CALLBACK (IntPtr sound, int subsound, uint position, TIMEUNIT postype);
    public delegate RESULT FILE_OPEN_CALLBACK       (IntPtr name, ref uint filesize, ref IntPtr handle, IntPtr userdata);
    public delegate RESULT FILE_CLOSE_CALLBACK      (IntPtr handle, IntPtr userdata);
    public delegate RESULT FILE_READ_CALLBACK       (IntPtr handle, IntPtr buffer, uint sizebytes, ref uint bytesread, IntPtr userdata);
    public delegate RESULT FILE_SEEK_CALLBACK       (IntPtr handle, uint pos, IntPtr userdata);
    public delegate RESULT FILE_ASYNCREAD_CALLBACK  (IntPtr info, IntPtr userdata);
    public delegate RESULT FILE_ASYNCCANCEL_CALLBACK(IntPtr info, IntPtr userdata);
    public delegate RESULT FILE_ASYNCDONE_FUNC      (IntPtr info, RESULT result);
    public delegate IntPtr MEMORY_ALLOC_CALLBACK    (uint size, MEMORY_TYPE type, IntPtr sourcestr);
    public delegate IntPtr MEMORY_REALLOC_CALLBACK  (IntPtr ptr, uint size, MEMORY_TYPE type, IntPtr sourcestr);
    public delegate void   MEMORY_FREE_CALLBACK     (IntPtr ptr, MEMORY_TYPE type, IntPtr sourcestr);
    public delegate float  CB_3D_ROLLOFF_CALLBACK   (IntPtr channelcontrol, float distance);

    public enum DSP_RESAMPLER : int
    {
        DEFAULT,
        NOINTERP,
        LINEAR,
        CUBIC,
        SPLINE,

        MAX,
    }

    public enum DSPCONNECTION_TYPE : int
    {
        STANDARD,
        SIDECHAIN,
        SEND,
        SEND_SIDECHAIN,

        MAX,
    }

    public enum TAGTYPE : int
    {
        UNKNOWN = 0,
        ID3V1,
        ID3V2,
        VORBISCOMMENT,
        SHOUTCAST,
        ICECAST,
        ASF,
        MIDI,
        PLAYLIST,
        FMOD,
        USER,

        MAX
    }

    public enum TAGDATATYPE : int
    {
        BINARY = 0,
        INT,
        FLOAT,
        STRING,
        STRING_UTF16,
        STRING_UTF16BE,
        STRING_UTF8,

        MAX
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TAG
    {
        public  TAGTYPE           type;
        public  TAGDATATYPE       datatype;
        public  StringWrapper     name;
        public  IntPtr            data;
        public  uint              datalen;
        public  bool              updated;
    }

    [Flags]
    public enum TIMEUNIT : uint
    {
        MS          = 0x00000001,
        PCM         = 0x00000002,
        PCMBYTES    = 0x00000004,
        RAWBYTES    = 0x00000008,
        PCMFRACTION = 0x00000010,
        MODORDER    = 0x00000100,
        MODROW      = 0x00000200,
        MODPATTERN  = 0x00000400,
    }

    public struct PORT_INDEX
    {
        public const ulong NONE = 0xFFFFFFFFFFFFFFFF;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CREATESOUNDEXINFO
    {
        public int                         cbsize;
        public uint                        length;
        public uint                        fileoffset;
        public int                         numchannels;
        public int                         defaultfrequency;
        public SOUND_FORMAT                format;
        public uint                        decodebuffersize;
        public int                         initialsubsound;
        public int                         numsubsounds;
        public IntPtr                      inclusionlist;
        public int                         inclusionlistnum;
        public SOUND_PCMREAD_CALLBACK      pcmreadcallback;
        public SOUND_PCMSETPOS_CALLBACK    pcmsetposcallback;
        public SOUND_NONBLOCK_CALLBACK     nonblockcallback;
        public IntPtr                      dlsname;
        public IntPtr                      encryptionkey;
        public int                         maxpolyphony;
        public IntPtr                      userdata;
        public SOUND_TYPE                  suggestedsoundtype;
        public FILE_OPEN_CALLBACK          fileuseropen;
        public FILE_CLOSE_CALLBACK         fileuserclose;
        public FILE_READ_CALLBACK          fileuserread;
        public FILE_SEEK_CALLBACK          fileuserseek;
        public FILE_ASYNCREAD_CALLBACK     fileuserasyncread;
        public FILE_ASYNCCANCEL_CALLBACK   fileuserasynccancel;
        public IntPtr                      fileuserdata;
        public int                         filebuffersize;
        public CHANNELORDER                channelorder;
        public IntPtr                      initialsoundgroup;
        public uint                        initialseekposition;
        public TIMEUNIT                    initialseekpostype;
        public int                         ignoresetfilesystem;
        public uint                        audioqueuepolicy;
        public uint                        minmidigranularity;
        public int                         nonblockthreadid;
        public IntPtr                      fsbguid;
    }

#pragma warning disable 414
    [StructLayout(LayoutKind.Sequential)]
    public struct REVERB_PROPERTIES
    {
        public float DecayTime;
        public float EarlyDelay;
        public float LateDelay;
        public float HFReference;
        public float HFDecayRatio;
        public float Diffusion;
        public float Density;
        public float LowShelfFrequency;
        public float LowShelfGain;
        public float HighCut;
        public float EarlyLateMix;
        public float WetLevel;

        #region wrapperinternal
        public REVERB_PROPERTIES(float decayTime, float earlyDelay, float lateDelay, float hfReference,
            float hfDecayRatio, float diffusion, float density, float lowShelfFrequency, float lowShelfGain,
            float highCut, float earlyLateMix, float wetLevel)
        {
            DecayTime = decayTime;
            EarlyDelay = earlyDelay;
            LateDelay = lateDelay;
            HFReference = hfReference;
            HFDecayRatio = hfDecayRatio;
            Diffusion = diffusion;
            Density = density;
            LowShelfFrequency = lowShelfFrequency;
            LowShelfGain = lowShelfGain;
            HighCut = highCut;
            EarlyLateMix = earlyLateMix;
            WetLevel = wetLevel;
        }
        #endregion
    }
#pragma warning restore 414

    public class PRESET
    {
        /*                                                                                  Instance  Env   Diffus  Room   RoomHF  RmLF DecTm   DecHF  DecLF   Refl  RefDel   Revb  RevDel  ModTm  ModDp   HFRef    LFRef   Diffus  Densty  FLAGS */
        public static REVERB_PROPERTIES OFF()                 { return new REVERB_PROPERTIES(  1000,    7,  11, 5000, 100, 100, 100, 250, 0,    20,  96, -80.0f );}
        public static REVERB_PROPERTIES GENERIC()             { return new REVERB_PROPERTIES(  1500,    7,  11, 5000,  83, 100, 100, 250, 0, 14500,  96,  -8.0f );}
        public static REVERB_PROPERTIES PADDEDCELL()          { return new REVERB_PROPERTIES(   170,    1,   2, 5000,  10, 100, 100, 250, 0,   160,  84,  -7.8f );}
        public static REVERB_PROPERTIES ROOM()                { return new REVERB_PROPERTIES(   400,    2,   3, 5000,  83, 100, 100, 250, 0,  6050,  88,  -9.4f );}
        public static REVERB_PROPERTIES BATHROOM()            { return new REVERB_PROPERTIES(  1500,    7,  11, 5000,  54, 100,  60, 250, 0,  2900,  83,   0.5f );}
        public static REVERB_PROPERTIES LIVINGROOM()          { return new REVERB_PROPERTIES(   500,    3,   4, 5000,  10, 100, 100, 250, 0,   160,  58, -19.0f );}
        public static REVERB_PROPERTIES STONEROOM()           { return new REVERB_PROPERTIES(  2300,   12,  17, 5000,  64, 100, 100, 250, 0,  7800,  71,  -8.5f );}
        public static REVERB_PROPERTIES AUDITORIUM()          { return new REVERB_PROPERTIES(  4300,   20,  30, 5000,  59, 100, 100, 250, 0,  5850,  64, -11.7f );}
        public static REVERB_PROPERTIES CONCERTHALL()         { return new REVERB_PROPERTIES(  3900,   20,  29, 5000,  70, 100, 100, 250, 0,  5650,  80,  -9.8f );}
        public static REVERB_PROPERTIES CAVE()                { return new REVERB_PROPERTIES(  2900,   15,  22, 5000, 100, 100, 100, 250, 0, 20000,  59, -11.3f );}
        public static REVERB_PROPERTIES ARENA()               { return new REVERB_PROPERTIES(  7200,   20,  30, 5000,  33, 100, 100, 250, 0,  4500,  80,  -9.6f );}
        public static REVERB_PROPERTIES HANGAR()              { return new REVERB_PROPERTIES( 10000,   20,  30, 5000,  23, 100, 100, 250, 0,  3400,  72,  -7.4f );}
        public static REVERB_PROPERTIES CARPETTEDHALLWAY()    { return new REVERB_PROPERTIES(   300,    2,  30, 5000,  10, 100, 100, 250, 0,   500,  56, -24.0f );}
        public static REVERB_PROPERTIES HALLWAY()             { return new REVERB_PROPERTIES(  1500,    7,  11, 5000,  59, 100, 100, 250, 0,  7800,  87,  -5.5f );}
        public static REVERB_PROPERTIES STONECORRIDOR()       { return new REVERB_PROPERTIES(   270,   13,  20, 5000,  79, 100, 100, 250, 0,  9000,  86,  -6.0f );}
        public static REVERB_PROPERTIES ALLEY()               { return new REVERB_PROPERTIES(  1500,    7,  11, 5000,  86, 100, 100, 250, 0,  8300,  80,  -9.8f );}
        public static REVERB_PROPERTIES FOREST()              { return new REVERB_PROPERTIES(  1500,  162,  88, 5000,  54,  79, 100, 250, 0,   760,  94, -12.3f );}
        public static REVERB_PROPERTIES CITY()                { return new REVERB_PROPERTIES(  1500,    7,  11, 5000,  67,  50, 100, 250, 0,  4050,  66, -26.0f );}
        public static REVERB_PROPERTIES MOUNTAINS()           { return new REVERB_PROPERTIES(  1500,  300, 100, 5000,  21,  27, 100, 250, 0,  1220,  82, -24.0f );}
        public static REVERB_PROPERTIES QUARRY()              { return new REVERB_PROPERTIES(  1500,   61,  25, 5000,  83, 100, 100, 250, 0,  3400, 100,  -5.0f );}
        public static REVERB_PROPERTIES PLAIN()               { return new REVERB_PROPERTIES(  1500,  179, 100, 5000,  50,  21, 100, 250, 0,  1670,  65, -28.0f );}
        public static REVERB_PROPERTIES PARKINGLOT()          { return new REVERB_PROPERTIES(  1700,    8,  12, 5000, 100, 100, 100, 250, 0, 20000,  56, -19.5f );}
        public static REVERB_PROPERTIES SEWERPIPE()           { return new REVERB_PROPERTIES(  2800,   14,  21, 5000,  14,  80,  60, 250, 0,  3400,  66,   1.2f );}
        public static REVERB_PROPERTIES UNDERWATER()          { return new REVERB_PROPERTIES(  1500,    7,  11, 5000,  10, 100, 100, 250, 0,   500,  92,   7.0f );}
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ADVANCEDSETTINGS
    {
        public int                 cbSize;
        public int                 maxMPEGCodecs;
        public int                 maxADPCMCodecs;
        public int                 maxXMACodecs;
        public int                 maxVorbisCodecs;
        public int                 maxAT9Codecs;
        public int                 maxFADPCMCodecs;
        public int                 maxPCMCodecs;
        public int                 ASIONumChannels;
        public IntPtr              ASIOChannelList;
        public IntPtr              ASIOSpeakerList;
        public float               vol0virtualvol;
        public uint                defaultDecodeBufferSize;
        public ushort              profilePort;
        public uint                geometryMaxFadeTime;
        public float               distanceFilterCenterFreq;
        public int                 reverb3Dinstance;
        public int                 DSPBufferPoolSize;
        public DSP_RESAMPLER       resamplerMethod;
        public uint                randomSeed;
        public int                 maxConvolutionThreads;
        public int                 maxOpusCodecs;
    }

    [Flags]
    public enum DRIVER_STATE : uint
    {
        CONNECTED = 0x00000001,
        DEFAULT   = 0x00000002,
    }

    public enum THREAD_PRIORITY : int
    {
        /* Platform specific priority range */
        PLATFORM_MIN        = -32 * 1024,
        PLATFORM_MAX        =  32 * 1024,

        /* Platform agnostic priorities, maps internally to platform specific value */
        DEFAULT             = PLATFORM_MIN - 1,
        LOW                 = PLATFORM_MIN - 2,
        MEDIUM              = PLATFORM_MIN - 3,
        HIGH                = PLATFORM_MIN - 4,
        VERY_HIGH           = PLATFORM_MIN - 5,
        EXTREME             = PLATFORM_MIN - 6,
        CRITICAL            = PLATFORM_MIN - 7,
        
        /* Thread defaults */
        MIXER               = EXTREME,
        FEEDER              = CRITICAL,
        STREAM              = VERY_HIGH,
        FILE                = HIGH,
        NONBLOCKING         = HIGH,
        RECORD              = HIGH,
        GEOMETRY            = LOW,
        PROFILER            = MEDIUM,
        STUDIO_UPDATE       = MEDIUM,
        STUDIO_LOAD_BANK    = MEDIUM,
        STUDIO_LOAD_SAMPLE  = MEDIUM,
        CONVOLUTION1        = VERY_HIGH,
        CONVOLUTION2        = VERY_HIGH

    }

    public enum THREAD_STACK_SIZE : uint
    {
        DEFAULT             = 0,
        MIXER               = 80  * 1024,
        FEEDER              = 16  * 1024,
        STREAM              = 96  * 1024,
        FILE                = 64  * 1024,
        NONBLOCKING         = 112 * 1024,
        RECORD              = 16  * 1024,
        GEOMETRY            = 48  * 1024,
        PROFILER            = 128 * 1024,
        STUDIO_UPDATE       = 96  * 1024,
        STUDIO_LOAD_BANK    = 96  * 1024,
        STUDIO_LOAD_SAMPLE  = 96  * 1024,
        CONVOLUTION1        = 16  * 1024,
        CONVOLUTION2        = 16  * 1024
    }

    [Flags]
    public enum THREAD_AFFINITY : long
    {
        /* Platform agnostic thread groupings */
        GROUP_DEFAULT       = 0x4000000000000000,
        GROUP_A             = 0x4000000000000001,
        GROUP_B             = 0x4000000000000002,
        GROUP_C             = 0x4000000000000003,
        
        /* Thread defaults */
        MIXER               = GROUP_A,
        FEEDER              = GROUP_C,
        STREAM              = GROUP_C,
        FILE                = GROUP_C,
        NONBLOCKING         = GROUP_C,
        RECORD              = GROUP_C,
        GEOMETRY            = GROUP_C,
        PROFILER            = GROUP_C,
        STUDIO_UPDATE       = GROUP_B,
        STUDIO_LOAD_BANK    = GROUP_C,
        STUDIO_LOAD_SAMPLE  = GROUP_C,
        CONVOLUTION1        = GROUP_C,
        CONVOLUTION2        = GROUP_C,
                
        /* Core mask, valid up to 1 << 61 */
        CORE_ALL            = 0,
        CORE_0              = 1 << 0,
        CORE_1              = 1 << 1,
        CORE_2              = 1 << 2,
        CORE_3              = 1 << 3,
        CORE_4              = 1 << 4,
        CORE_5              = 1 << 5,
        CORE_6              = 1 << 6,
        CORE_7              = 1 << 7,
        CORE_8              = 1 << 8,
        CORE_9              = 1 << 9,
        CORE_10             = 1 << 10,
        CORE_11             = 1 << 11,
        CORE_12             = 1 << 12,
        CORE_13             = 1 << 13,
        CORE_14             = 1 << 14,
        CORE_15             = 1 << 15
    }

    public enum THREAD_TYPE : int
    {
        MIXER,
        FEEDER,
        STREAM,
        FILE,
        NONBLOCKING,
        RECORD,
        GEOMETRY,
        PROFILER,
        STUDIO_UPDATE,
        STUDIO_LOAD_BANK,
        STUDIO_LOAD_SAMPLE,
        CONVOLUTION1,
        CONVOLUTION2,

        MAX
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct StringWrapper
    {
        IntPtr nativeUtf8Ptr;

        public StringWrapper(IntPtr ptr)
        {
            nativeUtf8Ptr = ptr;
        }

        public static implicit operator string(StringWrapper fstring)
        {
            using (StringHelper.ThreadSafeEncoding encoder = StringHelper.GetFreeHelper())
            {
                return encoder.stringFromNative(fstring.nativeUtf8Ptr);
            }
        }
    }
    
    static class StringHelper
    {
        public class ThreadSafeEncoding : IDisposable
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] encodedBuffer = new byte[128];
            char[] decodedBuffer = new char[128];
            bool inUse;
            GCHandle gcHandle;

            public bool InUse()    { return inUse; }
            public void SetInUse() { inUse = true; }

            private int roundUpPowerTwo(int number)
            {
                int newNumber = 1;
                while (newNumber <= number)
                {
                    newNumber *= 2;
                }

                return newNumber;
            }

            public byte[] byteFromStringUTF8(string s)
            {
                if (s == null)
                {
                    return null;
                }

                int maximumLength = encoding.GetMaxByteCount(s.Length) + 1; // +1 for null terminator
                if (maximumLength > encodedBuffer.Length)
                {
                    int encodedLength = encoding.GetByteCount(s) + 1; // +1 for null terminator
                    if (encodedLength > encodedBuffer.Length)
                    {
                        encodedBuffer = new byte[roundUpPowerTwo(encodedLength)];
                    }
                }

                int byteCount = encoding.GetBytes(s, 0, s.Length, encodedBuffer, 0);
                encodedBuffer[byteCount] = 0; // Apply null terminator

                return encodedBuffer;
            }

            public IntPtr intptrFromStringUTF8(string s)
            {
                if (s == null)
                {
                    return IntPtr.Zero;
                }

                gcHandle = GCHandle.Alloc(byteFromStringUTF8(s), GCHandleType.Pinned);
                return gcHandle.AddrOfPinnedObject();
            }

            public string stringFromNative(IntPtr nativePtr)
            {
                if (nativePtr == IntPtr.Zero)
                {
                    return "";
                }

                int nativeLen = 0;
                while (Marshal.ReadByte(nativePtr, nativeLen) != 0)
                {
                    nativeLen++;
                }

                if (nativeLen == 0)
                {
                    return "";
                }

                if (nativeLen > encodedBuffer.Length)
                {
                    encodedBuffer = new byte[roundUpPowerTwo(nativeLen)];
                }

                Marshal.Copy(nativePtr, encodedBuffer, 0, nativeLen);

                int maximumLength = encoding.GetMaxCharCount(nativeLen);
                if (maximumLength > decodedBuffer.Length)
                {
                    int decodedLength = encoding.GetCharCount(encodedBuffer, 0, nativeLen);
                    if (decodedLength > decodedBuffer.Length)
                    {
                        decodedBuffer = new char[roundUpPowerTwo(decodedLength)];
                    }
                }

                int charCount = encoding.GetChars(encodedBuffer, 0, nativeLen, decodedBuffer, 0);

                return new String(decodedBuffer, 0, charCount);
            }

            public void Dispose()
            {
                if (gcHandle.IsAllocated)
                {
                    gcHandle.Free();
                }
                lock (encoders)
                {
                    inUse = false;
                }
            }
        }

        static List<ThreadSafeEncoding> encoders = new List<ThreadSafeEncoding>(1);

        public static ThreadSafeEncoding GetFreeHelper()
        {
            lock (encoders)
            {
                ThreadSafeEncoding helper = null;
                // Search for not in use helper
                for (int i = 0; i < encoders.Count; i++)
                {
                    if (!encoders[i].InUse())
                    {
                        helper = encoders[i];
                        break;
                    }
                }
                // Otherwise create another helper
                if (helper == null)
                {
                    helper = new ThreadSafeEncoding();
                    encoders.Add(helper);
                }
                helper.SetInUse();
                return helper;
            }
        }
    }

    public static class Native
    {
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Create(out IntPtr system, uint headerversion);
        
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Memory_Initialize(IntPtr poolmem, int poollen, MEMORY_ALLOC_CALLBACK useralloc, MEMORY_REALLOC_CALLBACK userrealloc, MEMORY_FREE_CALLBACK userfree, MEMORY_TYPE memtypeflags);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Memory_GetStats  (out int currentalloced, out int maxalloced, bool blocking);
        
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Debug_Initialize(DEBUG_FLAGS flags, DEBUG_MODE mode, DEBUG_CALLBACK callback, string filename);
        
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Thread_SetAttributes(THREAD_TYPE type, THREAD_AFFINITY affinity, THREAD_PRIORITY priority, THREAD_STACK_SIZE stacksize);

        #region System
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Release                   (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetOutput                 (IntPtr system, OUTPUTTYPE output);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetOutput                 (IntPtr system, out OUTPUTTYPE output);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetNumDrivers             (IntPtr system, out int numdrivers);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetDriverInfo             (IntPtr system, int id, IntPtr name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetDriver                 (IntPtr system, int driver);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetDriver                 (IntPtr system, out int driver);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetSoftwareChannels       (IntPtr system, int numsoftwarechannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetSoftwareChannels       (IntPtr system, out int numsoftwarechannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetSoftwareFormat         (IntPtr system, int samplerate, SPEAKERMODE speakermode, int numrawspeakers);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetSoftwareFormat         (IntPtr system, out int samplerate, out SPEAKERMODE speakermode, out int numrawspeakers);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetDSPBufferSize          (IntPtr system, uint bufferlength, int numbuffers);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetDSPBufferSize          (IntPtr system, out uint bufferlength, out int numbuffers);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetFileSystem             (IntPtr system, FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek, FILE_ASYNCREAD_CALLBACK userasyncread, FILE_ASYNCCANCEL_CALLBACK userasynccancel, int blockalign);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_AttachFileSystem          (IntPtr system, FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetAdvancedSettings       (IntPtr system, ref ADVANCEDSETTINGS settings);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetAdvancedSettings       (IntPtr system, ref ADVANCEDSETTINGS settings);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetCallback               (IntPtr system, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetPluginPath             (IntPtr system, string path);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_LoadPlugin                (IntPtr system, string filename, out uint handle, uint priority);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_UnloadPlugin              (IntPtr system, uint handle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetNumNestedPlugins       (IntPtr system, uint handle, out int count);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetNestedPlugin           (IntPtr system, uint handle, int index, out uint nestedhandle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetNumPlugins             (IntPtr system, PLUGINTYPE plugintype, out int numplugins);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetPluginHandle           (IntPtr system, PLUGINTYPE plugintype, int index, out uint handle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetPluginInfo             (IntPtr system, uint handle, out PLUGINTYPE plugintype, IntPtr name, int namelen, out uint version);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetOutputByPlugin         (IntPtr system, uint handle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetOutputByPlugin         (IntPtr system, out uint handle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateDSPByPlugin         (IntPtr system, uint handle, out IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetDSPInfoByPlugin        (IntPtr system, uint handle, out IntPtr description);
        //[DllImport(VERSION.dll)]
        //public static extern RESULT FMOD5_System_RegisterCodec           (IntPtr system, out CODEC_DESCRIPTION description, out uint handle, uint priority);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_RegisterDSP               (IntPtr system, ref DSP_DESCRIPTION description, out uint handle);
        //[DllImport(VERSION.dll)]
        //public static extern RESULT FMOD5_System_RegisterOutput          (IntPtr system, ref OUTPUT_DESCRIPTION description, out uint handle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Init                      (IntPtr system, int maxchannels, INITFLAGS flags, IntPtr extradriverdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Close                     (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Update                    (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetSpeakerPosition        (IntPtr system, SPEAKER speaker, float x, float y, bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetSpeakerPosition        (IntPtr system, SPEAKER speaker, out float x, out float y, out bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetStreamBufferSize       (IntPtr system, uint filebuffersize, TIMEUNIT filebuffersizetype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetStreamBufferSize       (IntPtr system, out uint filebuffersize, out TIMEUNIT filebuffersizetype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Set3DSettings             (IntPtr system, float dopplerscale, float distancefactor, float rolloffscale);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Get3DSettings             (IntPtr system, out float dopplerscale, out float distancefactor, out float rolloffscale);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Set3DNumListeners         (IntPtr system, int numlisteners);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Get3DNumListeners         (IntPtr system, out int numlisteners);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Set3DListenerAttributes   (IntPtr system, int listener, ref Vector3 pos, ref Vector3 vel, ref Vector3 forward, ref Vector3 up);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Get3DListenerAttributes   (IntPtr system, int listener, out Vector3 pos, out Vector3 vel, out Vector3 forward, out Vector3 up);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_Set3DRolloffCallback      (IntPtr system, CB_3D_ROLLOFF_CALLBACK callback);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_MixerSuspend              (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_MixerResume               (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetDefaultMixMatrix       (IntPtr system, SPEAKERMODE sourcespeakermode, SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetSpeakerModeChannels    (IntPtr system, SPEAKERMODE mode, out int channels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetVersion                (IntPtr system, out uint version);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetOutputHandle           (IntPtr system, out IntPtr handle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetChannelsPlaying        (IntPtr system, out int channels, IntPtr zero);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetChannelsPlaying        (IntPtr system, out int channels, out int realchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetCPUUsage               (IntPtr system, out CPU_USAGE usage);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetFileUsage              (IntPtr system, out Int64 sampleBytesRead, out Int64 streamBytesRead, out Int64 otherBytesRead);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateSound               (IntPtr system, byte[] name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateSound               (IntPtr system, IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateStream              (IntPtr system, byte[] name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateStream              (IntPtr system, IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateDSP                 (IntPtr system, ref DSP_DESCRIPTION description, out IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateDSPByType           (IntPtr system, DSP_TYPE type, out IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateChannelGroup        (IntPtr system, byte[] name, out IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateSoundGroup          (IntPtr system, byte[] name, out IntPtr soundgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateReverb3D            (IntPtr system, out IntPtr reverb);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_PlaySound                 (IntPtr system, IntPtr sound, IntPtr channelgroup, bool paused, out IntPtr channel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_PlayDSP                   (IntPtr system, IntPtr dsp, IntPtr channelgroup, bool paused, out IntPtr channel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetChannel                (IntPtr system, int channelid, out IntPtr channel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetDSPInfoByType          (IntPtr system, DSP_TYPE type, out IntPtr description);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetMasterChannelGroup     (IntPtr system, out IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetMasterSoundGroup       (IntPtr system, out IntPtr soundgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_AttachChannelGroupToPort  (IntPtr system, PORT_TYPE portType, ulong portIndex, IntPtr channelgroup, bool passThru);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_DetachChannelGroupFromPort(IntPtr system, IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetReverbProperties       (IntPtr system, int instance, ref REVERB_PROPERTIES prop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetReverbProperties       (IntPtr system, int instance, out REVERB_PROPERTIES prop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_LockDSP                   (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_UnlockDSP                 (IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetRecordNumDrivers       (IntPtr system, out int numdrivers, out int numconnected);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetRecordDriverInfo       (IntPtr system, int id, IntPtr name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetRecordPosition         (IntPtr system, int id, out uint position);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_RecordStart               (IntPtr system, int id, IntPtr sound, bool loop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_RecordStop                (IntPtr system, int id);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_IsRecording               (IntPtr system, int id, out bool recording);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_CreateGeometry            (IntPtr system, int maxpolygons, int maxvertices, out IntPtr geometry);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetGeometrySettings       (IntPtr system, float maxworldsize);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetGeometrySettings       (IntPtr system, out float maxworldsize);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_LoadGeometry              (IntPtr system, IntPtr data, int datasize, out IntPtr geometry);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetGeometryOcclusion      (IntPtr system, ref Vector3 listener, ref Vector3 source, out float direct, out float reverb);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetNetworkProxy           (IntPtr system, byte[] proxy);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetNetworkProxy           (IntPtr system, IntPtr proxy, int proxylen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetNetworkTimeout         (IntPtr system, int timeout);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetNetworkTimeout         (IntPtr system, out int timeout);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_SetUserData               (IntPtr system, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_System_GetUserData               (IntPtr system, out IntPtr userdata);
        #endregion

        #region Sound
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Release                 (IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetSystemObject         (IntPtr sound, out IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Lock                    (IntPtr sound, uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Unlock                  (IntPtr sound, IntPtr ptr1,  IntPtr ptr2, uint len1, uint len2);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetDefaults             (IntPtr sound, float frequency, int priority);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetDefaults             (IntPtr sound, out float frequency, out int priority);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Set3DMinMaxDistance     (IntPtr sound, float min, float max);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Get3DMinMaxDistance     (IntPtr sound, out float min, out float max);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Set3DConeSettings       (IntPtr sound, float insideconeangle, float outsideconeangle, float outsidevolume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Get3DConeSettings       (IntPtr sound, out float insideconeangle, out float outsideconeangle, out float outsidevolume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Set3DCustomRolloff      (IntPtr sound, ref Vector3 points, int numpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_Get3DCustomRolloff      (IntPtr sound, out IntPtr points, out int numpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetSubSound             (IntPtr sound, int index, out IntPtr subsound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetSubSoundParent       (IntPtr sound, out IntPtr parentsound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetName                 (IntPtr sound, IntPtr name, int namelen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetLength               (IntPtr sound, out uint length, TIMEUNIT lengthtype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetFormat               (IntPtr sound, out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetNumSubSounds         (IntPtr sound, out int numsubsounds);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetNumTags              (IntPtr sound, out int numtags, out int numtagsupdated);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetTag                  (IntPtr sound, string name, int index, out TAG tag);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetOpenState            (IntPtr sound, out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_ReadData                (IntPtr sound, IntPtr buffer, uint length, out uint read);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SeekData                (IntPtr sound, uint pcm);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetSoundGroup           (IntPtr sound, IntPtr soundgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetSoundGroup           (IntPtr sound, out IntPtr soundgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetNumSyncPoints        (IntPtr sound, out int numsyncpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetSyncPoint            (IntPtr sound, int index, out IntPtr point);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetSyncPointInfo        (IntPtr sound, IntPtr point, IntPtr name, int namelen, out uint offset, TIMEUNIT offsettype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_AddSyncPoint            (IntPtr sound, uint offset, TIMEUNIT offsettype, string name, out IntPtr point);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_DeleteSyncPoint         (IntPtr sound, IntPtr point);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetMode                 (IntPtr sound, MODE mode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetMode                 (IntPtr sound, out MODE mode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetLoopCount            (IntPtr sound, int loopcount);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetLoopCount            (IntPtr sound, out int loopcount);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetLoopPoints           (IntPtr sound, uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetLoopPoints           (IntPtr sound, out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetMusicNumChannels     (IntPtr sound, out int numchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetMusicChannelVolume   (IntPtr sound, int channel, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetMusicChannelVolume   (IntPtr sound, int channel, out float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetMusicSpeed           (IntPtr sound, float speed);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetMusicSpeed           (IntPtr sound, out float speed);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_SetUserData             (IntPtr sound, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Sound_GetUserData             (IntPtr sound, out IntPtr userdata);
        #endregion

        #region Channel
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetFrequency         (IntPtr channel, float frequency);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetFrequency         (IntPtr channel, out float frequency);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetPriority          (IntPtr channel, int priority);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetPriority          (IntPtr channel, out int priority);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetPosition          (IntPtr channel, uint position, TIMEUNIT postype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetPosition          (IntPtr channel, out uint position, TIMEUNIT postype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetChannelGroup      (IntPtr channel, IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetChannelGroup      (IntPtr channel, out IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetLoopCount         (IntPtr channel, int loopcount);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetLoopCount         (IntPtr channel, out int loopcount);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetLoopPoints        (IntPtr channel, uint  loopstart, TIMEUNIT loopstarttype, uint  loopend, TIMEUNIT loopendtype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetLoopPoints        (IntPtr channel, out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_IsVirtual            (IntPtr channel, out bool isvirtual);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetCurrentSound      (IntPtr channel, out IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetIndex             (IntPtr channel, out int index);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetSystemObject      (IntPtr channel, out IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Stop                 (IntPtr channel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetPaused            (IntPtr channel, bool paused);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetPaused            (IntPtr channel, out bool paused);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetVolume            (IntPtr channel, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetVolume            (IntPtr channel, out float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetVolumeRamp        (IntPtr channel, bool ramp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetVolumeRamp        (IntPtr channel, out bool ramp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetAudibility        (IntPtr channel, out float audibility);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetPitch             (IntPtr channel, float pitch);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetPitch             (IntPtr channel, out float pitch);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetMute              (IntPtr channel, bool mute);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetMute              (IntPtr channel, out bool mute);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetReverbProperties  (IntPtr channel, int instance, float wet);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetReverbProperties  (IntPtr channel, int instance, out float wet);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetLowPassGain       (IntPtr channel, float gain);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetLowPassGain       (IntPtr channel, out float gain);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetMode              (IntPtr channel, MODE mode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetMode              (IntPtr channel, out MODE mode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetCallback          (IntPtr channel, CHANNELCONTROL_CALLBACK callback);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_IsPlaying            (IntPtr channel, out bool isplaying);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetPan               (IntPtr channel, float pan);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetMixLevelsOutput   (IntPtr channel, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetMixLevelsInput    (IntPtr channel, float[] levels, int numlevels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetMixMatrix         (IntPtr channel, float[] matrix, int outchannels, int inchannels, int inchannel_hop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetMixMatrix         (IntPtr channel, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetDSPClock          (IntPtr channel, out ulong dspclock, out ulong parentclock);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetDelay             (IntPtr channel, ulong dspclock_start, ulong dspclock_end, bool stopchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetDelay             (IntPtr channel, out ulong dspclock_start, out ulong dspclock_end, IntPtr zero);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetDelay             (IntPtr channel, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_AddFadePoint         (IntPtr channel, ulong dspclock, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetFadePointRamp     (IntPtr channel, ulong dspclock, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_RemoveFadePoints     (IntPtr channel, ulong dspclock_start, ulong dspclock_end);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetFadePoints        (IntPtr channel, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetDSP               (IntPtr channel, int index, out IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_AddDSP               (IntPtr channel, int index, IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_RemoveDSP            (IntPtr channel, IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetNumDSPs           (IntPtr channel, out int numdsps);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetDSPIndex          (IntPtr channel, IntPtr dsp, int index);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetDSPIndex          (IntPtr channel, IntPtr dsp, out int index);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DAttributes      (IntPtr channel, ref Vector3 pos, ref Vector3 vel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DAttributes      (IntPtr channel, out Vector3 pos, out Vector3 vel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DMinMaxDistance  (IntPtr channel, float mindistance, float maxdistance);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DMinMaxDistance  (IntPtr channel, out float mindistance, out float maxdistance);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DConeSettings    (IntPtr channel, float insideconeangle, float outsideconeangle, float outsidevolume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DConeSettings    (IntPtr channel, out float insideconeangle, out float outsideconeangle, out float outsidevolume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DConeOrientation (IntPtr channel, ref Vector3 orientation);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DConeOrientation (IntPtr channel, out Vector3 orientation);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DCustomRolloff   (IntPtr channel, ref Vector3 points, int numpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DCustomRolloff   (IntPtr channel, out IntPtr points, out int numpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DOcclusion       (IntPtr channel, float directocclusion, float reverbocclusion);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DOcclusion       (IntPtr channel, out float directocclusion, out float reverbocclusion);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DSpread          (IntPtr channel, float angle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DSpread          (IntPtr channel, out float angle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DLevel           (IntPtr channel, float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DLevel           (IntPtr channel, out float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DDopplerLevel    (IntPtr channel, float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DDopplerLevel    (IntPtr channel, out float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Set3DDistanceFilter  (IntPtr channel, bool custom, float customLevel, float centerFreq);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_Get3DDistanceFilter  (IntPtr channel, out bool custom, out float customLevel, out float centerFreq);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_SetUserData          (IntPtr channel, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Channel_GetUserData          (IntPtr channel, out IntPtr userdata);
        #endregion

        #region ChannelGroup
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Release             (IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_AddGroup            (IntPtr channelgroup, IntPtr group, bool propagatedspclock, IntPtr zero);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_AddGroup            (IntPtr channelgroup, IntPtr group, bool propagatedspclock, out IntPtr connection);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetNumGroups        (IntPtr channelgroup, out int numgroups);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetGroup            (IntPtr channelgroup, int index, out IntPtr group);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetParentGroup      (IntPtr channelgroup, out IntPtr group);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetName             (IntPtr channelgroup, IntPtr name, int namelen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetNumChannels      (IntPtr channelgroup, out int numchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetChannel          (IntPtr channelgroup, int index, out IntPtr channel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetSystemObject     (IntPtr channelgroup, out IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Stop                (IntPtr channelgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetPaused           (IntPtr channelgroup, bool paused);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetPaused           (IntPtr channelgroup, out bool paused);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetVolume           (IntPtr channelgroup, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetVolume           (IntPtr channelgroup, out float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetVolumeRamp       (IntPtr channelgroup, bool ramp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetVolumeRamp       (IntPtr channelgroup, out bool ramp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetAudibility       (IntPtr channelgroup, out float audibility);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetPitch            (IntPtr channelgroup, float pitch);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetPitch            (IntPtr channelgroup, out float pitch);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetMute             (IntPtr channelgroup, bool mute);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetMute             (IntPtr channelgroup, out bool mute);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetReverbProperties (IntPtr channelgroup, int instance, float wet);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetReverbProperties (IntPtr channelgroup, int instance, out float wet);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetLowPassGain      (IntPtr channelgroup, float gain);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetLowPassGain      (IntPtr channelgroup, out float gain);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetMode             (IntPtr channelgroup, MODE mode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetMode             (IntPtr channelgroup, out MODE mode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetCallback         (IntPtr channelgroup, CHANNELCONTROL_CALLBACK callback);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_IsPlaying           (IntPtr channelgroup, out bool isplaying);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetPan              (IntPtr channelgroup, float pan);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetMixLevelsOutput  (IntPtr channelgroup, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetMixLevelsInput   (IntPtr channelgroup, float[] levels, int numlevels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetMixMatrix        (IntPtr channelgroup, float[] matrix, int outchannels, int inchannels, int inchannel_hop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetMixMatrix        (IntPtr channelgroup, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetDSPClock         (IntPtr channelgroup, out ulong dspclock, out ulong parentclock);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetDelay            (IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end, bool stopchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetDelay            (IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, IntPtr zero);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetDelay            (IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_AddFadePoint        (IntPtr channelgroup, ulong dspclock, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetFadePointRamp    (IntPtr channelgroup, ulong dspclock, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_RemoveFadePoints    (IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetFadePoints       (IntPtr channelgroup, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetDSP              (IntPtr channelgroup, int index, out IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_AddDSP              (IntPtr channelgroup, int index, IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_RemoveDSP           (IntPtr channelgroup, IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetNumDSPs          (IntPtr channelgroup, out int numdsps);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetDSPIndex         (IntPtr channelgroup, IntPtr dsp, int index);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetDSPIndex         (IntPtr channelgroup, IntPtr dsp, out int index);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DAttributes     (IntPtr channelgroup, ref Vector3 pos, ref Vector3 vel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DAttributes     (IntPtr channelgroup, out Vector3 pos, out Vector3 vel);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DMinMaxDistance (IntPtr channelgroup, float mindistance, float maxdistance);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DMinMaxDistance (IntPtr channelgroup, out float mindistance, out float maxdistance);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DConeSettings   (IntPtr channelgroup, float insideconeangle, float outsideconeangle, float outsidevolume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DConeSettings   (IntPtr channelgroup, out float insideconeangle, out float outsideconeangle, out float outsidevolume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DConeOrientation(IntPtr channelgroup, ref Vector3 orientation);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DConeOrientation(IntPtr channelgroup, out Vector3 orientation);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DCustomRolloff  (IntPtr channelgroup, ref Vector3 points, int numpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DCustomRolloff  (IntPtr channelgroup, out IntPtr points, out int numpoints);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DOcclusion      (IntPtr channelgroup, float directocclusion, float reverbocclusion);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DOcclusion      (IntPtr channelgroup, out float directocclusion, out float reverbocclusion);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DSpread         (IntPtr channelgroup, float angle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DSpread         (IntPtr channelgroup, out float angle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DLevel          (IntPtr channelgroup, float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DLevel          (IntPtr channelgroup, out float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DDopplerLevel   (IntPtr channelgroup, float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DDopplerLevel   (IntPtr channelgroup, out float level);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Set3DDistanceFilter (IntPtr channelgroup, bool custom, float customLevel, float centerFreq);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_Get3DDistanceFilter (IntPtr channelgroup, out bool custom, out float customLevel, out float centerFreq);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_SetUserData         (IntPtr channelgroup, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_ChannelGroup_GetUserData         (IntPtr channelgroup, out IntPtr userdata);
        #endregion
        
        #region SoundGroup
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_Release               (IntPtr soundgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetSystemObject       (IntPtr soundgroup, out IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_SetMaxAudible         (IntPtr soundgroup, int maxaudible);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetMaxAudible         (IntPtr soundgroup, out int maxaudible);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_SetMaxAudibleBehavior (IntPtr soundgroup, SOUNDGROUP_BEHAVIOR behavior);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetMaxAudibleBehavior (IntPtr soundgroup, out SOUNDGROUP_BEHAVIOR behavior);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_SetMuteFadeSpeed      (IntPtr soundgroup, float speed);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetMuteFadeSpeed      (IntPtr soundgroup, out float speed);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_SetVolume             (IntPtr soundgroup, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetVolume             (IntPtr soundgroup, out float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_Stop                  (IntPtr soundgroup);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetName               (IntPtr soundgroup, IntPtr name, int namelen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetNumSounds          (IntPtr soundgroup, out int numsounds);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetSound              (IntPtr soundgroup, int index, out IntPtr sound);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetNumPlaying         (IntPtr soundgroup, out int numplaying);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_SetUserData           (IntPtr soundgroup, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_SoundGroup_GetUserData           (IntPtr soundgroup, out IntPtr userdata);
        #endregion

        #region DSP
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_Release                   (IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetSystemObject           (IntPtr dsp, out IntPtr system);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_AddInput                  (IntPtr dsp, IntPtr input, IntPtr zero, DSPCONNECTION_TYPE type);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_AddInput                  (IntPtr dsp, IntPtr input, out IntPtr connection, DSPCONNECTION_TYPE type);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_DisconnectFrom            (IntPtr dsp, IntPtr target, IntPtr connection);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_DisconnectAll             (IntPtr dsp, bool inputs, bool outputs);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetNumInputs              (IntPtr dsp, out int numinputs);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetNumOutputs             (IntPtr dsp, out int numoutputs);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetInput                  (IntPtr dsp, int index, out IntPtr input, out IntPtr inputconnection);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetOutput                 (IntPtr dsp, int index, out IntPtr output, out IntPtr outputconnection);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetActive                 (IntPtr dsp, bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetActive                 (IntPtr dsp, out bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetBypass                 (IntPtr dsp, bool bypass);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetBypass                 (IntPtr dsp, out bool bypass);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetWetDryMix              (IntPtr dsp, float prewet, float postwet, float dry);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetWetDryMix              (IntPtr dsp, out float prewet, out float postwet, out float dry);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetChannelFormat          (IntPtr dsp, CHANNELMASK channelmask, int numchannels, SPEAKERMODE source_speakermode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetChannelFormat          (IntPtr dsp, out CHANNELMASK channelmask, out int numchannels, out SPEAKERMODE source_speakermode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetOutputChannelFormat    (IntPtr dsp, CHANNELMASK inmask, int inchannels, SPEAKERMODE inspeakermode, out CHANNELMASK outmask, out int outchannels, out SPEAKERMODE outspeakermode);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_Reset                     (IntPtr dsp);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetParameterFloat         (IntPtr dsp, int index, float value);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetParameterInt           (IntPtr dsp, int index, int value);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetParameterBool          (IntPtr dsp, int index, bool value);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetParameterData          (IntPtr dsp, int index, IntPtr data, uint length);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetParameterFloat         (IntPtr dsp, int index, out float value, IntPtr valuestr, int valuestrlen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetParameterInt           (IntPtr dsp, int index, out int value, IntPtr valuestr, int valuestrlen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetParameterBool          (IntPtr dsp, int index, out bool value, IntPtr valuestr, int valuestrlen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetParameterData          (IntPtr dsp, int index, out IntPtr data, out uint length, IntPtr valuestr, int valuestrlen);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetNumParameters          (IntPtr dsp, out int numparams);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetParameterInfo          (IntPtr dsp, int index, out IntPtr desc);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetDataParameterIndex     (IntPtr dsp, int datatype, out int index);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_ShowConfigDialog          (IntPtr dsp, IntPtr hwnd, bool show);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetInfo                   (IntPtr dsp, IntPtr name, out uint version, out int channels, out int configwidth, out int configheight);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetType                   (IntPtr dsp, out DSP_TYPE type);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetIdle                   (IntPtr dsp, out bool idle);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetUserData               (IntPtr dsp, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetUserData               (IntPtr dsp, out IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_SetMeteringEnabled         (IntPtr dsp, bool inputEnabled, bool outputEnabled);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetMeteringEnabled         (IntPtr dsp, out bool inputEnabled, out bool outputEnabled);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetMeteringInfo            (IntPtr dsp, IntPtr zero, out DSP_METERING_INFO outputInfo);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetMeteringInfo            (IntPtr dsp, out DSP_METERING_INFO inputInfo, IntPtr zero);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetMeteringInfo            (IntPtr dsp, out DSP_METERING_INFO inputInfo, out DSP_METERING_INFO outputInfo);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSP_GetCPUUsage                (IntPtr dsp, out uint exclusive, out uint inclusive);
        #endregion

        #region DSPConnection
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_GetInput        (IntPtr dspconnection, out IntPtr input);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_GetOutput       (IntPtr dspconnection, out IntPtr output);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_SetMix          (IntPtr dspconnection, float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_GetMix          (IntPtr dspconnection, out float volume);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_SetMixMatrix    (IntPtr dspconnection, float[] matrix, int outchannels, int inchannels, int inchannel_hop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_GetMixMatrix    (IntPtr dspconnection, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_GetType         (IntPtr dspconnection, out DSPCONNECTION_TYPE type);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_SetUserData     (IntPtr dspconnection, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_DSPConnection_GetUserData     (IntPtr dspconnection, out IntPtr userdata);
        #endregion

        #region Geometry
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_Release              (IntPtr geometry);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_AddPolygon           (IntPtr geometry, float directocclusion, float reverbocclusion, bool doublesided, int numvertices, Vector3[] vertices, out int polygonindex);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetNumPolygons       (IntPtr geometry, out int numpolygons);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetMaxPolygons       (IntPtr geometry, out int maxpolygons, out int maxvertices);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetPolygonNumVertices(IntPtr geometry, int index, out int numvertices);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetPolygonVertex     (IntPtr geometry, int index, int vertexindex, ref Vector3 vertex);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetPolygonVertex     (IntPtr geometry, int index, int vertexindex, out Vector3 vertex);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetPolygonAttributes (IntPtr geometry, int index, float directocclusion, float reverbocclusion, bool doublesided);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetPolygonAttributes (IntPtr geometry, int index, out float directocclusion, out float reverbocclusion, out bool doublesided);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetActive            (IntPtr geometry, bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetActive            (IntPtr geometry, out bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetRotation          (IntPtr geometry, ref Vector3 forward, ref Vector3 up);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetRotation          (IntPtr geometry, out Vector3 forward, out Vector3 up);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetPosition          (IntPtr geometry, ref Vector3 position);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetPosition          (IntPtr geometry, out Vector3 position);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetScale             (IntPtr geometry, ref Vector3 scale);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetScale             (IntPtr geometry, out Vector3 scale);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_Save                 (IntPtr geometry, IntPtr data, out int datasize);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_SetUserData          (IntPtr geometry, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Geometry_GetUserData          (IntPtr geometry, out IntPtr userdata);
        #endregion

        #region Reverb3D
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_Release             (IntPtr reverb3d);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_Set3DAttributes     (IntPtr reverb3d, ref Vector3 position, float mindistance, float maxdistance);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_Get3DAttributes     (IntPtr reverb3d, ref Vector3 position, ref float mindistance, ref float maxdistance);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_SetProperties       (IntPtr reverb3d, ref REVERB_PROPERTIES properties);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_GetProperties       (IntPtr reverb3d, ref REVERB_PROPERTIES properties);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_SetActive           (IntPtr reverb3d, bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_GetActive           (IntPtr reverb3d, out bool active);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_SetUserData         (IntPtr reverb3d, IntPtr userdata);
        [DllImport(VERSION.dll)]
        public static extern RESULT FMOD5_Reverb3D_GetUserData         (IntPtr reverb3d, out IntPtr userdata);
        #endregion
    }
}
