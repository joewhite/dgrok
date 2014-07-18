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
            this.edtStartingDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trvSummary = new System.Windows.Forms.TreeView();
            this.edtFileMasks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lnkShowParseResults = new System.Windows.Forms.LinkLabel();
            this.lnkParseAll = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // edtStartingDirectory
            // 
            this.edtStartingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtStartingDirectory.Location = new System.Drawing.Point(82, 5);
            this.edtStartingDirectory.Name = "edtStartingDirectory";
            this.edtStartingDirectory.Size = new System.Drawing.Size(437, 20);
            this.edtStartingDirectory.TabIndex = 1;
            this.edtStartingDirectory.Text = "c:\\program files\\borland\\bds\\4.0\\source\\win32\\rtl\\**";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search paths:";
            // 
            // trvSummary
            // 
            this.trvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSummary.Location = new System.Drawing.Point(0, 0);
            this.trvSummary.Name = "trvSummary";
            this.trvSummary.Size = new System.Drawing.Size(386, 182);
            this.trvSummary.TabIndex = 7;
            this.trvSummary.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvSummary_NodeMouseDoubleClick);
            this.trvSummary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trvSummary_KeyPress);
            // 
            // edtFileMasks
            // 
            this.edtFileMasks.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtFileMasks.Location = new System.Drawing.Point(82, 31);
            this.edtFileMasks.Name = "edtFileMasks";
            this.edtFileMasks.Size = new System.Drawing.Size(437, 20);
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
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Location = new System.Drawing.Point(0, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "&Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 57);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlActions);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.trvSummary);
            this.splitContainer1.Size = new System.Drawing.Size(516, 182);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.TabIndex = 8;
            // 
            // pnlActions
            // 
            this.pnlActions.AutoScroll = true;
            this.pnlActions.Controls.Add(this.lnkShowParseResults);
            this.pnlActions.Controls.Add(this.lnkParseAll);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.Location = new System.Drawing.Point(0, 0);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(126, 153);
            this.pnlActions.TabIndex = 2;
            // 
            // lnkShowParseResults
            // 
            this.lnkShowParseResults.AutoSize = true;
            this.lnkShowParseResults.Location = new System.Drawing.Point(0, 17);
            this.lnkShowParseResults.Name = "lnkShowParseResults";
            this.lnkShowParseResults.Size = new System.Drawing.Size(102, 13);
            this.lnkShowParseResults.TabIndex = 1;
            this.lnkShowParseResults.TabStop = true;
            this.lnkShowParseResults.Text = "Show Parse Results";
            this.lnkShowParseResults.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkShowParseResults_LinkClicked);
            // 
            // lnkParseAll
            // 
            this.lnkParseAll.AutoSize = true;
            this.lnkParseAll.Location = new System.Drawing.Point(0, 0);
            this.lnkParseAll.Name = "lnkParseAll";
            this.lnkParseAll.Size = new System.Drawing.Size(48, 13);
            this.lnkParseAll.TabIndex = 0;
            this.lnkParseAll.TabStop = true;
            this.lnkParseAll.Text = "Parse All";
            this.lnkParseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkParseAll_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(126, 29);
            this.panel1.TabIndex = 1;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // ParseSourceTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtFileMasks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtStartingDirectory);
            this.Name = "ParseSourceTreeControl";
            this.Size = new System.Drawing.Size(522, 242);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtStartingDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView trvSummary;
        private System.Windows.Forms.TextBox edtFileMasks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.LinkLabel lnkParseAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkShowParseResults;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
