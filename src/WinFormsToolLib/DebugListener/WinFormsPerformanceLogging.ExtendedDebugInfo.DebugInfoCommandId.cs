// -------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// -------------------------------------------------------------------

namespace DevTools.Libs.DebugListener;

public partial class WinFormsPerformanceLogging
{
    public readonly partial struct ExtendedDebugInfo
    {
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
}
