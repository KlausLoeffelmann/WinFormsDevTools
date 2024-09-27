namespace ShadowCacheSpy;

/// <summary>
///  A fixed-size circular queue that maintains the average of its values efficiently.
/// </summary>
/// <remarks>
///  Initializes a new instance of the <see cref="FastQueue"/> struct with a specified capacity.
/// </remarks>
/// <param name="capacity">The maximum number of elements the queue can hold.</param>
public struct FastQueue
{
    private readonly int _capacity;
    private readonly double[] _values;
    private int _head;
    private int _tail;
    private int _count;
    private double _sum;

    public FastQueue() : this(10) { }

    public FastQueue(int capacity)
    {
        _capacity = capacity;
        _values = new double[capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
        _sum = 0;
    }

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
        if (_count == _capacity)
        {
            // Queue is full, so remove the oldest value (at _head) before adding the new one
            _sum -= _values[_head];
            _head = (_head + 1) % _capacity; // Move the head forward
        }
        else
        {
            _count++; // Increment count if the queue isn't full yet
        }

        // Add the new value to the tail
        _values[_tail] = value;
        _sum += value;
        _tail = (_tail + 1) % _capacity; // Move the tail forward
    }
}
