using System.Text;

namespace DebugListener;

public partial class FrmMain : Form
{
    public FrmMain()
    {
        InitializeComponent();
    }

    protected async override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await _logView.StartListeningAsync();
    }
}
