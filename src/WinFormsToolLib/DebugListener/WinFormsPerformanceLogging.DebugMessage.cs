// -------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// -------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace DevTools.Libs.DebugListener;

public partial class WinFormsPerformanceLogging
{
    public partial class DebugMessage
    {
        public DebugMessage(DateTime timeStamp, int processId, string? combineString = default)
        {
            if (combineString?.IndexOf("[[") > 0)
            {
                DisassembleCombineString(combineString);

                if (DebugInfo is not null)
                {
                    ProcessId = DebugInfo.Value.ProcessId;
                    Timestamp = DebugInfo.Value.Timestamp;

                    return;
                }
            }

            ProcessId = processId;
            Timestamp = timeStamp;
            Message = combineString;
        }

        private void DisassembleCombineString(string combineString)
        {
            var strings = ParseString(combineString);

            if (strings.Count == 5)
            {
                // The strings inside the [[...]] pairs
                DebugInfo = ExtendedDebugInfo.FromBase64LeadMessage(strings[0]);
                Category = strings[1];
                MethodName = strings[2];
                FilePath = strings[3];
                Message = strings[4];
            }
            else
            {
                Message = combineString;
            }
        }

        public DateTime Timestamp { get; }
        public TimeSpan Duration { get; set; }
        public int ProcessId { get; }
        public ExtendedDebugInfo? DebugInfo { get; private set; }
        public string? FilePath { get; private set; }
        public string? Message { get; private set; }
        public string? MethodName { get; private set; }
        public string? Category { get; private set; }
        public Color? ForeColor { get; set; }
        public Color? BackColor { get; set; }
        public object? Tag { get; set; }

        public static List<string> ParseString(string input)
        {
            ReadOnlySpan<char> span = input.AsSpan();
            List<string> parts = [];
            int length = span.Length;

            // Find the prefix (text before the first "[[")
            int prefixEnd = span.IndexOf("[[");
            int pos;

            if (prefixEnd == -1)
            {
                // No "[[" found; the entire string is the prefix
                parts.Add(input);
                return parts;
            }
            else
            {
                // Add the prefix to the parts list
                parts.Add(new string(span[..prefixEnd]));
                pos = prefixEnd;
            }

            while (pos < length)
            {
                // Check if the current position starts with "[["
                if (span[pos..].StartsWith("[[", StringComparison.Ordinal))
                {
                    pos += 2; // Move past the opening brackets
                    int end = span[pos..].IndexOf("]]", StringComparison.Ordinal);
                    if (end == -1)
                    {
                        // No matching closing brackets found; break the loop
                        break;
                    }

                    // Extract the content between "[[" and "]]"
                    string content = new(span.Slice(pos, end));
                    parts.Add(content);
                    pos += end + 2; // Move past the content and closing brackets
                }
                else
                {
                    pos++; // Move to the next character
                }
            }

            return parts;
        }
    }
}
