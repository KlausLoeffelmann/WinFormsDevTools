namespace DevTools.RuntimeDeploy.Engine.PatchBackup;

/// <summary>
///  Thrown by <see cref="RestoreService"/> when a backup fails a plausibility
///  check against the current machine (e.g. its TFM major version is not
///  present/installed here) - restoring it would very likely corrupt the
///  local .NET Desktop runtime rather than repair it.
/// </summary>
public sealed class RestorePlausibilityException(string message) : Exception(message)
{
}
