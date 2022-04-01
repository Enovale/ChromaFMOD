using System.Runtime.InteropServices;
using Native = FMOD.Studio.Native;
using SYSTEM_CALLBACK = FMOD.Studio.SYSTEM_CALLBACK;
using SYSTEM_CALLBACK_TYPE = FMOD.Studio.SYSTEM_CALLBACK_TYPE;

namespace ChromaFMOD.Studio;

public class StudioSystem : DisposableResource
{
    public IntPtr Handle { get; set; }

    public bool IsValid => Handle != IntPtr.Zero;

    public StudioSystem(int maxChannels, INITFLAGS studioFlags = INITFLAGS.SYNCHRONOUS_UPDATE,
        FMOD.INITFLAGS flags = FMOD.INITFLAGS.NORMAL,
        bool disableChromaAudio = true)
    {
        if (disableChromaAudio)
        {
            var output =
                (AudioOutput)typeof(AudioOutput).GetProperty("Instance",
                    BindingFlags.NonPublic | BindingFlags.Static)!.GetValue(null)!;
            var input = (AudioInput)typeof(AudioInput).GetProperty("Instance",
                BindingFlags.NonPublic | BindingFlags.Static)!.GetValue(null)!;
            output.Close();
            typeof(AudioInput).GetMethod("Close", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(input,
                Array.Empty<object?>());
        }

        CallAndThrow(Native.FMOD_Studio_System_Create(out var system, VERSION.number));

        Handle = system;
        EnsureHandleValid();

        CallAndThrow(Native.FMOD_Studio_System_Initialize(Handle, maxChannels, studioFlags, flags, IntPtr.Zero));
    }

    public Bank LoadBankFile(string filename, LOAD_BANK_FLAGS flags)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_LoadBankFile(Handle, filename, flags, out var bank));

        return new Bank(bank);
    }

    public Bank LoadBankFromMemory(byte[] data, LOAD_MEMORY_MODE mode, LOAD_BANK_FLAGS flags)
    {
        EnsureHandleValid();
        var pnt = Marshal.AllocHGlobal(Marshal.SizeOf(data));
        Marshal.StructureToPtr(data, pnt, true);
        CallAndThrow(Native.FMOD_Studio_System_LoadBankMemory(Handle, pnt, data.Length, mode, flags, out var bank));

        return new Bank(bank);
    }

    public Bank LoadBankFromMemory(MemoryStream stream, LOAD_MEMORY_MODE mode, LOAD_BANK_FLAGS flags) 
        => LoadBankFromMemory(stream.ToArray(), mode, flags);

    public EventDescription GetEvent(string path)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_GetEvent(Handle, Encoding.UTF8.GetBytes(path), out var newEvent));
        return new EventDescription(newEvent);
    }

    public EventDescription GetEvent(GUID id)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_GetEventByID(Handle, ref id, out var newEvent));
        return new EventDescription(newEvent);
    }

    public GUID GetID(string path)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_LookupID(Handle, path, out var id));
        return id;
    }

    public string GetPath(GUID id)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_LookupPath(Handle, ref id, out var path, 256, out _));
        return path;
    }

    public VolumeInfo GetParameter(string name)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_GetParameterByName(Handle, name, out var value, out var finalValue));
        return new(value, finalValue);
    }

    public VolumeInfo GetParameter(PARAMETER_ID id)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_GetParameterByID(Handle, id, out var value, out var finalValue));
        return new(value, finalValue);
    }

    public void SetParameter(string name, float value, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_SetParameterByName(Handle, name, value, ignoreSeekSpeed));
    }

    public void SetParameter(PARAMETER_ID id, float value, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_SetParameterByID(Handle, id, value, ignoreSeekSpeed));
    }

    public void SetParameterWithLabel(string name, string label, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_SetParameterByNameWithLabel(Handle, name, label, ignoreSeekSpeed));
    }

    public void SetParameterWithLabel(PARAMETER_ID id, string label, bool ignoreSeekSpeed)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_SetParameterByIDWithLabel(Handle, id, label, ignoreSeekSpeed));
    }

    public void SetCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE type)
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_SetCallback(Handle, callback, type));
    }

    public virtual void Update()
    {
        EnsureHandleValid();

        CallAndThrow(Native.FMOD_Studio_System_Update(Handle));
    }

    public void UnloadAll()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_UnloadAll(Handle));
    }

    public void FlushCommandQueue()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_FlushCommands(Handle));
    }

    public void FlushSampleLoading()
    {
        EnsureHandleValid();
        CallAndThrow(Native.FMOD_Studio_System_FlushSampleLoading(Handle));
    }

    protected override void FreeNativeResources()
    {
        if (IsValid)
        {
            CallAndThrow(Native.FMOD_Studio_System_Release(Handle));
        }
    }

    protected void EnsureHandleValid()
    {
        if (!IsValid)
            throw new FmodStudioException("Studio System source handle is not valid.");
    }
}