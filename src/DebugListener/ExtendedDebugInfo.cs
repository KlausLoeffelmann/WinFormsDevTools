using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static DebugListener.ExtendedDebugInfo;

namespace DebugListener;

[StructLayout(LayoutKind.Explicit)]
public readonly partial struct ExtendedDebugInfo(
    DateTime dateTime,
    ushort threadId = default,
    ushort methodId = default,
    ushort filenameId = default,
    ushort categoryId = default,
    DebugInfoCommandId command = default)
{
    [FieldOffset(0)]
    private readonly ushort _leadIn = 0x4242;
    [FieldOffset(2)]
    private readonly DateTime _timeStamp = dateTime;
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

    // Lookup for ProcessId(index => string)
    private static readonly Dictionary<int, Dictionary<ushort, string>> _processStringLookup = [];

    // Lookup for ProcessId(string => Index)
    private static readonly Dictionary<int, Dictionary<string, ushort>> _processStringReverseLookup = [];

    public unsafe static ExtendedDebugInfo FromByteArray(byte[] data) =>
        // Ensure the data is the correct size
        data.Length != sizeof(ExtendedDebugInfo)
            ? throw new ArgumentException("Data must be the same size as the ExtendedDebugInfo struct.")
            : Unsafe.As<byte, ExtendedDebugInfo>(ref data[0]);

    public unsafe static (ExtendedDebugInfo debugInfo, string message) FromBase64LeadMessage(string data)
    {
        // First 20 bytes are the ExtendedDebugInfo struct, then the message.
        byte[] buffer = Convert.FromBase64String(data[..20]);
        ExtendedDebugInfo debugInfo = FromByteArray(buffer);
        string message = data[20..];

        return (debugInfo, message);
    }

    public unsafe static ExtendedDebugInfo FromBase64(string data)
    {
        byte[] buffer = Convert.FromBase64String(data);
        return FromByteArray(buffer);
    }

    public readonly unsafe byte[] ToByteArray()
    {
        byte[] result = new byte[sizeof(ExtendedDebugInfo)];
        Unsafe.As<byte, ExtendedDebugInfo>(ref result[0]) = this;

        return result;
    }

    public readonly unsafe string ToBase64()
    {
        byte[] data = ToByteArray();
        return Convert.ToBase64String(data);
    }

    public static bool AddOrGetProcessStringIndex(int processId, string value, out ushort stringId)
    {
        if (!_processStringReverseLookup.TryGetValue(processId, out Dictionary<string, ushort>? reverseLookup))
        {
            reverseLookup = [];
            Dictionary<ushort, string> lookup = [];

            _processStringReverseLookup.Add(processId, reverseLookup);
            _processStringLookup.Add(processId, lookup);

            stringId = (ushort)reverseLookup.Count;
            reverseLookup.Add(value, stringId);
            lookup.Add(stringId, value);

            return true;
        }

        if (!reverseLookup.TryGetValue(value, out ushort innerStringId))
        {
            Dictionary<ushort, string> lookup = _processStringLookup[processId];

            innerStringId = (ushort)reverseLookup.Count;
            reverseLookup.Add(value, innerStringId);
            lookup.Add(innerStringId, value);
            stringId = innerStringId;

            return true;
        }

        stringId = innerStringId;

        return false;
    }

    public static string? GetProcessString(int processId, ushort index) =>
        _processStringLookup.TryGetValue(processId, out Dictionary<ushort, string>? processStringLookup)
            ? processStringLookup.TryGetValue(index, out string? value)
                ? value
                : null
            : null;
}
