// @formatter:off
namespace ChromaFMOD;

public static class Dispatcher
{
    public static bool CallAndThrow(RESULT res)
    {
        return res switch
        {
            RESULT.OK => false,
            RESULT.ERR_BADCOMMAND => throw new FmodNotSupportedException(Error.String(res), new NotImplementedException()),
            RESULT.ERR_CHANNEL_ALLOC => throw new FmodSystemException(Error.String(res)),
            RESULT.ERR_CHANNEL_STOLEN => throw new FmodSystemException(Error.String(res)),
            RESULT.ERR_DMA => throw new FmodNotSupportedException(Error.String(res), new NotImplementedException()),
            RESULT.ERR_DSP_CONNECTION => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_DONTPROCESS => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_FORMAT => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_INUSE => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_NOTFOUND => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_RESERVED => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_SILENCE => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_DSP_TYPE => throw new FmodDspException(Error.String(res)),
            RESULT.ERR_FILE_BAD => throw new FmodFileException(Error.String(res), new FileLoadException()),
            RESULT.ERR_FILE_COULDNOTSEEK => throw new FmodFileException(Error.String(res)),
            RESULT.ERR_FILE_DISKEJECTED => throw new FmodFileException(Error.String(res)),
            RESULT.ERR_FILE_EOF => throw new FmodFileException(Error.String(res), new EndOfStreamException()),
            RESULT.ERR_FILE_ENDOFDATA => throw new FmodFileException(Error.String(res), new EndOfStreamException()),
            RESULT.ERR_FILE_NOTFOUND => throw new FmodFileException(Error.String(res), new FileNotFoundException()),
            RESULT.ERR_FORMAT => throw new FmodFileException(Error.String(res)),
            RESULT.ERR_HEADER_MISMATCH => throw new FmodFileException(Error.String(res), new BadImageFormatException()),
            RESULT.ERR_HTTP => throw new FmodNetworkException(Error.String(res), new HttpRequestException()),
            RESULT.ERR_HTTP_ACCESS => throw new FmodNetworkException(Error.String(res), new HttpRequestException()),
            RESULT.ERR_HTTP_PROXY_AUTH => throw new FmodNetworkException(Error.String(res), new HttpRequestException()),
            RESULT.ERR_HTTP_SERVER_ERROR => throw new FmodNetworkException(Error.String(res), new HttpListenerException()),
            RESULT.ERR_HTTP_TIMEOUT => throw new FmodNetworkException(Error.String(res), new TimeoutException()),
            RESULT.ERR_INITIALIZATION => throw new FmodException(Error.String(res)),
            RESULT.ERR_INITIALIZED => throw new FmodSystemException(Error.String(res)),
            RESULT.ERR_INTERNAL => throw new FmodException(Error.String(res)),
            RESULT.ERR_INVALID_FLOAT => throw new FmodParameterException(Error.String(res), new ArgumentException()),
            RESULT.ERR_INVALID_HANDLE => throw new FmodParameterException(Error.String(res), new ArgumentException()),
            RESULT.ERR_INVALID_PARAM => throw new FmodParameterException(Error.String(res), new ArgumentException()),
            RESULT.ERR_INVALID_POSITION => throw new FmodParameterException(Error.String(res), new ArgumentException()),
            RESULT.ERR_INVALID_SPEAKER => throw new FmodParameterException(Error.String(res), new ArgumentException()),
            RESULT.ERR_INVALID_SYNCPOINT => throw new FmodSoundException(Error.String(res), new ArgumentException()),
            RESULT.ERR_INVALID_THREAD => throw new FmodException(Error.String(res), new ThreadStateException()),
            RESULT.ERR_INVALID_VECTOR => throw new FmodParameterException(Error.String(res), new ArgumentException()),
            RESULT.ERR_MAXAUDIBLE => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_MEMORY => throw new FmodException(Error.String(res), new InsufficientMemoryException()),
            RESULT.ERR_MEMORY_CANTPOINT => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_NEEDS3D => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_NEEDSHARDWARE => throw new FmodException(Error.String(res)),
            RESULT.ERR_NET_CONNECT => throw new FmodNetworkException(Error.String(res), new HttpRequestException()),
            RESULT.ERR_NET_SOCKET_ERROR => throw new FmodNetworkException(Error.String(res), new SocketException()),
            RESULT.ERR_NET_URL => throw new FmodNetworkException(Error.String(res), new SocketException()),
            RESULT.ERR_NET_WOULD_BLOCK => throw new FmodNetworkException(Error.String(res), new SocketException()),
            RESULT.ERR_NOTREADY => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_OUTPUT_ALLOCATED => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_OUTPUT_CREATEBUFFER => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_OUTPUT_DRIVERCALL => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_OUTPUT_FORMAT => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_OUTPUT_INIT => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_OUTPUT_NODRIVERS => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_PLUGIN => throw new FmodPluginException(Error.String(res)),
            RESULT.ERR_PLUGIN_MISSING => throw new FmodPluginException(Error.String(res)),
            RESULT.ERR_PLUGIN_RESOURCE => throw new FmodPluginException(Error.String(res)),
            RESULT.ERR_PLUGIN_VERSION => throw new FmodPluginException(Error.String(res)),
            RESULT.ERR_RECORD => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_REVERB_CHANNELGROUP => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_REVERB_INSTANCE => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_SUBSOUNDS => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_SUBSOUND_ALLOCATED => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_SUBSOUND_CANTMOVE => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_TAGNOTFOUND => throw new FmodException(Error.String(res)),
            RESULT.ERR_TOOMANYCHANNELS => throw new FmodSoundException(Error.String(res)),
            RESULT.ERR_TRUNCATED => throw new FmodException(Error.String(res), new InternalBufferOverflowException()),
            RESULT.ERR_UNIMPLEMENTED => throw new FmodNotSupportedException(Error.String(res), new NotImplementedException()),
            RESULT.ERR_UNINITIALIZED => throw new FmodSystemException(Error.String(res)),
            RESULT.ERR_UNSUPPORTED => throw new FmodNotSupportedException(Error.String(res), new NotImplementedException()),
            RESULT.ERR_VERSION => throw new FmodFileException(Error.String(res)),
            RESULT.ERR_EVENT_ALREADY_LOADED => throw new FmodEventException(Error.String(res)),
            RESULT.ERR_EVENT_LIVEUPDATE_BUSY => throw new FmodEventException(Error.String(res)),
            RESULT.ERR_EVENT_LIVEUPDATE_MISMATCH => throw new FmodEventException(Error.String(res)),
            RESULT.ERR_EVENT_LIVEUPDATE_TIMEOUT => throw new FmodEventException(Error.String(res)),
            RESULT.ERR_EVENT_NOTFOUND => throw new FmodEventException(Error.String(res)),
            RESULT.ERR_STUDIO_UNINITIALIZED => throw new FmodStudioException(Error.String(res)),
            RESULT.ERR_STUDIO_NOT_LOADED => throw new FmodStudioException(Error.String(res), new TypeUnloadedException()),
            RESULT.ERR_INVALID_STRING => throw new FmodParameterException(Error.String(res)),
            RESULT.ERR_ALREADY_LOCKED => throw new FmodLockException(Error.String(res)),
            RESULT.ERR_NOT_LOCKED => throw new FmodLockException(Error.String(res)),
            RESULT.ERR_RECORD_DISCONNECTED => throw new FmodDeviceException(Error.String(res)),
            RESULT.ERR_TOOMANYSAMPLES => throw new FmodSoundException(Error.String(res)),
            _ => throw new ArgumentOutOfRangeException(nameof(res)),
        };
    }
}