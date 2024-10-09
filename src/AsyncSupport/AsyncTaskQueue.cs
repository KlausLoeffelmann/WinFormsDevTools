using System.Collections.Concurrent;

namespace CommunityToolkit.WinForms.AsyncSupport;

/// <summary>
///  Represents a queue for managing asynchronous tasks.
/// </summary>
public class AsyncTaskQueue : IDisposable
{
    private readonly ConcurrentQueue<Func<Task>> _taskQueue = new();
    private readonly SemaphoreSlim _signal = new(0);
    private readonly CancellationTokenSource _cts = new();
    private readonly Task _processingTask;

    /// <summary>
    ///  Initializes a new instance of the <see cref="AsyncTaskQueue"/> class.
    /// </summary>
    public AsyncTaskQueue()
    {
        _processingTask = Task.Run(ProcessQueueAsync);
    }

    /// <summary>
    ///  Processes the queue of tasks asynchronously.
    /// </summary>
    private async Task ProcessQueueAsync()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            await _signal.WaitAsync(_cts.Token);

            if (_cts.Token.IsCancellationRequested)
                break;

            while (_taskQueue.TryDequeue(out var workItem))
            {
                try
                {
                    await workItem();
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed
                    Console.WriteLine($"Task error: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    ///  Enqueues an asynchronous method to be executed.
    /// </summary>
    /// <param name="asyncMethod">The asynchronous method to enqueue.</param>
    public void Enqueue(Func<Task> asyncMethod)
    {
        _taskQueue.Enqueue(asyncMethod);
        _signal.Release();
    }

    /// <summary>
    ///  Enqueues an asynchronous method to be executed and returns a completed task.
    /// </summary>
    /// <param name="asyncMethod">The asynchronous method to enqueue.</param>
    /// <returns>A completed task.</returns>
    public Task EnqueueAsync(Func<Task> asyncMethod)
    {
        _taskQueue.Enqueue(asyncMethod);
        _signal.Release();

        return Task.CompletedTask;
    }

    /// <summary>
    ///  Releases all resources used by the <see cref="AsyncTaskQueue"/>.
    /// </summary>
    public void Dispose()
    {
        _cts.Cancel();
        _signal.Release();
        _processingTask.Wait();
        _cts.Dispose();
        _signal.Dispose();
    }
}
