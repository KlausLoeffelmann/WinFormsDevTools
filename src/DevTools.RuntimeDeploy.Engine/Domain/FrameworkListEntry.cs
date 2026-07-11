namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  One row in <c>FrameworkList.xml</c>: the per-assembly identity and
///  versioning attributes the manifest carries.
/// </summary>
/// <param name="FileType">
///  The <c>Type</c> attribute value (e.g. <c>"WinForms"</c>,
///  <c>"Analyzer"</c>). See <see cref="AssemblyFileTypeClassifier"/>.
/// </param>
/// <param name="RelativePath">
///  The path of the file relative to the ref-pack base, in Windows
///  backslash form. The editor converts to forward-slash on write.
/// </param>
/// <param name="AssemblyName">The simple assembly name (no version, no token).</param>
/// <param name="PublicKeyToken">Lower-case hex of the strong-name token.</param>
/// <param name="AssemblyVersion">Already rounded to the SDK's <c>major.0.0.0</c> shape.</param>
/// <param name="FileVersion">Already rounded to the SDK's <c>major.0.0.0</c> shape.</param>
/// <param name="Profile">Typically <c>"WindowsForms"</c>.</param>
public sealed record FrameworkListEntry(
    string FileType,
    string RelativePath,
    string AssemblyName,
    string PublicKeyToken,
    string AssemblyVersion,
    string FileVersion,
    string Profile);
