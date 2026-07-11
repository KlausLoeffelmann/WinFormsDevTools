using System.Text.RegularExpressions;

namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Extracts the major version number from a target framework moniker
///  (e.g. <c>"net10.0"</c> -&gt; <c>10</c>, <c>"net9.0-windows10.0.19041.0"</c>
///  -&gt; <c>9</c>).
/// </summary>
public static partial class TfmMajorVersionParser
{
    [GeneratedRegex(@"^\D*(\d+)")]
    private static partial Regex LeadingDigitsRegex();

    /// <summary>
    ///  Attempts to parse the major version number out of <paramref name="tfm"/>.
    /// </summary>
    public static bool TryParse(string? tfm, out int majorVersion)
    {
        majorVersion = 0;

        if (string.IsNullOrWhiteSpace(tfm))
        {
            return false;
        }

        Match match = LeadingDigitsRegex().Match(tfm);
        if (!match.Success)
        {
            return false;
        }

        return int.TryParse(match.Groups[1].Value, out majorVersion);
    }

    /// <summary>
    ///  Parses the major version number out of <paramref name="tfm"/>, throwing
    ///  when it cannot be determined.
    /// </summary>
    public static int Parse(string tfm)
    {
        if (!TryParse(tfm, out int majorVersion))
        {
            throw new ArgumentException($"Could not determine the major version from TFM '{tfm}'.", nameof(tfm));
        }

        return majorVersion;
    }
}
