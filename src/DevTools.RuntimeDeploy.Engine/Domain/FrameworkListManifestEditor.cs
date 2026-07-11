using System.Xml.Linq;

namespace DevTools.RuntimeDeploy.Engine.Domain;

/// <summary>
///  Reads, mutates, and writes a <c>FrameworkList.xml</c> manifest under
///  <c>Microsoft.WindowsDesktop.App.Ref\&lt;version&gt;\data</c>.
/// </summary>
/// <remarks>
///  <para>
///   Construction loads the document once and pre-builds a "last entry
///   per <c>Type</c>" index so subsequent <see cref="Upsert"/> calls can
///   insert new entries in the right neighbourhood without re-walking the
///   element list. Call <see cref="Save"/> exactly once after batching
///   <see cref="Upsert"/> calls.
///  </para>
///  <para>
///   The path-equality check between an in-memory <c>FileInfo</c> path
///   (Windows backslashes) and the on-disk <c>&lt;File Path="..."&gt;</c>
///   attribute (forward slashes) is the existing zero-allocation span
///   comparator copied from the original <c>UpdateAssemblyInfo</c>.
///  </para>
/// </remarks>
public sealed class FrameworkListManifestEditor
{
    private const string FileListElementName = "FileList";
    private const string FileElementName = "File";

    private readonly string _xmlFilePath;
    private readonly XDocument _xmlDoc;
    private readonly XElement _fileList;

    /// <summary>
    ///  Last <c>&lt;File&gt;</c> element observed for each distinct
    ///  <c>Type</c> attribute value. Used as the insertion anchor for
    ///  newly-created entries so the manifest stays grouped by type.
    /// </summary>
    private readonly Dictionary<string, XElement> _lastEntryByType;

    /// <summary>
    ///  Loads <paramref name="xmlFilePath"/> and prepares the in-memory index.
    /// </summary>
    /// <exception cref="FileNotFoundException">When the file is missing.</exception>
    /// <exception cref="InvalidDataException">
    ///  When the document has no <c>FileList</c> root element.
    /// </exception>
    public FrameworkListManifestEditor(string xmlFilePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlFilePath);

        _xmlFilePath = xmlFilePath;
        _xmlDoc = XDocument.Load(xmlFilePath);
        _fileList = _xmlDoc.Element(FileListElementName)
            ?? throw new InvalidDataException(
                $"'{xmlFilePath}' is not a FrameworkList manifest: missing <{FileListElementName}> root element.");

        _lastEntryByType = BuildLastEntryByTypeIndex(_fileList);
    }

    /// <summary>
    ///  Inserts <paramref name="entry"/> if the manifest has no matching
    ///  <c>Path</c>, or compares its <c>PublicKeyToken</c> against the
    ///  existing entry and (optionally) overwrites it.
    /// </summary>
    /// <param name="entry">The desired manifest row.</param>
    /// <param name="updatePublicKey">
    ///  When <see langword="true"/>, an existing entry with a mismatched
    ///  public-key token is rewritten with <paramref name="entry"/>'s token.
    /// </param>
    public AssemblyManifestProcessResult Upsert(FrameworkListEntry entry, bool updatePublicKey)
    {
        ArgumentNullException.ThrowIfNull(entry);

        XElement? existingFile = FindByPath(entry.RelativePath);

        if (existingFile is null)
        {
            XElement newFile = new(FileElementName,
                new XAttribute("Type", entry.FileType),
                new XAttribute("Path", entry.RelativePath.Replace('\\', '/')),
                new XAttribute("AssemblyName", entry.AssemblyName),
                new XAttribute("PublicKeyToken", entry.PublicKeyToken),
                new XAttribute("AssemblyVersion", entry.AssemblyVersion),
                new XAttribute("FileVersion", entry.FileVersion),
                new XAttribute("Profile", entry.Profile));

            if (_lastEntryByType.TryGetValue(entry.FileType, out XElement? lastTypeEntry))
            {
                lastTypeEntry.AddAfterSelf(newFile);
            }
            else
            {
                _fileList.Add(newFile);
            }

            _lastEntryByType[entry.FileType] = newFile;
            return AssemblyManifestProcessResult.Created;
        }

        string? existingToken = existingFile.Attribute("PublicKeyToken")?.Value;
        if (existingToken != entry.PublicKeyToken)
        {
            if (!updatePublicKey)
            {
                return AssemblyManifestProcessResult.PublicKeyDoesNotMatch;
            }

            existingFile.SetAttributeValue("PublicKeyToken", entry.PublicKeyToken);
            return AssemblyManifestProcessResult.PublicKeyUpdated;
        }

        return AssemblyManifestProcessResult.OK;
    }

    /// <summary>
    ///  Persists pending changes to the manifest file. Intended to be
    ///  called exactly once after a batch of <see cref="Upsert"/> calls.
    /// </summary>
    public void Save() => _xmlDoc.Save(_xmlFilePath);

    private XElement? FindByPath(string relativePath)
    {
        foreach (XElement file in _fileList.Elements(FileElementName))
        {
            string? pathAttr = file.Attribute("Path")?.Value;
            if (pathAttr is not null && IsPathMatch(pathAttr.AsSpan(), relativePath))
            {
                return file;
            }
        }

        return null;
    }

    private static Dictionary<string, XElement> BuildLastEntryByTypeIndex(XElement fileList)
    {
        Dictionary<string, XElement> index = [];

        foreach (XElement file in fileList.Elements(FileElementName))
        {
            string? type = file.Attribute("Type")?.Value;
            if (type is null)
            {
                continue;
            }

            // Overwrite -- by walking the elements in document order we end
            // up with the *last* entry per type, which is what we want as
            // the insertion anchor.
            index[type] = file;
        }

        return index;
    }

    /// <summary>
    ///  Case-sensitive path equality that treats <c>'/'</c> in
    ///  <paramref name="path"/> as equal to <c>'\'</c> in
    ///  <paramref name="deltaPath"/>. Zero-allocation; see the original
    ///  comment from the deleted UpdateAssemblyInfo.
    /// </summary>
    private static bool IsPathMatch(ReadOnlySpan<char> path, string deltaPath)
    {
        if (path.Length != deltaPath.Length)
        {
            return false;
        }

        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] != deltaPath[i] && !(path[i] == '/' && deltaPath[i] == '\\'))
            {
                return false;
            }
        }

        return true;
    }
}
