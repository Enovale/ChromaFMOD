using System.Numerics;
using Chroma;
using Chroma.Diagnostics;
using Chroma.Graphics;
using Chroma.Graphics.TextRendering.TrueType;
using Chroma.Input;
using ChromaFMOD.Studio;
using FMOD;
using FMOD.Studio;
using EventInstance = ChromaFMOD.Studio.EventInstance;
using INITFLAGS = FMOD.Studio.INITFLAGS;
using Native = FMOD.Studio.Native;

namespace ChromaFMOD.Tests;

public class GameCore : Game
{
    private StudioSystem _studioSystem;
    private Bank MasterBank;
    private EventDescription PistolEvent;
    private EventDescription FunnyCarNoise;
    private EventDescription BRRRRRRRRRRRR;
    private EventInstance CarNoiseInstance;
    private EventInstance RATATATATATATATA;

    public GameCore() : base(new(false, false))
    {
        _studioSystem = new StudioSystem(4, INITFLAGS.LIVEUPDATE);
        MasterBank = _studioSystem.LoadBankFile("Content/Master.bank", LOAD_BANK_FLAGS.NORMAL);
        _studioSystem.LoadBankFile("Content/Master.strings.bank", LOAD_BANK_FLAGS.NORMAL);
        _studioSystem.LoadBankFile("Content/VO.bank", LOAD_BANK_FLAGS.NORMAL);
        _studioSystem.LoadBankFile("Content/SFX.bank", LOAD_BANK_FLAGS.NORMAL);
        _studioSystem.LoadBankFile("Content/Vehicles.bank", LOAD_BANK_FLAGS.NORMAL);
        MasterBank.LoadSampleData();
        PistolEvent = _studioSystem.GetEvent("event:/Weapons/Pistol");
        PistolEvent.SetCallback(ShotsFired, EVENT_CALLBACK_TYPE.ALL);
        FunnyCarNoise = _studioSystem.GetEvent("event:/Vehicles/Car Engine");
        FunnyCarNoise.SetCallback(ShotsFired, EVENT_CALLBACK_TYPE.ALL);
        BRRRRRRRRRRRR = _studioSystem.GetEvent("event:/Weapons/Machine Gun");
    }

    private RESULT ShotsFired(EVENT_CALLBACK_TYPE type, EventInstance eventinstance, object? parameter)
    {
        if(parameter is not null)
            Console.Write(parameter);

        Console.WriteLine(type);
        return RESULT.OK;
    }

    protected override void Update(float delta)
    {
        _studioSystem.Update();
        if (CarNoiseInstance is { Disposed: false })
        {
            CarNoiseInstance.SetParameter("RPM", 9500 * (Mouse.GetPosition().X / Window.Width), false);
        }

        Window.Title = $"{PerformanceCounter.FPS}";
    }

    protected override void KeyPressed(KeyEventArgs e)
    {
        if (e.IsRepeat)
            return;
        
        if (e.KeyCode == KeyCode.A)
        {
            var instance = new EventInstance(PistolEvent);
            instance.Start();
            instance.Dispose();
        }

        if (e.KeyCode == KeyCode.E)
        {
            RATATATATATATATA = new EventInstance(BRRRRRRRRRRRR);
            RATATATATATATATA.Start();
        }

        if (e.KeyCode == KeyCode.Space)
        {
            CarNoiseInstance = new EventInstance(FunnyCarNoise);
            CarNoiseInstance.Start();
        }
    }

    protected override void KeyReleased(KeyEventArgs e)
    {
        if(e.KeyCode == KeyCode.E)
            RATATATATATATATA.Stop(STOP_MODE.ALLOWFADEOUT, true);
        if(e.KeyCode == KeyCode.Space)
            CarNoiseInstance.Stop(STOP_MODE.ALLOWFADEOUT, true);
    }

    protected override void Draw(RenderContext context)
    {
        context.DrawString(
            "Welcome to the Chroma FMOD Test!\n" +
            "To test a basic sound, press A.\n" +
            "To test parameters, hold space and move your mouse around the screen.",
            Vector2.Zero, Color.White);
    }
}