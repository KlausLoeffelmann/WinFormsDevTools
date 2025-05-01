using System;
using System.ComponentModel;

namespace IconLibrary.Controls;

public class FileSystemNodeEventArgs : CancelEventArgs
{
    /// <summary>
    /// Gets the FileSystemNode associated with the event
    /// </summary>
    public FileSystemNode Node { get; }
    
    /// <summary>
    /// Gets the type of action that triggered this event
    /// </summary>
    public string Action { get; }
    
    /// <summary>
    /// Creates a new instance of FileSystemNodeEventArgs
    /// </summary>
    /// <param name="node">The FileSystemNode associated with the event</param>
    /// <param name="action">The type of action that triggered this event</param>
    public FileSystemNodeEventArgs(FileSystemNode node, string action)
    {
        Node = node;
        Action = action;
    }
}