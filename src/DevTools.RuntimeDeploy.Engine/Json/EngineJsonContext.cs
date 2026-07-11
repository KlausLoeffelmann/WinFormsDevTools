using System.Text.Json.Serialization;
using DevTools.RuntimeDeploy.Engine.PatchBackup;
using DevTools.RuntimeDeploy.Engine.Packaging;

namespace DevTools.RuntimeDeploy.Engine.Json;

/// <summary>
///  Source-generated <see cref="JsonSerializerContext"/> covering every DTO
///  serialized to/from disk by the Engine (package manifests, backup
///  manifests, and the CLI control-file settings). Using source generation
///  instead of reflection-based (de)serialization keeps the self-contained,
///  trimmed CLI tool working correctly under trimming.
/// </summary>
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(PackageManifest))]
[JsonSerializable(typeof(PackageAssemblyEntry))]
[JsonSerializable(typeof(BackupManifest))]
[JsonSerializable(typeof(BackupFileEntry))]
[JsonSerializable(typeof(RuntimePatcherSettings))]
public sealed partial class EngineJsonContext : JsonSerializerContext
{
}
