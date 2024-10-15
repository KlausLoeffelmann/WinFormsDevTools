// -------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// -------------------------------------------------------------------

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace DevTools.Libs.DebugListener;

public partial class WinFormsPerformanceLogging
{
    private static readonly string s_leadIn = Encoding.UTF8.GetString([0x42, 0x42]);

    /// <summary>
    ///  Represents extended debug information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly partial struct ExtendedDebugInfo(
        DateTime timestamp,
        ushort processId,
        ushort threadId = default,
        ushort lineNo = default,
        ushort value1 = default,
        ushort value2 = default,
        ushort value3 = default,
        ExtendedDebugInfo.DebugInfoCommandId command = default)
    {
        [FieldOffset(0)]
        private readonly ushort _leadIn = 0x4242;

        [FieldOffset(2)]
        private readonly DateTime _timeStamp = timestamp;

        [FieldOffset(10)]
        private readonly ushort _threadId = threadId;

        [FieldOffset(12)]
        private readonly ushort _lineNo = lineNo;

        [FieldOffset(14)]
        private readonly ushort _value1 = value1;

        [FieldOffset(16)]
        private readonly ushort _value2 = value2;

        [FieldOffset(18)]
        private readonly ushort _value3 = value3;

        [FieldOffset(20)]
        private readonly DebugInfoCommandId _command = command;

        [FieldOffset(22)]
        private readonly ushort _processId = processId;

        /// <summary>
        ///  Gets the timestamp.
        /// </summary>
        public readonly DateTime Timestamp => _timeStamp;

        /// <summary>
        ///  Gets the process ID.
        /// </summary>
        public readonly ushort ProcessId => _processId;

        /// <summary>
        ///  Gets the thread ID.
        /// </summary>
        public readonly int ThreadId => _threadId;

        /// <summary>
        ///  Gets the Lino No.
        /// </summary>
        public readonly int LineNo => _lineNo;

        /// <summary>
        ///  Gets Value1.
        /// </summary>
        public readonly int Value1 => _value1;

        /// <summary>
        ///  Gets Value2.
        /// </summary>
        public readonly int Value2 => _value2;

        /// <summary>
        ///  Gets Value3.
        /// </summary>
        public readonly int Value3 => _value3;

        /// <summary>
        ///  Gets the debug info command.
        /// </summary>
        public readonly DebugInfoCommandId Command => _command;

        /// <summary>
        ///  Converts a byte array to an instance of ExtendedDebugInfo.
        /// </summary>
        /// <param name="data">The byte array.</param>
        /// <returns>The instance of ExtendedDebugInfo.</returns>
        public unsafe static ExtendedDebugInfo FromByteSpan(Span<byte> data)
         => data.Length != sizeof(ExtendedDebugInfo)
                ? throw new ArgumentException("Data must be the same size as the ExtendedDebugInfo struct.")
                : Unsafe.As<byte, ExtendedDebugInfo>(ref data[0]);

        /// <summary>
        ///  Converts a base64 string to an instance of ExtendedDebugInfo and a message.
        /// </summary>
        /// <param name="data">The base64 string.</param>
        /// <returns>The instance of ExtendedDebugInfo and the message.</returns>
        public unsafe static ExtendedDebugInfo FromBase64LeadMessage(string data)
        {
            // First 34 bytes are the ExtendedDebugInfo struct, then the message.
            string dataString = data[2..34];

            Span<byte> resultSpan = new byte[sizeof(ExtendedDebugInfo)];

            // Set the first two bytes to 0x42
            resultSpan[0] = 0x42;
            resultSpan[1] = 0x42;

            // Decode the Base64 string directly into the result span, starting from index 2:
            if (Convert.TryFromBase64String(dataString, resultSpan[2..], out _))
            {
                ExtendedDebugInfo debugInfo = FromByteSpan(resultSpan);

                return debugInfo;
            }

            throw new ArgumentException("Data must be the same size as the ExtendedDebugInfo struct.");
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
        ///  Converts the instance of ExtendedDebugInfo to a base64 string, but doesn't code the 4242-lead-in.
        /// </summary>
        /// <returns>The base64 string.</returns>
        public readonly unsafe string ToBase64()
        {
            byte[] data = ToByteArray();

            return s_leadIn + Convert.ToBase64String(
                inArray: data,
                offset: 2,
                length: sizeof(ExtendedDebugInfo) - 2);
        }
    }
}
