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
            ImportantMessage = 1,
            HighlyImportantMessage = 2,
            CriticalMessage = 3,
            ExceptionMessage = 4,
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
