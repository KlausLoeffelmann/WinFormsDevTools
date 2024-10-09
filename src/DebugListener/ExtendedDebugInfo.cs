using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static DebugListener.ExtendedDebugInfo;

namespace DebugListener;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ExtendedDebugInfo(
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

    public readonly unsafe byte[] ToByteArray()
    {
        byte[] result = new byte[sizeof(ExtendedDebugInfo)];
        Unsafe.As<byte, ExtendedDebugInfo>(ref result[0]) = this;

        return result;
    }

    public unsafe static ExtendedDebugInfo FromByteArray(byte[] data) =>
        // Ensure the data is the correct size
        data.Length != sizeof(ExtendedDebugInfo)
            ? throw new ArgumentException("Data must be the same size as the ExtendedDebugInfo struct.")
            : Unsafe.As<byte, ExtendedDebugInfo>(ref data[0]);

    public enum DebugInfoCommandId : ushort
    {
        Message = 0,
        MethodNameDefinition = 1,
        FileNameDefinition = 2,
        ProcessNameDefinition = 3,
        CategoryDefinition = 4,
        SignalStart = 100,
        SignalReset = 101,
        SignalStop = 102,
        SignalPause = 103,
        SignalResume = 104,
        SignalTerminate = 105,
        SignalLapTime = 106,
    }
}
