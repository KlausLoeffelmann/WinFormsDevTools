namespace LayoutScalingIssues
{
    partial class Form1
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
            layoutDebugButton1 = new LayoutDebugButton();
            layoutDebugButton2 = new LayoutDebugButton();
            layoutDebugButton3 = new LayoutDebugButton();
            layoutDebugButton4 = new LayoutDebugButton();
            formInfoStatusStrip1 = new FormInfoStatusStrip();
            SuspendLayout();
            // 
            // layoutDebugButton1
            // 
            layoutDebugButton1.Location = new Point(20, 20);
            layoutDebugButton1.Margin = new Padding(4);
            layoutDebugButton1.Name = "layoutDebugButton1";
            layoutDebugButton1.Size = new Size(200, 100);
            layoutDebugButton1.TabIndex = 0;
            layoutDebugButton1.Text = "left-top";
            layoutDebugButton1.UseVisualStyleBackColor = true;
            // 
            // layoutDebugButton2
            // 
            layoutDebugButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            layoutDebugButton2.Location = new Point(780, 20);
            layoutDebugButton2.Margin = new Padding(4);
            layoutDebugButton2.Name = "layoutDebugButton2";
            layoutDebugButton2.Size = new Size(200, 100);
            layoutDebugButton2.TabIndex = 1;
            layoutDebugButton2.Text = "right-top";
            layoutDebugButton2.UseVisualStyleBackColor = true;
            // 
            // layoutDebugButton3
            // 
            layoutDebugButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            layoutDebugButton3.Location = new Point(20, 360);
            layoutDebugButton3.Margin = new Padding(4);
            layoutDebugButton3.Name = "layoutDebugButton3";
            layoutDebugButton3.Size = new Size(200, 100);
            layoutDebugButton3.TabIndex = 2;
            layoutDebugButton3.Text = "left-bottom";
            layoutDebugButton3.UseVisualStyleBackColor = true;
            // 
            // layoutDebugButton4
            // 
            layoutDebugButton4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            layoutDebugButton4.Location = new Point(780, 360);
            layoutDebugButton4.Margin = new Padding(4);
            layoutDebugButton4.Name = "layoutDebugButton4";
            layoutDebugButton4.Size = new Size(200, 100);
            layoutDebugButton4.TabIndex = 3;
            layoutDebugButton4.Text = "right-bottom";
            layoutDebugButton4.UseVisualStyleBackColor = true;
            // 
            // formInfoStatusStrip1
            // 
            formInfoStatusStrip1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            formInfoStatusStrip1.ImageScalingSize = new Size(28, 28);
            formInfoStatusStrip1.Location = new Point(0, 458);
            formInfoStatusStrip1.Name = "formInfoStatusStrip1";
            formInfoStatusStrip1.Size = new Size(1000, 42);
            formInfoStatusStrip1.TabIndex = 4;
            formInfoStatusStrip1.Text = "formInfoStatusStrip1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(15F, 38F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 500);
            Controls.Add(formInfoStatusStrip1);
            Controls.Add(layoutDebugButton4);
            Controls.Add(layoutDebugButton3);
            Controls.Add(layoutDebugButton2);
            Controls.Add(layoutDebugButton1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LayoutDebugButton layoutDebugButton1;
        private LayoutDebugButton layoutDebugButton2;
        private LayoutDebugButton layoutDebugButton3;
        private LayoutDebugButton layoutDebugButton4;
        private FormInfoStatusStrip formInfoStatusStrip1;
    }
}
