using System.Diagnostics;
using System.Reflection;

namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Result of probing an assembly file for its strong-name and version metadata.
/// </summary>
public sealed record AssemblyProbeResult(
    string Name,
    string Version,
    string PublicKeyTokenHex,
    string? FileVersion);

/// <summary>
///  Reads assembly identity and version metadata from an assembly file
///  without keeping the file locked.
/// </summary>
/// <remarks>
///  <para>
///   Uses <see cref="AssemblyName.GetAssemblyName(string)"/> for the strong-name
///   identity (name, version, public key token) and
///   <see cref="FileVersionInfo.GetVersionInfo(string)"/> for the Win32
///   file-version resource. Both APIs open the file just long enough to read
///   metadata and then close it &#x2014; unlike <see cref="Assembly.LoadFrom(string)"/>,
///   which keeps the assembly loaded in the AppDomain (and the file locked)
///   for the lifetime of the process.
///  </para>
///  <para>
///   This replaces the old <c>AssemblyTempManager</c> + <c>AssemblyMetadataReader</c>
///   pair, the former of which existed only to work around the locking caused
///   by <see cref="Assembly.LoadFrom"/>, and the latter of which had a broken
///   <c>ComputePublicKeyToken</c> implementation (missing the byte-reversal
///   step required by the strong-name token format).
///  </para>
/// </remarks>
public static class AssemblyProbe
{
    /// <summary>
    ///  Attempts to read identity and version metadata from the assembly at
    ///  <paramref name="assemblyPath"/>.
    /// </summary>
    /// <param name="assemblyPath">The full path to the assembly file.</param>
    /// <returns>
    ///  An <see cref="AssemblyProbeResult"/>, or <see langword="null"/> if the
    ///  file is missing, is not a managed assembly, or has no strong-name token.
    /// </returns>
    public static AssemblyProbeResult? TryRead(string assemblyPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(assemblyPath);

        if (!File.Exists(assemblyPath))
        {
            return null;
        }

        AssemblyName name;
        try
        {
            name = AssemblyName.GetAssemblyName(assemblyPath);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"AssemblyProbe.TryRead: GetAssemblyName failed for '{assemblyPath}': {ex.Message}");
            return null;
        }

        byte[]? tokenBytes = name.GetPublicKeyToken();
        if (tokenBytes is null || tokenBytes.Length == 0)
        {
            return null;
        }

        string? fileVersion = null;
        try
        {
            fileVersion = FileVersionInfo.GetVersionInfo(assemblyPath).FileVersion;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"AssemblyProbe.TryRead: GetVersionInfo failed for '{assemblyPath}': {ex.Message}");
            // FileVersion is optional; the caller falls back to the sentinel.
        }

        return new AssemblyProbeResult(
            Name: name.Name ?? string.Empty,
            Version: name.Version?.ToString() ?? FrameworkVersionFormatter.FailedReadSentinel,
            PublicKeyTokenHex: Convert.ToHexStringLower(tokenBytes),
            FileVersion: fileVersion);
    }
}
