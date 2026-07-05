using System.Security.Cryptography;
using System.Text.Json;
using System.IO.Compression;
using DevTools.RuntimeDeploy.Engine.Domain;
using DevTools.RuntimeDeploy.Engine.Json;

namespace DevTools.RuntimeDeploy.Engine.Packaging;

/// <summary>
///  Creates <c>.netdeploy</c> packages: a zip archive containing a selected
///  set of assembly files plus a <c>manifest.json</c> describing them (TFM,
///  configuration, and a per-file SHA-256 for integrity verification on the
///  target machine).
/// </summary>
public static class PackageBuilder
{
    public const string ManifestEntryName = "manifest.json";

    /// <summary>
    ///  Writes <paramref name="outputPackageFile"/> containing every file in
    ///  <paramref name="assemblyFiles"/> plus a manifest describing them.
    /// </summary>
    /// <param name="assemblyFiles">The assembly files to include, flattened.</param>
    /// <param name="tfm">The target framework moniker the assemblies were built for (e.g. <c>"net10.0"</c>).</param>
    /// <param name="configuration">The build configuration (e.g. <c>"Release"</c>).</param>
    /// <param name="outputPackageFile">
    ///  Destination <c>.netdeploy</c> file. Overwritten if it already exists.
    /// </param>
    public static async Task<PackageManifest> CreatePackageAsync(
        IEnumerable<FileInfo> assemblyFiles,
        string tfm,
        string configuration,
        FileInfo outputPackageFile,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(assemblyFiles);
        ArgumentException.ThrowIfNullOrEmpty(tfm);
        ArgumentException.ThrowIfNullOrEmpty(configuration);
        ArgumentNullException.ThrowIfNull(outputPackageFile);

        FileInfo[] files = [.. assemblyFiles];
        if (files.Length == 0)
        {
            throw new ArgumentException("At least one assembly file must be provided.", nameof(assemblyFiles));
        }

        outputPackageFile.Directory?.Create();

        if (outputPackageFile.Exists)
        {
            outputPackageFile.Delete();
        }

        List<PackageAssemblyEntry> entries = [];

        using (FileStream zipStream = outputPackageFile.Create())
        using (ZipArchive archive = new(zipStream, ZipArchiveMode.Create))
        {
            HashSet<string> uniqueNames = new(StringComparer.OrdinalIgnoreCase);

            foreach (FileInfo file in files)
            {
                if (!uniqueNames.Add(file.Name))
                {
                    // Skip duplicates - the caller may have selected the same
                    // assembly from more than one directory group.
                    continue;
                }

                string sha256 = await ComputeSha256Async(file, cancellationToken);

                ZipArchiveEntry entry = archive.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Optimal);

                entries.Add(new PackageAssemblyEntry(
                    FileName: file.Name,
                    RelativePath: entry.FullName,
                    SizeBytes: file.Length,
                    Sha256: sha256));
            }

            PackageManifest manifest = new(
                SchemaVersion: PackageManifest.CurrentSchemaVersion,
                CreatedUtc: DateTime.UtcNow,
                Tfm: tfm,
                TfmMajorVersion: TfmMajorVersionParser.Parse(tfm),
                Configuration: configuration,
                Platform: null,
                Assemblies: entries);

            ZipArchiveEntry manifestEntry = archive.CreateEntry(ManifestEntryName, CompressionLevel.Optimal);
            using (Stream manifestStream = manifestEntry.Open())
            {
                await JsonSerializer.SerializeAsync(manifestStream, manifest, EngineJsonContext.Default.PackageManifest, cancellationToken);
            }

            return manifest;
        }
    }

    /// <summary>
    ///  Reads the <see cref="PackageManifest"/> out of an existing
    ///  <c>.netdeploy</c> package without extracting any assembly files.
    /// </summary>
    public static async Task<PackageManifest> ReadManifestAsync(FileInfo packageFile, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(packageFile);

        using FileStream zipStream = packageFile.OpenRead();
        using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);

        ZipArchiveEntry manifestEntry = archive.GetEntry(ManifestEntryName)
            ?? throw new InvalidDataException($"'{packageFile.FullName}' does not contain a '{ManifestEntryName}' entry.");

        using Stream manifestStream = manifestEntry.Open();
        return await JsonSerializer.DeserializeAsync(manifestStream, EngineJsonContext.Default.PackageManifest, cancellationToken)
            ?? throw new InvalidDataException($"'{packageFile.FullName}' has an unreadable manifest.");
    }

    /// <summary>
    ///  Extracts every assembly file from <paramref name="packageFile"/> into
    ///  <paramref name="destinationDirectory"/>, overwriting existing files.
    /// </summary>
    public static void ExtractAssemblies(FileInfo packageFile, DirectoryInfo destinationDirectory)
    {
        ArgumentNullException.ThrowIfNull(packageFile);
        ArgumentNullException.ThrowIfNull(destinationDirectory);

        destinationDirectory.Create();

        using FileStream zipStream = packageFile.OpenRead();
        using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);

        foreach (ZipArchiveEntry entry in archive.Entries)
        {
            if (string.Equals(entry.FullName, ManifestEntryName, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string destinationPath = Path.Combine(destinationDirectory.FullName, entry.Name);
            entry.ExtractToFile(destinationPath, overwrite: true);
        }
    }

    internal static async Task<string> ComputeSha256Async(FileInfo file, CancellationToken cancellationToken)
    {
        using FileStream stream = file.OpenRead();
        byte[] hash = await SHA256.HashDataAsync(stream, cancellationToken);
        return Convert.ToHexStringLower(hash);
    }
}
