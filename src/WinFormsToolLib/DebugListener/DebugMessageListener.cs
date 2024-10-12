using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Security.AccessControl;
using System.Security.Principal;
using static DevTools.Libs.DebugListener.WinFormsPerformanceLogging;

namespace DevTools.Libs.DebugListener;

/// <summary>
///  Listens for debug messages from a shared memory buffer.
/// </summary>
public class DebugMessageListener : Component
{
    private EventWaitHandle bufferReadyEvent = null!;
    private EventWaitHandle dataReadyEvent = null!;
    private MemoryMappedFile sharedMemory = null!;
    private MemoryMappedViewAccessor accessor = null!;

    /// <summary>
    ///  Delegate for handling debug messages received asynchronously.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="DebugMessageEventArgs"/> that contains the event data.</param>
    public delegate Task DebugMessageReceivedEventAsyncHandler(object sender, DebugMessageEventArgs e);

    /// <summary>
    ///  Occurs when a debug message is received.
    /// </summary>
    public event DebugMessageReceivedEventAsyncHandler? DebugMessageReceivedAsync;

    /// <summary>
    ///  Starts listening for debug messages asynchronously.
    /// </summary>
    public async Task StartListeningAsync()
    {
        // Define the names in the global namespace
        string bufferReadyEventName = "DBWIN_BUFFER_READY";
        string dataReadyEventName = "DBWIN_DATA_READY";
        string sharedMemoryName = "DBWIN_BUFFER";

        // Set security attributes to allow access by all users
        EventWaitHandleSecurity eventSecurity = new();
        EventWaitHandleAccessRule eventRule = new(
            new SecurityIdentifier(WellKnownSidType.WorldSid, null),
            EventWaitHandleRights.FullControl,
            AccessControlType.Allow);
        eventSecurity.AddAccessRule(eventRule);

        // Create or open the buffer ready event
        bufferReadyEvent = new EventWaitHandle(false, EventResetMode.AutoReset,
            bufferReadyEventName, out bool _);

        // Create or open the data ready event
        dataReadyEvent = new EventWaitHandle(false, EventResetMode.AutoReset,
            dataReadyEventName, out bool _);

        // Create or open the shared memory buffer
        sharedMemory = MemoryMappedFile.CreateOrOpen(sharedMemoryName,
            4096, MemoryMappedFileAccess.ReadWrite,
            MemoryMappedFileOptions.None,
            HandleInheritability.None);

        // Create a view accessor for the shared memory
        accessor = sharedMemory.CreateViewAccessor(0, 4096, MemoryMappedFileAccess.Read);

        // Signal that the listener is ready
        bufferReadyEvent.Set();

        // Start the listening loop
        await Task.Run(async () => await ListenForMessagesAsync());
    }

    /// <summary>
    ///  Listens for debug messages asynchronously.
    /// </summary>
    private async Task ListenForMessagesAsync()
    {
        DateTime timeStamp;
        byte[] buffer = new byte[20];

        while (true)
        {
            // Wait for a debug message to be written
            dataReadyEvent.WaitOne();
            timeStamp = DateTime.Now;

            // Read the process ID and message
            int processId = accessor.ReadInt32(0);
            int extendedDebugInfo = accessor.ReadUInt16(4);
            if (extendedDebugInfo == 0x4242)
            {
                // Read the extended debug info
                accessor.ReadArray(0, buffer, 0, 20);
                string message = ReadNullTerminatedString(accessor, 20);
                ExtendedDebugInfo debugInfo = ExtendedDebugInfo.FromByteArray(buffer);
                await OnDebugMessageReceivedAsync(new DebugMessageEventArgs(timeStamp, processId, debugInfo, message));
            }
            else
            {
                // Read the message
                string message = ReadNullTerminatedString(accessor, 4);
                await OnDebugMessageReceivedAsync(new DebugMessageEventArgs(timeStamp, processId, null, message));
            }

            // Signal that the buffer is ready for the next message
            bufferReadyEvent.Set();
        }
    }

    /// <summary>
    ///  Raises the <see cref="DebugMessageReceivedAsync"/> event.
    /// </summary>
    /// <param name="e">A <see cref="DebugMessageEventArgs"/> that contains the event data.</param>
    protected virtual async Task OnDebugMessageReceivedAsync(DebugMessageEventArgs e)
    {
        if (DebugMessageReceivedAsync != null)
        {
            await DebugMessageReceivedAsync(this, e);
        }
    }

    /// <summary>
    ///  Reads a null-terminated string from the shared memory accessor.
    /// </summary>
    /// <param name="accessor">The memory-mapped view accessor.</param>
    /// <param name="offset">The offset at which to start reading.</param>
    /// <returns>The read string.</returns>
    private static string ReadNullTerminatedString(MemoryMappedViewAccessor accessor, long offset)
    {
        const int maxMessageLength = 4092; // 4096 bytes total - 4 bytes for process ID
        byte[] buffer = new byte[maxMessageLength];
        accessor.ReadArray(offset, buffer, 0, maxMessageLength);

        int nullIndex = Array.IndexOf<byte>(buffer, 0);
        if (nullIndex < 0) nullIndex = maxMessageLength;

        return System.Text.Encoding.Default.GetString(buffer, 0, nullIndex);
    }

    /// <summary>
    ///  Stops listening for debug messages and disposes resources.
    /// </summary>
    public void StopListening()
    {
        // Dispose resources
        accessor?.Dispose();
        sharedMemory?.Dispose();
        bufferReadyEvent?.Dispose();
        dataReadyEvent?.Dispose();
    }
}
