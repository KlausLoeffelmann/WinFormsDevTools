namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Normalises assembly / framework version strings for inclusion in the
///  <c>FrameworkList.xml</c> manifest of the .NET Desktop ref-pack.
/// </summary>
public static class FrameworkVersionFormatter
{
    /// <summary>
    ///  Sentinel value returned by the file/assembly-version helpers when
    ///  metadata could not be read. A version starting with this prefix tells
    ///  <see cref="ToMajorOnly"/> to fall back to the SDK framework version
    ///  instead of using the (clearly invalid) assembly version.
    /// </summary>
    public const string FailedReadSentinelPrefix = "42";

    /// <summary>
    ///  The full sentinel string written to manifest entries when an assembly
    ///  version cannot be obtained.
    /// </summary>
    public const string FailedReadSentinel = "42.42.42.42424";

    /// <summary>
    ///  Returns <c>major.minor</c> for a version like <c>9.0.0</c> (which
    ///  becomes <c>9.0</c>). A single-component version such as <c>9</c>
    ///  becomes <c>9.0</c>.
    /// </summary>
    /// <param name="versionString">
    ///  The dotted-numeric version string. Must contain at least one component.
    /// </param>
    /// <exception cref="ArgumentException">
    ///  <paramref name="versionString"/> contained no version components.
    /// </exception>
    public static string ToMajorMinor(string versionString)
    {
        ArgumentNullException.ThrowIfNull(versionString);

        string[] items = versionString.Split('.');

        return items.Length switch
        {
            1 => $"{items[0]}.0",
            > 1 => $"{items[0]}.{items[1]}",
            _ => throw new ArgumentException(
                "Could not figure out .NET Major/Minor Version for Ref Assemblies.",
                nameof(versionString)),
        };
    }

    /// <summary>
    ///  Returns the "rounded" <c>major.0.0.0</c> form that the
    ///  <c>AssemblyVersion</c> and <c>FileVersion</c> attributes in
    ///  <c>FrameworkList.xml</c> use; e.g. <c>9.6.4-dev</c> becomes
    ///  <c>9.0.0.0</c>.
    /// </summary>
    /// <remarks>
    ///  When <paramref name="assemblyVersion"/> starts with
    ///  <see cref="FailedReadSentinelPrefix"/> the SDK framework version is
    ///  used instead (the assembly version was unreadable). When the version
    ///  has fewer than four components it is returned as-is.
    /// </remarks>
    /// <param name="actualFrameworkVersion">The installed SDK version (e.g. <c>10.0.0</c>).</param>
    /// <param name="assemblyVersion">The version read from the assembly being deployed.</param>
    public static string ToMajorOnly(string actualFrameworkVersion, string assemblyVersion)
    {
        ArgumentNullException.ThrowIfNull(actualFrameworkVersion);
        ArgumentNullException.ThrowIfNull(assemblyVersion);

        string version = assemblyVersion.StartsWith(FailedReadSentinelPrefix, StringComparison.Ordinal)
            ? actualFrameworkVersion
            : assemblyVersion;

        string[] parts = version.Split('.');

        if (parts.Length < 4)
        {
            return version;
        }

        return $"{parts[0]}.0.0.0";
    }
}
