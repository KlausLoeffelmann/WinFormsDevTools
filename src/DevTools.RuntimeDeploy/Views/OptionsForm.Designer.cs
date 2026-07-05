namespace DevTools.RuntimeDeploy.Views;

partial class OptionsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        _sourceFolderLabel = new Label();
        _sourceFolderTextBox = new TextBox();
        _browseButton = new Button();
        _targetsLabel = new Label();
        _targetsComboBox = new ComboBox();
        _loadAssembliesButton = new Button();
        _assembliesLabel = new Label();
        _assembliesListView = new ListView();
        _fontsGroupBox = new GroupBox();
        _uiFontCaptionLabel = new Label();
        _uiFontPreviewLabel = new Label();
        _changeUiFontButton = new Button();
        _outputFontCaptionLabel = new Label();
        _outputFontPreviewLabel = new Label();
        _changeOutputFontButton = new Button();
        _okButton = new Button();
        _cancelButton = new Button();
        _fontsGroupBox.SuspendLayout();
        SuspendLayout();
        // 
        // _sourceFolderLabel
        // 
        _sourceFolderLabel.AutoSize = true;
        _sourceFolderLabel.Location = new Point(15, 19);
        _sourceFolderLabel.Name = "_sourceFolderLabel";
        _sourceFolderLabel.Size = new Size(326, 30);
        _sourceFolderLabel.TabIndex = 0;
        _sourceFolderLabel.Text = "WinForms artifacts source folder:";
        // 
        // _sourceFolderTextBox
        // 
        _sourceFolderTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _sourceFolderTextBox.Location = new Point(347, 16);
        _sourceFolderTextBox.Name = "_sourceFolderTextBox";
        _sourceFolderTextBox.Size = new Size(699, 37);
        _sourceFolderTextBox.TabIndex = 1;
        _sourceFolderTextBox.TextChanged += SourceFolderTextBox_TextChanged;
        // 
        // _browseButton
        // 
        _browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _browseButton.Location = new Point(1058, 14);
        _browseButton.Name = "_browseButton";
        _browseButton.Size = new Size(50, 42);
        _browseButton.TabIndex = 2;
        _browseButton.Text = "...";
        _browseButton.UseVisualStyleBackColor = true;
        _browseButton.Click += BrowseButton_Click;
        // 
        // _targetsLabel
        // 
        _targetsLabel.AutoSize = true;
        _targetsLabel.Location = new Point(15, 77);
        _targetsLabel.Name = "_targetsLabel";
        _targetsLabel.Size = new Size(282, 30);
        _targetsLabel.TabIndex = 3;
        _targetsLabel.Text = "Available artifacts binaries:";
        // 
        // _targetsComboBox
        // 
        _targetsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _targetsComboBox.FormattingEnabled = true;
        _targetsComboBox.Location = new Point(347, 74);
        _targetsComboBox.Name = "_targetsComboBox";
        _targetsComboBox.Size = new Size(430, 38);
        _targetsComboBox.TabIndex = 4;
        _targetsComboBox.SelectedIndexChanged += TargetsComboBox_SelectedIndexChanged;
        // 
        // _loadAssembliesButton
        // 
        _loadAssembliesButton.Location = new Point(794, 72);
        _loadAssembliesButton.Name = "_loadAssembliesButton";
        _loadAssembliesButton.Size = new Size(214, 42);
        _loadAssembliesButton.TabIndex = 5;
        _loadAssembliesButton.Text = "Load Assemblies";
        _loadAssembliesButton.UseVisualStyleBackColor = true;
        _loadAssembliesButton.Click += LoadAssembliesButton_Click;
        // 
        // _assembliesLabel
        // 
        _assembliesLabel.AutoSize = true;
        _assembliesLabel.Location = new Point(15, 137);
        _assembliesLabel.Name = "_assembliesLabel";
        _assembliesLabel.Size = new Size(305, 30);
        _assembliesLabel.TabIndex = 6;
        _assembliesLabel.Text = "Check assemblies to exclude:";
        // 
        // _assembliesListView
        // 
        _assembliesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _assembliesListView.Location = new Point(15, 177);
        _assembliesListView.Name = "_assembliesListView";
        _assembliesListView.Size = new Size(1093, 332);
        _assembliesListView.TabIndex = 7;
        _assembliesListView.UseCompatibleStateImageBehavior = false;
        _assembliesListView.View = View.Details;
        // 
        // _fontsGroupBox
        // 
        _fontsGroupBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _fontsGroupBox.Controls.Add(_uiFontCaptionLabel);
        _fontsGroupBox.Controls.Add(_uiFontPreviewLabel);
        _fontsGroupBox.Controls.Add(_changeUiFontButton);
        _fontsGroupBox.Controls.Add(_outputFontCaptionLabel);
        _fontsGroupBox.Controls.Add(_outputFontPreviewLabel);
        _fontsGroupBox.Controls.Add(_changeOutputFontButton);
        _fontsGroupBox.Location = new Point(15, 521);
        _fontsGroupBox.Name = "_fontsGroupBox";
        _fontsGroupBox.Size = new Size(1093, 130);
        _fontsGroupBox.TabIndex = 8;
        _fontsGroupBox.TabStop = false;
        _fontsGroupBox.Text = "Fonts";
        // 
        // _uiFontCaptionLabel
        // 
        _uiFontCaptionLabel.AutoSize = true;
        _uiFontCaptionLabel.Location = new Point(18, 42);
        _uiFontCaptionLabel.Name = "_uiFontCaptionLabel";
        _uiFontCaptionLabel.Size = new Size(82, 30);
        _uiFontCaptionLabel.TabIndex = 0;
        _uiFontCaptionLabel.Text = "UI font:";
        // 
        // _uiFontPreviewLabel
        // 
        _uiFontPreviewLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _uiFontPreviewLabel.AutoEllipsis = true;
        _uiFontPreviewLabel.BorderStyle = BorderStyle.FixedSingle;
        _uiFontPreviewLabel.Location = new Point(160, 39);
        _uiFontPreviewLabel.Name = "_uiFontPreviewLabel";
        _uiFontPreviewLabel.Size = new Size(745, 38);
        _uiFontPreviewLabel.TabIndex = 1;
        _uiFontPreviewLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _changeUiFontButton
        // 
        _changeUiFontButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _changeUiFontButton.Location = new Point(917, 37);
        _changeUiFontButton.Name = "_changeUiFontButton";
        _changeUiFontButton.Size = new Size(160, 42);
        _changeUiFontButton.TabIndex = 2;
        _changeUiFontButton.Text = "Change...";
        _changeUiFontButton.UseVisualStyleBackColor = true;
        _changeUiFontButton.Click += ChangeUiFontButton_Click;
        // 
        // _outputFontCaptionLabel
        // 
        _outputFontCaptionLabel.AutoSize = true;
        _outputFontCaptionLabel.Location = new Point(18, 88);
        _outputFontCaptionLabel.Name = "_outputFontCaptionLabel";
        _outputFontCaptionLabel.Size = new Size(126, 30);
        _outputFontCaptionLabel.TabIndex = 3;
        _outputFontCaptionLabel.Text = "Output font:";
        // 
        // _outputFontPreviewLabel
        // 
        _outputFontPreviewLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _outputFontPreviewLabel.AutoEllipsis = true;
        _outputFontPreviewLabel.BorderStyle = BorderStyle.FixedSingle;
        _outputFontPreviewLabel.Location = new Point(160, 85);
        _outputFontPreviewLabel.Name = "_outputFontPreviewLabel";
        _outputFontPreviewLabel.Size = new Size(745, 38);
        _outputFontPreviewLabel.TabIndex = 4;
        _outputFontPreviewLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _changeOutputFontButton
        // 
        _changeOutputFontButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _changeOutputFontButton.Location = new Point(917, 83);
        _changeOutputFontButton.Name = "_changeOutputFontButton";
        _changeOutputFontButton.Size = new Size(160, 42);
        _changeOutputFontButton.TabIndex = 5;
        _changeOutputFontButton.Text = "Change...";
        _changeOutputFontButton.UseVisualStyleBackColor = true;
        _changeOutputFontButton.Click += ChangeOutputFontButton_Click;
        // 
        // _okButton
        // 
        _okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _okButton.Location = new Point(869, 661);
        _okButton.Name = "_okButton";
        _okButton.Size = new Size(115, 44);
        _okButton.TabIndex = 9;
        _okButton.Text = "OK";
        _okButton.UseVisualStyleBackColor = true;
        _okButton.Click += OkButton_Click;
        // 
        // _cancelButton
        // 
        _cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.Location = new Point(993, 661);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(115, 44);
        _cancelButton.TabIndex = 10;
        _cancelButton.Text = "Cancel";
        _cancelButton.UseVisualStyleBackColor = true;
        // 
        // OptionsForm
        // 
        AcceptButton = _okButton;
        AutoScaleDimensions = new SizeF(12F, 30F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _cancelButton;
        ClientSize = new Size(1123, 722);
        Controls.Add(_cancelButton);
        Controls.Add(_okButton);
        Controls.Add(_fontsGroupBox);
        Controls.Add(_assembliesListView);
        Controls.Add(_assembliesLabel);
        Controls.Add(_loadAssembliesButton);
        Controls.Add(_targetsComboBox);
        Controls.Add(_targetsLabel);
        Controls.Add(_browseButton);
        Controls.Add(_sourceFolderTextBox);
        Controls.Add(_sourceFolderLabel);
        Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        Margin = new Padding(3, 4, 3, 4);
        MinimizeBox = false;
        MinimumSize = new Size(980, 620);
        Name = "OptionsForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Runtime Deploy Options";
        _fontsGroupBox.ResumeLayout(false);
        _fontsGroupBox.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label _sourceFolderLabel;
    private TextBox _sourceFolderTextBox;
    private Button _browseButton;
    private Label _targetsLabel;
    private ComboBox _targetsComboBox;
    private Button _loadAssembliesButton;
    private Label _assembliesLabel;
    private ListView _assembliesListView;
    private GroupBox _fontsGroupBox;
    private Label _uiFontCaptionLabel;
    private Label _uiFontPreviewLabel;
    private Button _changeUiFontButton;
    private Label _outputFontCaptionLabel;
    private Label _outputFontPreviewLabel;
    private Button _changeOutputFontButton;
    private Button _okButton;
    private Button _cancelButton;
}
