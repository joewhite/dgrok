namespace DGrok.Demo
{
    partial class AdHocParseControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lstRules = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.edtSource = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.edtResults = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parse rule:\r\n(Use \'Goal\' for source files)";
            // 
            // lstRules
            // 
            this.lstRules.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstRules.FormattingEnabled = true;
            this.lstRules.IntegralHeight = false;
            this.lstRules.Location = new System.Drawing.Point(3, 32);
            this.lstRules.Name = "lstRules";
            this.lstRules.Size = new System.Drawing.Size(143, 265);
            this.lstRules.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(152, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnParse);
            this.splitContainer1.Panel1.Controls.Add(this.edtSource);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.edtResults);
            this.splitContainer1.Size = new System.Drawing.Size(351, 294);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Source code:";
            // 
            // edtSource
            // 
            this.edtSource.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtSource.Location = new System.Drawing.Point(0, 16);
            this.edtSource.Multiline = true;
            this.edtSource.Name = "edtSource";
            this.edtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edtSource.Size = new System.Drawing.Size(351, 112);
            this.edtSource.TabIndex = 1;
            this.edtSource.WordWrap = false;
            // 
            // btnParse
            // 
            this.btnParse.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnParse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnParse.Location = new System.Drawing.Point(0, 134);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(75, 23);
            this.btnParse.TabIndex = 2;
            this.btnParse.Text = "&Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // edtResults
            // 
            this.edtResults.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtResults.Location = new System.Drawing.Point(0, 2);
            this.edtResults.Multiline = true;
            this.edtResults.Name = "edtResults";
            this.edtResults.ReadOnly = true;
            this.edtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edtResults.Size = new System.Drawing.Size(351, 131);
            this.edtResults.TabIndex = 0;
            this.edtResults.WordWrap = false;
            // 
            // AdHocParseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lstRules);
            this.Controls.Add(this.label1);
            this.Name = "AdHocParseControl";
            this.Size = new System.Drawing.Size(506, 300);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstRules;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.TextBox edtSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtResults;
    }
}
