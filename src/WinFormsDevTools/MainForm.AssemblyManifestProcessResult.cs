namespace WinFormsDevTools;

public partial class MainForm
{
    private enum AssemblyManifestProcessResult
    {
        MissingAssembly,
        Created,
        PublicKeyDoesNotMatch,
        PublicKeyUpdated,
        InvalidAssembly,
        MissingPublicKey,
        InvalidXmlFile,
        OK
    }
}
