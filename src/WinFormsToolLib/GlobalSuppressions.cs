// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1840:Use 'Environment.CurrentManagedThreadId'", Justification = "Be compatible with WinForms Designer code base.", Scope = "member", Target = "~M:DevTools.Libs.DebugListener.WinFormsPerformanceLogging.PerfDbgTrace(System.String,System.String,System.String,System.String,System.Int32)")]
[assembly: SuppressMessage("Performance", "CA1837:Use 'Environment.ProcessId'", Justification = "Be compatible with WinForms Designer code base.", Scope = "member", Target = "~M:DevTools.Libs.DebugListener.WinFormsPerformanceLogging.DPrintAsync(System.String,System.String,System.String,System.String,System.Int32)~System.Threading.Tasks.Task")]
