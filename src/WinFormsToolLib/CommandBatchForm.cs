﻿namespace WinFormsToolLib
{
    public partial class CommandBatchForm : Form
    {
        public CommandBatchForm(string? windowTitel)
        {
            InitializeComponent();

            if (windowTitel == null)
            {
                windowTitel = $"Command Batch";
            }

            Text = windowTitel + $" - started {DateTime.Now: ddd, yy/MM/dd hh:mm:ss}";
        }

        public void Print(string? message)
            => _printableTextBox.Print(message);

        public void PrintLine(string? message)
            => _printableTextBox.PrintLine(message);

        public void StartBatch()
        {
            _okButton.Enabled = false;
            Show();
        }

        public void EndBatch()
        {
            _okButton.Enabled = true;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
