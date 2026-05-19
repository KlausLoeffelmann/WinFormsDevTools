namespace DevTools.RuntimeDeploy.Domain;

/// <summary>
///  Classifies an assembly file by its base name into a <c>FrameworkList.xml</c>
///  <c>Type</c> attribute value.
/// </summary>
/// <remarks>
///  <para>
///   The returned values (<c>WinForms</c>, <c>Drawing</c>, <c>Analyzer</c>,
///   <c>Primitive</c>, <c>Design</c>, <c>DrawingDesign</c>, <c>VisualBasic</c>,
///   <c>Accessibility</c>, <c>WindowsCore</c>) are the values the existing
///   tool has been writing to FrameworkList.xml.
///  </para>
///  <para>
///   <b>Important:</b> the upstream Microsoft SDK FrameworkList.xml is believed
///   to use only the values <c>Managed</c> and <c>Analyzer</c>. Do not change
///   the strings returned here without first diff-ing against an upstream
///   install of <c>Microsoft.WindowsDesktop.App.Ref</c>; changing them
///   silently could break SDK consumers.
///  </para>
/// </remarks>
internal static class AssemblyFileTypeClassifier
{
    /// <summary>
    ///  Returns the FrameworkList <c>Type</c> attribute value for an
    ///  assembly identified by its file name without extension.
    /// </summary>
    /// <param name="assemblyBaseName">
    ///  The assembly file name without extension (e.g. <c>System.Windows.Forms</c>,
    ///  <c>System.Windows.Forms.Analyzers</c>).
    /// </param>
    /// <returns>The classification string; <c>"Unknown"</c> when no rule matches.</returns>
    public static string Classify(string assemblyBaseName)
    {
        if (assemblyBaseName.StartsWith("System.Windows.Forms", StringComparison.Ordinal))
        {
            if (assemblyBaseName.Contains("Analyzers", StringComparison.Ordinal))
            {
                return "Analyzer";
            }

            if (assemblyBaseName.Contains("Primitives", StringComparison.Ordinal))
            {
                return "Primitive";
            }

            if (assemblyBaseName.Contains("Design", StringComparison.Ordinal))
            {
                return "Design";
            }

            return "WinForms";
        }

        if (assemblyBaseName.StartsWith("System.Drawing", StringComparison.Ordinal))
        {
            if (assemblyBaseName.Contains("Design", StringComparison.Ordinal))
            {
                return "DrawingDesign";
            }

            return "Drawing";
        }

        if (assemblyBaseName.StartsWith("Microsoft.VisualBasic", StringComparison.Ordinal))
        {
            return "VisualBasic";
        }

        if (assemblyBaseName.StartsWith("Accessibility", StringComparison.Ordinal))
        {
            return "Accessibility";
        }

        if (assemblyBaseName.StartsWith("System.Design", StringComparison.Ordinal))
        {
            return "Design";
        }

        if (assemblyBaseName.StartsWith("System.Private.Windows.Core", StringComparison.Ordinal))
        {
            return "WindowsCore";
        }

        return "Unknown";
    }
}
