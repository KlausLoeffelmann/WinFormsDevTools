using System.IO;
using System.Windows.Forms;

namespace IconLibrary.Controls;

public class FileSystemNode : TreeNode
{
    /// <summary>
    /// Gets or sets the directory associated with this node
    /// </summary>
    public DirectoryInfo? Folder { get; set; }
    
    /// <summary>
    /// Gets or sets the display name for this node
    /// </summary>
    public string DisplayName 
    {
        get => Text;
        set => Text = value;
    }
    
    /// <summary>
    /// Creates a new FileSystemNode with the specified display name and folder
    /// </summary>
    /// <param name="displayName">The name to display for the node</param>
    /// <param name="folder">The directory associated with the node, or null</param>
    public FileSystemNode(string displayName, DirectoryInfo? folder)
    {
        Text = displayName;
        Folder = folder;
    }
}