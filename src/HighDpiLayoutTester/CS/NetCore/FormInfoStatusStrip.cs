namespace LayoutScalingIssues;

internal class FormInfoStatusStrip : StatusStrip
{
    private Form? _parentForm;
    private ToolStripTextBox? _textBoxOffsetX;
    private ToolStripTextBox? _textBoxOffsetY;
    private ToolStripTextBox? _textBoxSizeWidth;
    private ToolStripTextBox? _textBoxSizeHeight;

    public FormInfoStatusStrip()
    {
        InitializeComponent();
    }

    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        
        if (Parent is Form form)
        {
            // Unsubscribe from previous form if any
            if (_parentForm != null)
            {
                _parentForm.Resize -= ParentForm_Resize;
            }
            
            // Store reference to the new parent form
            _parentForm = form;
            
            // Subscribe to the form's resize event
            _parentForm.Resize += ParentForm_Resize;
            
            // Initial update
            UpdateClientInfo();
        }
    }

    private void ParentForm_Resize(object? sender, EventArgs e)
    {
        UpdateClientInfo();
    }

    private void UpdateClientInfo()
    {
        if (_parentForm != null && _textBoxOffsetX != null && _textBoxOffsetY != null && 
            _textBoxSizeWidth != null && _textBoxSizeHeight != null)
        {
            _textBoxOffsetX.Text = ClientLocation.X.ToString();
            _textBoxOffsetY.Text = ClientLocation.Y.ToString();
            _textBoxSizeWidth.Text = _parentForm.ClientSize.Width.ToString();
            _textBoxSizeHeight.Text = _parentForm.ClientSize.Height.ToString();
        }
    }

    private Point ClientLocation
    {
        get
        {
            if (_parentForm != null)
            {
                // Calculate the client location relative to the form's client area
                return new Point(
                    _parentForm.ClientRectangle.X, 
                    _parentForm.ClientRectangle.Y);
            }

            return Point.Empty;
        }
    }

    private void TextBoxSize_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && _parentForm != null && 
            _textBoxSizeWidth != null && _textBoxSizeHeight != null)
        {
            if (int.TryParse(_textBoxSizeWidth.Text, out int width) && 
                int.TryParse(_textBoxSizeHeight.Text, out int height))
            {
                _parentForm.ClientSize = new Size(width, height);
                // Prevent the ding sound
                e.SuppressKeyPress = true;
            }
        }
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.Dock = DockStyle.Bottom;
        
        // Client Offset Labels and TextBoxes
        ToolStripLabel labelClientOffset = new("Client Offset:");
        _textBoxOffsetX = new() { Name = "textBoxOffsetX", Width = 40, ReadOnly = true };
        ToolStripLabel labelCommaOffset = new(",");
        _textBoxOffsetY = new() { Name = "textBoxOffsetY", Width = 40, ReadOnly = true };
        
        // Separator
        ToolStripSeparator separator = new();
        
        // Client Size Labels and TextBoxes
        ToolStripLabel labelClientSize = new("Client Size:");
        _textBoxSizeWidth = new() { Name = "textBoxSizeWidth", Width = 40 };
        ToolStripLabel labelSizeMultiplier = new("×");
        _textBoxSizeHeight = new() { Name = "textBoxSizeHeight", Width = 40 };
        
        // Set up event handlers for size textboxes
        _textBoxSizeWidth.KeyDown += TextBoxSize_KeyDown;
        _textBoxSizeHeight.KeyDown += TextBoxSize_KeyDown;
        
        // Add items to StatusStrip
        this.Items.AddRange(new ToolStripItem[] {
            labelClientOffset,
            _textBoxOffsetX,
            labelCommaOffset,
            _textBoxOffsetY,
            separator,
            labelClientSize,
            _textBoxSizeWidth,
            labelSizeMultiplier,
            _textBoxSizeHeight
        });
        
        this.ResumeLayout(false);
    }
}
