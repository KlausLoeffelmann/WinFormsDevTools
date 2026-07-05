using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DevTools.RuntimeDeploy.Infrastructure;

/// <summary>
///  Describes a single process that currently holds a lock on a file.
/// </summary>
/// <param name="ProcessId">The operating-system process id.</param>
/// <param name="ProcessName">The short process name (e.g. <c>devenv</c>).</param>
/// <param name="StartTime">When the process started, if it could be queried.</param>
/// <param name="ApplicationName">
///  The friendly application name reported by the Restart Manager, if any.
/// </param>
public sealed record FileLockProcessInfo(
    int ProcessId,
    string ProcessName,
    DateTime? StartTime,
    string? ApplicationName)
{
    public override string ToString()
    {
        string name = string.IsNullOrWhiteSpace(ApplicationName)
            ? ProcessName
            : ApplicationName!;

        string started = StartTime is null
            ? "start time unknown"
            : $"started {StartTime.Value:yyyy-MM-dd HH:mm:ss}";

        return $"{name} (PID {ProcessId}, {started})";
    }
}

/// <summary>
///  Determines which processes currently hold a lock on a given file by querying
///  the Windows Restart Manager. This type is intentionally self-contained so it
///  can later be lifted into a reusable library without dragging in dependencies.
/// </summary>
public static class FileLockInspector
{
    private const int RmRebootReasonNone = 0;
    private const int CCH_RM_MAX_APP_NAME = 255;
    private const int CCH_RM_MAX_SVC_NAME = 63;
    private const int ERROR_MORE_DATA = 234;

    /// <summary>
    ///  Returns the processes that currently hold a handle to <paramref name="path"/>.
    ///  Returns an empty list when the file is not locked or the holders cannot be
    ///  determined (the method never throws for a normal lock-inspection failure).
    /// </summary>
    public static IReadOnlyList<FileLockProcessInfo> GetLockingProcesses(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return [];
        }

        string sessionKey = Guid.NewGuid().ToString();

        int result = RmStartSession(out uint sessionHandle, 0, sessionKey);
        if (result != 0)
        {
            return [];
        }

        try
        {
            string[] resources = [path];

            result = RmRegisterResources(
                sessionHandle,
                nFiles: 1,
                rgsFilenames: resources,
                nApplications: 0,
                rgApplications: null,
                nServices: 0,
                rgsServiceNames: null);

            if (result != 0)
            {
                return [];
            }

            uint pnProcInfoNeeded = 0;
            uint pnProcInfo = 0;
            uint rebootReasons = RmRebootReasonNone;

            result = RmGetList(
                sessionHandle,
                out pnProcInfoNeeded,
                ref pnProcInfo,
                null,
                ref rebootReasons);

            if (result != ERROR_MORE_DATA || pnProcInfoNeeded == 0)
            {
                return [];
            }

            RM_PROCESS_INFO[] processInfo = new RM_PROCESS_INFO[pnProcInfoNeeded];
            pnProcInfo = pnProcInfoNeeded;

            result = RmGetList(
                sessionHandle,
                out pnProcInfoNeeded,
                ref pnProcInfo,
                processInfo,
                ref rebootReasons);

            if (result != 0)
            {
                return [];
            }

            List<FileLockProcessInfo> lockers = new((int)pnProcInfo);

            for (int i = 0; i < pnProcInfo; i++)
            {
                int processId = processInfo[i].Process.dwProcessId;
                string applicationName = processInfo[i].strAppName;
                string processName = applicationName;
                DateTime? startTime = null;

                try
                {
                    using Process process = Process.GetProcessById(processId);
                    processName = process.ProcessName;
                    startTime = process.StartTime;
                }
                catch
                {
                    // The process may already have exited or be inaccessible; fall
                    // back to the Restart Manager supplied application name.
                }

                lockers.Add(new FileLockProcessInfo(
                    processId,
                    processName,
                    startTime,
                    applicationName));
            }

            return lockers;
        }
        catch
        {
            // Lock inspection is best-effort diagnostics; never let it surface as a
            // failure of the surrounding copy operation.
            return [];
        }
        finally
        {
            RmEndSession(sessionHandle);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RM_UNIQUE_PROCESS
    {
        public int dwProcessId;
        public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct RM_PROCESS_INFO
    {
        public RM_UNIQUE_PROCESS Process;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_APP_NAME + 1)]
        public string strAppName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_SVC_NAME + 1)]
        public string strServiceShortName;

        public uint ApplicationType;
        public uint AppStatus;
        public uint TSSessionId;

        [MarshalAs(UnmanagedType.Bool)]
        public bool bRestartable;
    }

    [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
    private static extern int RmStartSession(
        out uint pSessionHandle,
        int dwSessionFlags,
        string strSessionKey);

    [DllImport("rstrtmgr.dll")]
    private static extern int RmEndSession(uint pSessionHandle);

    [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
    private static extern int RmRegisterResources(
        uint pSessionHandle,
        uint nFiles,
        string[] rgsFilenames,
        uint nApplications,
        [In] RM_UNIQUE_PROCESS[]? rgApplications,
        uint nServices,
        string[]? rgsServiceNames);

    [DllImport("rstrtmgr.dll")]
    private static extern int RmGetList(
        uint dwSessionHandle,
        out uint pnProcInfoNeeded,
        ref uint pnProcInfo,
        [In, Out] RM_PROCESS_INFO[]? rgAffectedApps,
        ref uint lpdwRebootReasons);
}
