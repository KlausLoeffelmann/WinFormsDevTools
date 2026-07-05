namespace DevTools.RuntimeDeploy.Engine.Packaging;

/// <summary>
///  Metadata describing one assembly file included in a <c>.netdeploy</c>
///  package.
/// </summary>
public sealed record PackageAssemblyEntry(
    string FileName,
    string RelativePath,
    long SizeBytes,
    string Sha256);

/// <summary>
///  The <c>manifest.json</c> entry stored alongside the assemblies inside a
///  <c>.netdeploy</c> package (a plain zip archive).
/// </summary>
public sealed record PackageManifest(
    int SchemaVersion,
    DateTime CreatedUtc,
    string Tfm,
    int TfmMajorVersion,
    string Configuration,
    string? Platform,
    IReadOnlyList<PackageAssemblyEntry> Assemblies)
{
    /// <summary>
    ///  Current schema version written by this build of the tool. Bump this
    ///  when the shape of <see cref="PackageManifest"/> changes in a way that
    ///  is not purely additive.
    /// </summary>
    public const int CurrentSchemaVersion = 1;
}
