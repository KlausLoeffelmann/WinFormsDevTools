namespace DevTools.ShadowCacheSpy;

/// <summary>
///  A fixed-size circular queue that maintains the average of its values efficiently.
/// </summary>
/// <remarks>
///  Initializes a new instance of the <see cref="FastQueue"/> struct with a specified capacity.
/// </remarks>
/// <param name="capacity">The maximum number of elements the queue can hold.</param>
public struct FastQueue(int capacity)
{
    private readonly double[] _values = new double[capacity];
    private int _head = 0;
    private int _tail = 0;
    private int _count = 0;
    private double _sum = 0;

    public FastQueue() : this(10) { }

    /// <summary>
    ///  Gets the current average of the values in the queue.
    /// </summary>
    public readonly double Average => _count == 0 ? 0 : _sum / _count;

    /// <summary>
    ///  Adds a new value to the queue and updates the average.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public void Add(double value)
    {
        if (_count == capacity)
        {
            // Queue is full, so remove the oldest value (at _head) before adding the new one
            _sum -= _values[_head];
            _head = (_head + 1) % capacity; // Move the head forward
        }
        else
        {
            _count++; // Increment count if the queue isn't full yet
        }

        // Add the new value to the tail
        _values[_tail] = value;
        _sum += value;
        _tail = (_tail + 1) % capacity; // Move the tail forward
    }
}
