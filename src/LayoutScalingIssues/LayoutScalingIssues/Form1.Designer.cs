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
            this.layoutDebugButton1 = new LayoutScalingIssues.LayoutDebugButton();
            this.layoutDebugButton2 = new LayoutScalingIssues.LayoutDebugButton();
            this.layoutDebugButton3 = new LayoutScalingIssues.LayoutDebugButton();
            this.layoutDebugButton4 = new LayoutScalingIssues.LayoutDebugButton();
            this.formInfoStatusStrip1 = new LayoutScalingIssues.FormInfoStatusStrip();
            this.debugPanel1 = new LayoutScalingIssues.DebugPanel();
            this.debugPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutDebugButton1
            // 
            this.layoutDebugButton1.Location = new System.Drawing.Point(100, 100);
            this.layoutDebugButton1.Margin = new System.Windows.Forms.Padding(100);
            this.layoutDebugButton1.Name = "layoutDebugButton1";
            this.layoutDebugButton1.Size = new System.Drawing.Size(200, 100);
            this.layoutDebugButton1.TabIndex = 0;
            this.layoutDebugButton1.Text = "left-top";
            this.layoutDebugButton1.UseVisualStyleBackColor = true;
            // 
            // layoutDebugButton2
            // 
            this.layoutDebugButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutDebugButton2.Location = new System.Drawing.Point(642, 100);
            this.layoutDebugButton2.Margin = new System.Windows.Forms.Padding(100);
            this.layoutDebugButton2.Name = "layoutDebugButton2";
            this.layoutDebugButton2.Size = new System.Drawing.Size(200, 100);
            this.layoutDebugButton2.TabIndex = 1;
            this.layoutDebugButton2.Text = "right-top";
            this.layoutDebugButton2.UseVisualStyleBackColor = true;
            // 
            // layoutDebugButton3
            // 
            this.layoutDebugButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.layoutDebugButton3.Location = new System.Drawing.Point(100, 228);
            this.layoutDebugButton3.Margin = new System.Windows.Forms.Padding(100);
            this.layoutDebugButton3.Name = "layoutDebugButton3";
            this.layoutDebugButton3.Size = new System.Drawing.Size(200, 100);
            this.layoutDebugButton3.TabIndex = 2;
            this.layoutDebugButton3.Text = "left-bottom";
            this.layoutDebugButton3.UseVisualStyleBackColor = true;
            // 
            // layoutDebugButton4
            // 
            this.layoutDebugButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutDebugButton4.Location = new System.Drawing.Point(642, 228);
            this.layoutDebugButton4.Margin = new System.Windows.Forms.Padding(100);
            this.layoutDebugButton4.Name = "layoutDebugButton4";
            this.layoutDebugButton4.Size = new System.Drawing.Size(200, 100);
            this.layoutDebugButton4.TabIndex = 3;
            this.layoutDebugButton4.Text = "right-bottom";
            this.layoutDebugButton4.UseVisualStyleBackColor = true;
            // 
            // formInfoStatusStrip1
            // 
            this.formInfoStatusStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formInfoStatusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.formInfoStatusStrip1.Location = new System.Drawing.Point(0, 477);
            this.formInfoStatusStrip1.Name = "formInfoStatusStrip1";
            this.formInfoStatusStrip1.Size = new System.Drawing.Size(1000, 23);
            this.formInfoStatusStrip1.TabIndex = 4;
            this.formInfoStatusStrip1.Text = "formInfoStatusStrip1";
            // 
            // debugPanel1
            // 
            this.debugPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugPanel1.Controls.Add(this.layoutDebugButton1);
            this.debugPanel1.Controls.Add(this.layoutDebugButton3);
            this.debugPanel1.Controls.Add(this.layoutDebugButton4);
            this.debugPanel1.Controls.Add(this.layoutDebugButton2);
            this.debugPanel1.Location = new System.Drawing.Point(29, 29);
            this.debugPanel1.Margin = new System.Windows.Forms.Padding(20);
            this.debugPanel1.Name = "debugPanel1";
            this.debugPanel1.Size = new System.Drawing.Size(942, 428);
            this.debugPanel1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.debugPanel1);
            this.Controls.Add(this.formInfoStatusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.debugPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LayoutDebugButton layoutDebugButton1;
        private LayoutDebugButton layoutDebugButton2;
        private LayoutDebugButton layoutDebugButton3;
        private LayoutDebugButton layoutDebugButton4;
        private FormInfoStatusStrip formInfoStatusStrip1;
        private DebugPanel debugPanel1;
    }
}
