namespace WfRuntimeDeploy.Views;

public partial class DeployRuntimeView
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
