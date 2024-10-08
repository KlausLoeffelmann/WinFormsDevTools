using System.Diagnostics;

namespace WfRuntimeDeploy
{
    public partial class PathShortCutControl : UserControl
    {
        public PathShortCutControl()
        {
            InitializeComponent();
        }

        private void _revealInExplorerButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_revealInExplorerButton.Text))
            {
                FileInfo file = new(_revealInExplorerButton.Text);
                if (file.Exists)
                {
                    Process.Start("explorer.exe", $"/select,{file.FullName}");
                }
            }
        }
    }
}
