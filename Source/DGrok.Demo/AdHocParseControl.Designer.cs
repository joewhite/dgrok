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
            this.parseTextControl1 = new DGrok.Demo.ParseTextControl();
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
            this.lstRules.SelectedIndexChanged += new System.EventHandler(this.lstRules_SelectedIndexChanged);
            // 
            // parseTextControl1
            // 
            this.parseTextControl1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.parseTextControl1.Location = new System.Drawing.Point(152, 3);
            this.parseTextControl1.Name = "parseTextControl1";
            this.parseTextControl1.Size = new System.Drawing.Size(351, 294);
            this.parseTextControl1.TabIndex = 2;
            // 
            // AdHocParseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.parseTextControl1);
            this.Controls.Add(this.lstRules);
            this.Controls.Add(this.label1);
            this.Name = "AdHocParseControl";
            this.Size = new System.Drawing.Size(506, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstRules;
        private ParseTextControl parseTextControl1;
    }
}
