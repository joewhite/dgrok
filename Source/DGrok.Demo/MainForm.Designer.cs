namespace DGrok.Demo
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.parseSourceTreeControl1 = new DGrok.Demo.ParseSourceTreeControl();
            this.adHocParseControl1 = new DGrok.Demo.AdHocParseControl();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(579, 348);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.adHocParseControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(571, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ad-Hoc Parsing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.parseSourceTreeControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parse Source Tree";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // parseSourceTreeControl1
            // 
            this.parseSourceTreeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parseSourceTreeControl1.Location = new System.Drawing.Point(3, 3);
            this.parseSourceTreeControl1.Name = "parseSourceTreeControl1";
            this.parseSourceTreeControl1.Size = new System.Drawing.Size(565, 316);
            this.parseSourceTreeControl1.TabIndex = 0;
            // 
            // adHocParseControl1
            // 
            this.adHocParseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adHocParseControl1.Location = new System.Drawing.Point(3, 3);
            this.adHocParseControl1.Name = "adHocParseControl1";
            this.adHocParseControl1.Size = new System.Drawing.Size(565, 316);
            this.adHocParseControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 348);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "DGrok Demo";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ParseSourceTreeControl parseSourceTreeControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private AdHocParseControl adHocParseControl1;
    }
}