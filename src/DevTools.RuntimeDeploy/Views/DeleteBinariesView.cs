using System.Collections.Concurrent;

namespace DevTools.RuntimeDeploy.Views;

public partial class DeleteBinariesView : UserControl
{
    // Local dictionary to store source folders
    private readonly Dictionary<string, string> _sourceFolders = new();

    public DeleteBinariesView()
    {
        InitializeComponent();
    }

    private async void AddFolders_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select one or more source folders",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = false,
            Multiselect = true
        };

        // Fallback for Multiselect if not supported
        var selectedFolders = new List<string>();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            selectedFolders.Add(dialog.SelectedPath);
        }

        // Add new folders to the dictionary and ListView
        foreach (var folder in selectedFolders)
        {
            if (!_sourceFolders.ContainsKey(folder))
            {
                _sourceFolders[folder] = folder;
                _lvwSourceFolders.Items.Add(folder);
            }
        }

        // Asynchronously crawl for bin/obj folders and update _lvwPurgeTargets
        await Task.Run(() =>
        {
            var foundTargets = new ConcurrentBag<string>();
            foreach (var folder in selectedFolders)
            {
                FindBinObjFolders(folder, foundTargets);
            }

            // Update UI on the main thread
            if (!foundTargets.IsEmpty)
            {
                BeginInvoke(new Action(() =>
                {
                    foreach (var target in foundTargets)
                    {
                        if (_lvwPurgeTargets.Items.Cast<ListViewItem>().All(i => i.Text != target))
                        {
                            _lvwPurgeTargets.Items.Add(target);
                        }
                    }
                }));
            }
        });
    }

    private static void FindBinObjFolders(string root, ConcurrentBag<string> found)
    {
        try
        {
            foreach (var dir in Directory.EnumerateDirectories(root))
            {
                var dirName = Path.GetFileName(dir);
                if (dirName is "bin" or "obj")
                {
                    found.Add(dir);
                }

                // Recurse
                FindBinObjFolders(dir, found);
            }
        }
        catch
        {
            // Ignore access exceptions
        }
    }
}
