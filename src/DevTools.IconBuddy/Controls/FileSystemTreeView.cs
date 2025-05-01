using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace IconLibrary.Controls;

public class FileSystemTreeView : TreeView
{
    #region Events
    public event EventHandler<FileSystemNodeEventArgs>? FileSystemNodeClick;
    public event EventHandler<FileSystemNodeEventArgs>? FileSystemNodeDoubleClick;
    public event EventHandler<FileSystemNodeEventArgs>? FileSystemNodeBeforeSelect;
    public event EventHandler<FileSystemNodeEventArgs>? SelectedFileSystemNodeChanged;
    public event EventHandler<FileSystemNodeEventArgs>? FileSystemNodeAfterSelect;
    public event EventHandler<FileSystemNodeEventArgs>? FileSystemNodeExpanded;
    public event EventHandler<FileSystemNodeEventArgs>? FileSystemNodeCollapsed;
    #endregion

    private readonly Dictionary<string, bool> _expandedNodes = new();

    public FileSystemTreeView()
    {
        // Hook up the TreeView events to our custom events
        this.BeforeExpand += FileSystemTreeView_BeforeExpand;
        this.AfterExpand += FileSystemTreeView_AfterExpand;
        this.AfterCollapse += FileSystemTreeView_AfterCollapse;
        this.BeforeSelect += FileSystemTreeView_BeforeSelect;
        this.AfterSelect += FileSystemTreeView_AfterSelect;
        this.NodeMouseClick += FileSystemTreeView_NodeMouseClick;
        this.NodeMouseDoubleClick += FileSystemTreeView_NodeMouseDoubleClick;

        InitializeTreeView();
    }

    #region Tree Initialization
    private void InitializeTreeView()
    {
        // Clear existing nodes
        Nodes.Clear();

        // Add "This PC" with drives
        AddThisPCNode();

        // Add OneDrive accounts
        AddOneDriveNodes();

        // Add special folders
        AddSpecialFolders();
    }

    private void AddThisPCNode()
    {
        var thisPcNode = new FileSystemNode("This PC", null)
        {
            ImageIndex = 0,
            SelectedImageIndex = 0
        };
        Nodes.Add(thisPcNode);

        // Add drives under "This PC"
        foreach (var drive in DriveInfo.GetDrives())
        {
            try
            {
                if (drive.IsReady)
                {
                    var driveFolder = new DirectoryInfo(drive.Name);
                    var driveNode = new FileSystemNode(drive.VolumeLabel ?? $"{drive.Name} ({drive.DriveType})", driveFolder)
                    {
                        ImageIndex = 1, 
                        SelectedImageIndex = 1
                    };
                    thisPcNode.Nodes.Add(driveNode);

                    // Add a dummy node so the expand (+) button shows up
                    driveNode.Nodes.Add(new FileSystemNode("Loading...", null));
                }
            }
            catch (Exception)
            {
                // Skip drives that cause errors
            }
        }
    }

    private void AddOneDriveNodes()
    {
        try
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var oneDriveRoot = Path.Combine(userFolder, "OneDrive");
            
            if (Directory.Exists(oneDriveRoot))
            {
                var oneDriveFolder = new DirectoryInfo(oneDriveRoot);
                var oneDriveNode = new FileSystemNode("OneDrive", oneDriveFolder)
                {
                    ImageIndex = 2,
                    SelectedImageIndex = 2
                };
                Nodes.Add(oneDriveNode);
                
                // Add a dummy node so the expand (+) button shows up
                oneDriveNode.Nodes.Add(new FileSystemNode("Loading...", null));
            }

            // Check for additional OneDrive accounts (Business, School, etc.)
            var oneDriveCommercial = Path.Combine(userFolder, "OneDrive - ");
            var potentialOneDriveFolders = Directory.GetDirectories(userFolder)
                .Where(dir => Path.GetFileName(dir).StartsWith("OneDrive - "));

            foreach (var folder in potentialOneDriveFolders)
            {
                var dirInfo = new DirectoryInfo(folder);
                var displayName = dirInfo.Name;
                var oneDriveNode = new FileSystemNode(displayName, dirInfo)
                {
                    ImageIndex = 2,
                    SelectedImageIndex = 2
                };
                Nodes.Add(oneDriveNode);
                
                // Add a dummy node so the expand (+) button shows up
                oneDriveNode.Nodes.Add(new FileSystemNode("Loading...", null));
            }
        }
        catch (Exception)
        {
            // Silently fail if OneDrive paths can't be accessed
        }
    }

    private void AddSpecialFolders()
    {
        AddSpecialFolder(Environment.SpecialFolder.MyPictures, "Pictures");
        AddSpecialFolder(Environment.SpecialFolder.MyDocuments, "Documents");
        AddSpecialFolder(Environment.SpecialFolder.Personal, "Personal");
        
        // Try to add Downloads folder
        AddSpecialFolder(Environment.SpecialFolder.UserProfile, "Downloads", "Downloads");
        
        // Try to add Camera Roll folder (typically in Pictures\Camera Roll)
        string picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        string cameraRollPath = Path.Combine(picturesFolder, "Camera Roll");
        
        if (Directory.Exists(cameraRollPath))
        {
            var cameraRollDir = new DirectoryInfo(cameraRollPath);
            var cameraRollNode = new FileSystemNode("Camera Roll", cameraRollDir)
            {
                ImageIndex = 3,
                SelectedImageIndex = 3
            };
            Nodes.Add(cameraRollNode);
            
            // Add a dummy node so the expand (+) button shows up
            cameraRollNode.Nodes.Add(new FileSystemNode("Loading...", null));
        }
    }

    private void AddSpecialFolder(Environment.SpecialFolder specialFolder, string displayName, string? subdirectory = null)
    {
        try
        {
            string folderPath = Environment.GetFolderPath(specialFolder);
            
            if (subdirectory != null)
            {
                folderPath = Path.Combine(folderPath, subdirectory);
            }
            
            if (Directory.Exists(folderPath))
            {
                var dirInfo = new DirectoryInfo(folderPath);
                var folderNode = new FileSystemNode(displayName, dirInfo)
                {
                    ImageIndex = 3,
                    SelectedImageIndex = 3
                };
                Nodes.Add(folderNode);
                
                // Add a dummy node so the expand (+) button shows up
                folderNode.Nodes.Add(new FileSystemNode("Loading...", null));
            }
        }
        catch (Exception)
        {
            // Skip folders that cause errors
        }
    }
    #endregion

    #region Node Population
    private void PopulateDirectoryNode(FileSystemNode node)
    {
        if (node?.Folder == null)
            return;

        node.Nodes.Clear();
        
        try
        {
            var directories = node.Folder.GetDirectories();
            foreach (var directory in directories)
            {
                try
                {
                    if ((directory.Attributes & FileAttributes.Hidden) == 0)
                    {
                        var childNode = new FileSystemNode(directory.Name, directory)
                        {
                            ImageIndex = 3,
                            SelectedImageIndex = 3
                        };
                        node.Nodes.Add(childNode);
                        
                        // Check if this directory has subdirectories
                        bool hasSubdirectories = directory.GetDirectories().Any(dir => 
                            (dir.Attributes & FileAttributes.Hidden) == 0);
                        
                        if (hasSubdirectories)
                        {
                            // Add a dummy node so the expand (+) button shows up
                            childNode.Nodes.Add(new FileSystemNode("Loading...", null));
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Skip directories we don't have access to
                }
            }
        }
        catch (Exception)
        {
            // Handle any errors in accessing the directory
            node.Nodes.Add(new FileSystemNode("Error accessing folder", null));
        }
    }
    #endregion

    #region Event Handlers
    private void FileSystemTreeView_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            // Remove the dummy node and populate with actual subdirectories
            if (fsNode.Nodes.Count == 1 && fsNode.Nodes[0].Text == "Loading...")
            {
                PopulateDirectoryNode(fsNode);
            }

            // Store expanded state
            _expandedNodes[fsNode.FullPath] = true;
        }
    }

    private void FileSystemTreeView_AfterExpand(object? sender, TreeViewEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            FileSystemNodeExpanded?.Invoke(this, new FileSystemNodeEventArgs(fsNode, "Expanded"));
        }
    }

    private void FileSystemTreeView_AfterCollapse(object? sender, TreeViewEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            // Update expanded state
            if (_expandedNodes.ContainsKey(fsNode.FullPath))
            {
                _expandedNodes[fsNode.FullPath] = false;
            }

            FileSystemNodeCollapsed?.Invoke(this, new FileSystemNodeEventArgs(fsNode, "Collapsed"));
        }
    }

    private void FileSystemTreeView_BeforeSelect(object? sender, TreeViewCancelEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            var args = new FileSystemNodeEventArgs(fsNode, "BeforeSelect");
            FileSystemNodeBeforeSelect?.Invoke(this, args);
            e.Cancel = args.Cancel;
        }
    }

    private void FileSystemTreeView_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            SelectedFileSystemNodeChanged?.Invoke(this, new FileSystemNodeEventArgs(fsNode, "SelectedChanged"));
            FileSystemNodeAfterSelect?.Invoke(this, new FileSystemNodeEventArgs(fsNode, "AfterSelect"));
        }
    }

    private void FileSystemTreeView_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            FileSystemNodeClick?.Invoke(this, new FileSystemNodeEventArgs(fsNode, "Click"));
        }
    }

    private void FileSystemTreeView_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node is FileSystemNode fsNode)
        {
            FileSystemNodeDoubleClick?.Invoke(this, new FileSystemNodeEventArgs(fsNode, "DoubleClick"));
        }
    }
    #endregion

    #region Configuration
    /// <summary>
    /// Gets the configuration of expanded nodes as a JSON string
    /// </summary>
    public string GetConfiguration()
    {
        var expandedPaths = _expandedNodes
            .Where(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
            
        return JsonSerializer.Serialize(expandedPaths);
    }

    /// <summary>
    /// Sets the configuration of expanded nodes from a JSON string
    /// </summary>
    public void SetConfiguration(string configuration)
    {
        try
        {
            var expandedPaths = JsonSerializer.Deserialize<List<string>>(configuration);
            
            if (expandedPaths != null)
            {
                _expandedNodes.Clear();
                foreach (var path in expandedPaths)
                {
                    _expandedNodes[path] = true;
                }
                
                // Expand the nodes based on the configuration
                ExpandSavedNodes();
            }
        }
        catch (JsonException)
        {
            // Handle invalid JSON
        }
    }

    private void ExpandSavedNodes()
    {
        foreach (var path in _expandedNodes.Keys.Where(k => _expandedNodes[k]))
        {
            // Find and expand the node with this path
            var parts = path.Split('\\');
            TreeNode currentNode = null;
            
            // Find the root node
            foreach (TreeNode rootNode in Nodes)
            {
                if (rootNode.Text == parts[0])
                {
                    currentNode = rootNode;
                    break;
                }
            }
            
            // Traverse the path
            for (int i = 1; currentNode != null && i < parts.Length; i++)
            {
                // Expand the current node to populate its children
                if (!currentNode.IsExpanded)
                {
                    currentNode.Expand();
                }
                
                // Find the next node in the path
                TreeNode nextNode = null;
                foreach (TreeNode childNode in currentNode.Nodes)
                {
                    if (childNode.Text == parts[i])
                    {
                        nextNode = childNode;
                        break;
                    }
                }
                
                currentNode = nextNode;
                if (currentNode == null) break;
            }
            
            // Expand the final node
            currentNode?.Expand();
        }
    }
    #endregion
}
