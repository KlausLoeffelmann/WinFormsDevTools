namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Outcome of probing an assembly and (potentially) upserting its entry
///  into <c>FrameworkList.xml</c>.
/// </summary>
public enum AssemblyManifestProcessResult
{
    /// <summary>The destination assembly file was not found.</summary>
    MissingAssembly,

    /// <summary>A new <c>&lt;File&gt;</c> element was added to the manifest.</summary>
    Created,

    /// <summary>An existing entry was found but its <c>PublicKeyToken</c> differs.</summary>
    PublicKeyDoesNotMatch,

    /// <summary>An existing entry's <c>PublicKeyToken</c> was overwritten.</summary>
    PublicKeyUpdated,

    /// <summary>The destination file could not be read as a managed assembly.</summary>
    InvalidAssembly,

    /// <summary>The assembly has no strong-name public key token.</summary>
    MissingPublicKey,

    /// <summary>The manifest XML failed to load.</summary>
    InvalidXmlFile,

    /// <summary>An existing entry matched and required no changes.</summary>
    OK,
}
