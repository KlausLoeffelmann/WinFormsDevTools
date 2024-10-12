using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static DevTools.Libs.DebugListener.ExtendedDebugInfo;

namespace DevTools.Libs.DebugListener;

/// <summary>
///  Represents extended debug information.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly partial struct ExtendedDebugInfo(
    TimeSpan timestamp,
    ushort processId,
    ushort threadId = default,
    ushort methodId = default,
    ushort filenameId = default,
    ushort categoryId = default,
    DebugInfoCommandId command = default)
{
    [FieldOffset(0)]
    private readonly ushort _leadIn = 0x4242;

    [FieldOffset(2)]
    private readonly TimeSpan _timeStamp = timestamp;

    [FieldOffset(10)]
    private readonly ushort _threadId = threadId;

    [FieldOffset(12)]
    private readonly ushort _methodId = methodId;

    [FieldOffset(14)]
    private readonly ushort _filenameId = filenameId;

    [FieldOffset(16)]
    private readonly ushort _categoryId = categoryId;

    [FieldOffset(18)]
    private readonly DebugInfoCommandId _command = command;

    [FieldOffset(20)]
    private readonly ushort _processId = processId;

    /// <summary>
    ///  Gets the timestamp.
    /// </summary>
    public readonly TimeSpan Timestamp => _timeStamp;

    /// <summary>
    ///  Gets the process ID.
    /// </summary>
    public readonly ushort ProcessId => _processId;

    /// <summary>
    ///  Gets the thread ID.
    /// </summary>
    public readonly string ThreadId => _processStringLookup[_processId][_threadId];

    /// <summary>
    ///  Gets the method name.
    /// </summary>
    public readonly string Method => _processStringLookup[_processId][_methodId];

    /// <summary>
    ///  Gets the filename.
    /// </summary>
    public readonly string Filename => _processStringLookup[_processId][_filenameId];

    /// <summary>
    ///  Gets the category.
    /// </summary>
    public readonly string Category => _processStringLookup[_processId][_categoryId];

    /// <summary>
    ///  Gets the debug info command.
    /// </summary>
    public readonly DebugInfoCommandId Command => _command;

    // Lookup for ProcessId(index => string)
    private static readonly Dictionary<int, Dictionary<ushort, string>> _processStringLookup = [];

    // Lookup for ProcessId(string => Index)
    private static readonly Dictionary<int, Dictionary<string, ushort>> _processStringReverseLookup = [];

    /// <summary>
    ///  Converts a byte array to an instance of ExtendedDebugInfo.
    /// </summary>
    /// <param name="data">The byte array.</param>
    /// <returns>The instance of ExtendedDebugInfo.</returns>
    public unsafe static ExtendedDebugInfo FromByteArray(byte[] data) =>
        // Ensure the data is the correct size
        data.Length != sizeof(ExtendedDebugInfo) - 2
            ? throw new ArgumentException("Data must be the same size as the ExtendedDebugInfo struct.")
            : Unsafe.As<byte, ExtendedDebugInfo>(ref data[0]);

    /// <summary>
    ///  Converts a base64 string to an instance of ExtendedDebugInfo and a message.
    /// </summary>
    /// <param name="data">The base64 string.</param>
    /// <returns>The instance of ExtendedDebugInfo and the message.</returns>
    public unsafe static (ExtendedDebugInfo debugInfo, string message) FromBase64LeadMessage(string data)
    {
        // First 20 bytes are the ExtendedDebugInfo struct, then the message.
        byte[] buffer = Convert.FromBase64String(data[..20]);
        ExtendedDebugInfo debugInfo = FromByteArray(buffer);
        string message = data[20..];

        return (debugInfo, message);
    }

    /// <summary>
    ///  Converts a base64 string to an instance of ExtendedDebugInfo.
    /// </summary>
    /// <param name="data">The base64 string.</param>
    /// <returns>The instance of ExtendedDebugInfo.</returns>
    public unsafe static ExtendedDebugInfo FromBase64(string data)
    {
        byte[] buffer = Convert.FromBase64String(data);
        return FromByteArray(buffer);
    }

    /// <summary>
    ///  Converts the instance of ExtendedDebugInfo to a byte array.
    /// </summary>
    /// <returns>The byte array.</returns>
    public readonly unsafe byte[] ToByteArray()
    {
        byte[] result = new byte[sizeof(ExtendedDebugInfo)];
        Unsafe.As<byte, ExtendedDebugInfo>(ref result[0]) = this;

        return result;
    }

    /// <summary>
    ///  Converts the instance of ExtendedDebugInfo to a base64 string.
    /// </summary>
    /// <returns>The base64 string.</returns>
    public readonly unsafe string ToBase64()
    {
        byte[] data = ToByteArray();
        return Convert.ToBase64String(data);
    }

    /// <summary>
    ///  Adds or gets the process string index.
    /// </summary>
    /// <param name="processId">The process ID.</param>
    /// <param name="value">The string value.</param>
    /// <param name="stringId">The string ID.</param>
    /// <returns>True if the string was added, false otherwise.</returns>
    public static bool AddOrGetProcessStringIndex(int processId, string value, out ushort stringId)
    {
        if (!_processStringReverseLookup.TryGetValue(processId, out Dictionary<string, ushort>? reverseLookup))
        {
            reverseLookup = new Dictionary<string, ushort>();
            Dictionary<ushort, string> lookup = new Dictionary<ushort, string>();

            _processStringReverseLookup.Add(processId, reverseLookup);
            _processStringLookup.Add(processId, lookup);

            stringId = (ushort)(reverseLookup.Count + 1);
            reverseLookup.Add(value, stringId);
            lookup.Add(stringId, value);

            return true;
        }

        if (!reverseLookup.TryGetValue(value, out ushort innerStringId))
        {
            Dictionary<ushort, string> lookup = _processStringLookup[processId];

            innerStringId = (ushort)(reverseLookup.Count + 1);
            reverseLookup.Add(value, innerStringId);
            lookup.Add(innerStringId, value);
            stringId = innerStringId;

            return true;
        }

        stringId = innerStringId;

        return false;
    }

    /// <summary>
    ///  Gets the process string.
    /// </summary>
    /// <param name="processId">The process ID.</param>
    /// <param name="index">The index.</param>
    /// <returns>The process string.</returns>
    public static string? GetProcessString(int processId, ushort index) =>
        _processStringLookup.TryGetValue(processId, out Dictionary<ushort, string>? processStringLookup)
            ? processStringLookup.TryGetValue(index, out string? value)
                ? value
                : null
            : null;
}
