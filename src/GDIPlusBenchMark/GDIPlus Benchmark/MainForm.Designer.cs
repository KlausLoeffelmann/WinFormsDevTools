namespace GDIPlus_Benchmark
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _menuStrip = new MenuStrip();
            _tsmBenchMarks = new ToolStripMenuItem();
            _tsmLines = new ToolStripMenuItem();
            _tsmShapes = new ToolStripMenuItem();
            _tsmFilledShapes = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            _tsmStartBenchmark = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            totalRunsToolStripMenuItem = new ToolStripMenuItem();
            figureRunsToolStripMenuItem = new ToolStripMenuItem();
            _toolStrip = new ToolStrip();
            _tslBenchmark1Caption = new ToolStripLabel();
            _tscBenchmark1 = new ToolStripComboBox();
            _tslBenchmark2Caption = new ToolStripLabel();
            _tscBenchmark2 = new ToolStripComboBox();
            _pnlRenderSurface = new Panel();
            _statusStrip = new StatusStrip();
            _tslTestResolutionLabel = new ToolStripStatusLabel();
            _tslTestResolution = new ToolStripStatusLabel();
            _tslTotalRunsCaption = new ToolStripStatusLabel();
            _tslTotalRuns = new ToolStripStatusLabel();
            _tslFigureRunsCaption = new ToolStripStatusLabel();
            _tslFigureRuns = new ToolStripStatusLabel();
            _lvwResults = new ListView();
            _menuStrip.SuspendLayout();
            _toolStrip.SuspendLayout();
            _statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _menuStrip
            // 
            _menuStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _menuStrip.ImageScalingSize = new Size(32, 32);
            _menuStrip.Items.AddRange(new ToolStripItem[] { _tsmBenchMarks, _tsmStartBenchmark, toolStripMenuItem1, totalRunsToolStripMenuItem, figureRunsToolStripMenuItem });
            _menuStrip.Location = new Point(0, 0);
            _menuStrip.Name = "_menuStrip";
            _menuStrip.Padding = new Padding(10, 2, 2, 2);
            _menuStrip.Size = new Size(1400, 33);
            _menuStrip.TabIndex = 0;
            _menuStrip.Text = "menuStrip1";
            // 
            // _tsmBenchMarks
            // 
            _tsmBenchMarks.DropDownItems.AddRange(new ToolStripItem[] { _tsmLines, _tsmShapes, _tsmFilledShapes, quitToolStripMenuItem });
            _tsmBenchMarks.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _tsmBenchMarks.Name = "_tsmBenchMarks";
            _tsmBenchMarks.Size = new Size(52, 29);
            _tsmBenchMarks.Text = "&File";
            // 
            // _tsmLines
            // 
            _tsmLines.Name = "_tsmLines";
            _tsmLines.Size = new Size(209, 30);
            _tsmLines.Text = "Save Results...";
            // 
            // _tsmShapes
            // 
            _tsmShapes.Name = "_tsmShapes";
            _tsmShapes.Size = new Size(209, 30);
            _tsmShapes.Text = "Load Results...";
            // 
            // _tsmFilledShapes
            // 
            _tsmFilledShapes.Name = "_tsmFilledShapes";
            _tsmFilledShapes.Size = new Size(206, 6);
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(209, 30);
            quitToolStripMenuItem.Text = "Quit";
            // 
            // _tsmStartBenchmark
            // 
            _tsmStartBenchmark.Name = "_tsmStartBenchmark";
            _tsmStartBenchmark.Size = new Size(175, 29);
            _tsmStartBenchmark.Text = "Start Benchmark...";
            _tsmStartBenchmark.Click += OnStartBenchmarkClicked;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(151, 29);
            toolStripMenuItem1.Text = "Test Resolution";
            // 
            // totalRunsToolStripMenuItem
            // 
            totalRunsToolStripMenuItem.Name = "totalRunsToolStripMenuItem";
            totalRunsToolStripMenuItem.Size = new Size(112, 29);
            totalRunsToolStripMenuItem.Text = "Total Runs";
            // 
            // figureRunsToolStripMenuItem
            // 
            figureRunsToolStripMenuItem.Name = "figureRunsToolStripMenuItem";
            figureRunsToolStripMenuItem.Size = new Size(125, 29);
            figureRunsToolStripMenuItem.Text = "Figure Runs";
            // 
            // _toolStrip
            // 
            _toolStrip.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _toolStrip.ImageScalingSize = new Size(32, 32);
            _toolStrip.Items.AddRange(new ToolStripItem[] { _tslBenchmark1Caption, _tscBenchmark1, _tslBenchmark2Caption, _tscBenchmark2 });
            _toolStrip.Location = new Point(0, 33);
            _toolStrip.Name = "_toolStrip";
            _toolStrip.Size = new Size(1400, 38);
            _toolStrip.TabIndex = 1;
            _toolStrip.Text = "toolStrip1";
            // 
            // _tslBenchmark1Caption
            // 
            _tslBenchmark1Caption.Margin = new Padding(5);
            _tslBenchmark1Caption.Name = "_tslBenchmark1Caption";
            _tslBenchmark1Caption.Size = new Size(118, 28);
            _tslBenchmark1Caption.Text = "Benchmark 1:";
            // 
            // _tscBenchmark1
            // 
            _tscBenchmark1.Margin = new Padding(5);
            _tscBenchmark1.Name = "_tscBenchmark1";
            _tscBenchmark1.Size = new Size(200, 28);
            // 
            // _tslBenchmark2Caption
            // 
            _tslBenchmark2Caption.Margin = new Padding(5);
            _tslBenchmark2Caption.Name = "_tslBenchmark2Caption";
            _tslBenchmark2Caption.Size = new Size(118, 28);
            _tslBenchmark2Caption.Text = "Benchmark 2:";
            // 
            // _tscBenchmark2
            // 
            _tscBenchmark2.Margin = new Padding(5);
            _tscBenchmark2.Name = "_tscBenchmark2";
            _tscBenchmark2.Size = new Size(200, 28);
            // 
            // _pnlRenderSurface
            // 
            _pnlRenderSurface.Dock = DockStyle.Fill;
            _pnlRenderSurface.Location = new Point(0, 71);
            _pnlRenderSurface.Margin = new Padding(4);
            _pnlRenderSurface.Name = "_pnlRenderSurface";
            _pnlRenderSurface.Size = new Size(1400, 500);
            _pnlRenderSurface.TabIndex = 2;
            // 
            // _statusStrip
            // 
            _statusStrip.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _statusStrip.ImageScalingSize = new Size(24, 24);
            _statusStrip.Items.AddRange(new ToolStripItem[] { _tslTestResolutionLabel, _tslTestResolution, _tslTotalRunsCaption, _tslTotalRuns, _tslFigureRunsCaption, _tslFigureRuns });
            _statusStrip.Location = new Point(0, 806);
            _statusStrip.Name = "_statusStrip";
            _statusStrip.Size = new Size(1400, 33);
            _statusStrip.TabIndex = 4;
            _statusStrip.Text = "statusStrip1";
            // 
            // _tslTestResolutionLabel
            // 
            _tslTestResolutionLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _tslTestResolutionLabel.Margin = new Padding(4);
            _tslTestResolutionLabel.Name = "_tslTestResolutionLabel";
            _tslTestResolutionLabel.Size = new Size(151, 25);
            _tslTestResolutionLabel.Text = "Test Resolution:";
            // 
            // _tslTestResolution
            // 
            _tslTestResolution.Margin = new Padding(4);
            _tslTestResolution.Name = "_tslTestResolution";
            _tslTestResolution.Size = new Size(119, 25);
            _tslTestResolution.Text = "#### x ####";
            // 
            // _tslTotalRunsCaption
            // 
            _tslTotalRunsCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _tslTotalRunsCaption.Margin = new Padding(4);
            _tslTotalRunsCaption.Name = "_tslTotalRunsCaption";
            _tslTotalRunsCaption.Size = new Size(105, 25);
            _tslTotalRunsCaption.Text = "Total runs:";
            // 
            // _tslTotalRuns
            // 
            _tslTotalRuns.Margin = new Padding(4);
            _tslTotalRuns.Name = "_tslTotalRuns";
            _tslTotalRuns.Size = new Size(56, 25);
            _tslTotalRuns.Text = "####";
            // 
            // _tslFigureRunsCaption
            // 
            _tslFigureRunsCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _tslFigureRunsCaption.Margin = new Padding(4);
            _tslFigureRunsCaption.Name = "_tslFigureRunsCaption";
            _tslFigureRunsCaption.Size = new Size(119, 25);
            _tslFigureRunsCaption.Text = "Figure runs:";
            // 
            // _tslFigureRuns
            // 
            _tslFigureRuns.Margin = new Padding(4);
            _tslFigureRuns.Name = "_tslFigureRuns";
            _tslFigureRuns.Size = new Size(56, 25);
            _tslFigureRuns.Text = "####";
            // 
            // _lvwResults
            // 
            _lvwResults.Dock = DockStyle.Bottom;
            _lvwResults.Location = new Point(0, 571);
            _lvwResults.Margin = new Padding(4);
            _lvwResults.Name = "_lvwResults";
            _lvwResults.Size = new Size(1400, 235);
            _lvwResults.TabIndex = 3;
            _lvwResults.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 839);
            Controls.Add(_pnlRenderSurface);
            Controls.Add(_lvwResults);
            Controls.Add(_toolStrip);
            Controls.Add(_menuStrip);
            Controls.Add(_statusStrip);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = _menuStrip;
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "GDI/GDI+/Direct2D Benchmark Tester";
            _menuStrip.ResumeLayout(false);
            _menuStrip.PerformLayout();
            _toolStrip.ResumeLayout(false);
            _toolStrip.PerformLayout();
            _statusStrip.ResumeLayout(false);
            _statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip _menuStrip;
        private ToolStripMenuItem _tsmBenchMarks;
        private ToolStripMenuItem _tsmLines;
        private ToolStripMenuItem _tsmShapes;
        private ToolStrip _toolStrip;
        private ToolStripLabel _tslBenchmark2Caption;
        private Panel _pnlRenderSurface;
        private ToolStripComboBox _tscBenchmark2;
        private ToolStripLabel _tslBenchmark1Caption;
        private ToolStripComboBox _tscBenchmark1;
        private ToolStripSeparator _tsmFilledShapes;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem totalRunsToolStripMenuItem;
        private ToolStripMenuItem figureRunsToolStripMenuItem;
        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _tslTestResolutionLabel;
        private ToolStripStatusLabel _tslTestResolution;
        private ToolStripStatusLabel _tslTotalRunsCaption;
        private ToolStripStatusLabel _tslTotalRuns;
        private ToolStripStatusLabel _tslFigureRunsCaption;
        private ToolStripStatusLabel _tslFigureRuns;
        private ListView _lvwResults;
        private ToolStripMenuItem _tsmStartBenchmark;
    }
}
