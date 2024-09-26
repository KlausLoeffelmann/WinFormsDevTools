using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowCacheSpy
{
    public partial class FrmOptions : Form
    {
        private AppSettings? _appSettings;

        public FrmOptions()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _appSettings = DataContext as AppSettings;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DataContext is not null && _appSettings is null)
            {
                // DataContext did not carry date we were supposed to use.
                return;
            }

            if (DialogResult == DialogResult.OK)
            {
                _appSettings ??= new();
                _appSettings.WatchFolder = _watchFolderPicker.FilePath;
                DataContext = _appSettings;
            }
        }
    }
}
