namespace ChromaFMOD;

public static class NativeInitializer
{
    [ModuleInitializer]
    public static void LoadNatives() => NativeLoader.LoadNatives(false);
}