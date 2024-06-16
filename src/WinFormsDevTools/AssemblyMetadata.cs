namespace WinFormsDevTools;

internal readonly struct AssemblyMetadata
{
    public string AssemblyName { get; init; }
    public string PublicKeyToken { get; init; }
    public string AssemblyVersion { get; init; }
    public string FileVersion { get; init; }
}
