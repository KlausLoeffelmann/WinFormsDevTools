namespace DevTools.ShadowCacheSpy;

public class AwaitableCancellationTokenSource
{
    private readonly CancellationTokenSource _cts;
    private readonly TaskCompletionSource _acknowledgmentTcs;

    public AwaitableCancellationTokenSource()
    {
        _cts = new CancellationTokenSource();
        _acknowledgmentTcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    /// <summary>
    ///  Gets the cancellation token that can be used for cancellation requests.
    /// </summary>
    public CancellationToken Token => _cts.Token;

    /// <summary>
    ///  Cancels the token and awaits until the cancellation is acknowledged.
    /// </summary>
    public async Task CancelAndWaitForAcknowledgmentAsync()
    {
        _cts.Cancel();
        await _acknowledgmentTcs.Task;
    }

    /// <summary>
    ///  Acknowledges that the cancellation request has been handled.
    /// </summary>
    public void AcknowledgeCancellation()
    {
        if (_acknowledgmentTcs.Task.IsCompleted)
            throw new InvalidOperationException("Cancellation has already been acknowledged.");

        _acknowledgmentTcs.TrySetResult();
    }

    /// <summary>
    ///  Disposes the internal CancellationTokenSource.
    /// </summary>
    public void Dispose() => _cts.Dispose();
}
