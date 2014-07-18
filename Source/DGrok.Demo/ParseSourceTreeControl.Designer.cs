namespace DGrok.Demo
{
    partial class ParseSourceTreeControl
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
            this.components = new System.ComponentModel.Container();
            this.btnParseAll = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.edtStartingDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trvSummary = new System.Windows.Forms.TreeView();
            this.dlgBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.edtFileMasks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrRunner = new System.Windows.Forms.Timer(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnParseAll
            // 
            this.btnParseAll.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParseAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnParseAll.Location = new System.Drawing.Point(363, 29);
            this.btnParseAll.Name = "btnParseAll";
            this.btnParseAll.Size = new System.Drawing.Size(75, 23);
            this.btnParseAll.TabIndex = 5;
            this.btnParseAll.Text = "&Parse All";
            this.btnParseAll.UseVisualStyleBackColor = true;
            this.btnParseAll.Click += new System.EventHandler(this.btnParseAll_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowse.Location = new System.Drawing.Point(444, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // edtStartingDirectory
            // 
            this.edtStartingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtStartingDirectory.Location = new System.Drawing.Point(98, 5);
            this.edtStartingDirectory.Name = "edtStartingDirectory";
            this.edtStartingDirectory.Size = new System.Drawing.Size(340, 20);
            this.edtStartingDirectory.TabIndex = 1;
            this.edtStartingDirectory.Text = "c:\\program files\\borland\\bds\\4.0\\source\\win32\\rtl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting directory:";
            // 
            // trvSummary
            // 
            this.trvSummary.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvSummary.Location = new System.Drawing.Point(3, 57);
            this.trvSummary.Name = "trvSummary";
            this.trvSummary.Size = new System.Drawing.Size(516, 182);
            this.trvSummary.TabIndex = 7;
            this.trvSummary.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvSummary_NodeMouseDoubleClick);
            this.trvSummary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trvSummary_KeyPress);
            // 
            // edtFileMasks
            // 
            this.edtFileMasks.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtFileMasks.Location = new System.Drawing.Point(98, 31);
            this.edtFileMasks.Name = "edtFileMasks";
            this.edtFileMasks.Size = new System.Drawing.Size(259, 20);
            this.edtFileMasks.TabIndex = 4;
            this.edtFileMasks.Text = "*.pas;*.dpr;*.dpk;*.pp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File masks:";
            // 
            // tmrRunner
            // 
            this.tmrRunner.Interval = 250;
            this.tmrRunner.Tick += new System.EventHandler(this.tmrRunner_Tick);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Location = new System.Drawing.Point(444, 29);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "&Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ParseSourceTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtFileMasks);
            this.Controls.Add(this.trvSummary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtStartingDirectory);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnParseAll);
            this.Name = "ParseSourceTreeControl";
            this.Size = new System.Drawing.Size(522, 242);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnParseAll;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox edtStartingDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView trvSummary;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowse;
        private System.Windows.Forms.TextBox edtFileMasks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmrRunner;
        private System.Windows.Forms.Button btnStop;
    }
}
