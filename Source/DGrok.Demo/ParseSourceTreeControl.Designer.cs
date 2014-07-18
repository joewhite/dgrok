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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trvActions = new System.Windows.Forms.TreeView();
            this.edtActionDescription = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnParseAll = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRunAction = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnCopyAllHits = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.edtStartingDirectory.TextChanged += new System.EventHandler(this.edtStartingDirectory_TextChanged);
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
            this.trvSummary.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvSummary.Location = new System.Drawing.Point(0, 0);
            this.trvSummary.Name = "trvSummary";
            this.trvSummary.Size = new System.Drawing.Size(356, 153);
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
            this.edtFileMasks.TextChanged += new System.EventHandler(this.edtFileMasks_TextChanged);
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
            this.btnStop.Location = new System.Drawing.Point(81, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCopyAllHits);
            this.splitContainer1.Panel2.Controls.Add(this.trvSummary);
            this.splitContainer1.Size = new System.Drawing.Size(516, 182);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 29);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.trvActions);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.edtActionDescription);
            this.splitContainer2.Size = new System.Drawing.Size(156, 124);
            this.splitContainer2.SplitterDistance = 74;
            this.splitContainer2.TabIndex = 3;
            // 
            // trvActions
            // 
            this.trvActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvActions.HideSelection = false;
            this.trvActions.Location = new System.Drawing.Point(0, 0);
            this.trvActions.Name = "trvActions";
            this.trvActions.Size = new System.Drawing.Size(156, 74);
            this.trvActions.TabIndex = 1;
            this.trvActions.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvActions_NodeMouseDoubleClick);
            this.trvActions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvActions_AfterSelect);
            this.trvActions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trvActions_KeyPress);
            // 
            // edtActionDescription
            // 
            this.edtActionDescription.BackColor = System.Drawing.SystemColors.Info;
            this.edtActionDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtActionDescription.ForeColor = System.Drawing.SystemColors.InfoText;
            this.edtActionDescription.Location = new System.Drawing.Point(0, 0);
            this.edtActionDescription.Multiline = true;
            this.edtActionDescription.Name = "edtActionDescription";
            this.edtActionDescription.ReadOnly = true;
            this.edtActionDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtActionDescription.Size = new System.Drawing.Size(156, 46);
            this.edtActionDescription.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnParseAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(156, 29);
            this.panel2.TabIndex = 0;
            // 
            // btnParseAll
            // 
            this.btnParseAll.Location = new System.Drawing.Point(0, 0);
            this.btnParseAll.Name = "btnParseAll";
            this.btnParseAll.Size = new System.Drawing.Size(75, 23);
            this.btnParseAll.TabIndex = 0;
            this.btnParseAll.Text = "&Parse All";
            this.btnParseAll.UseVisualStyleBackColor = true;
            this.btnParseAll.Click += new System.EventHandler(this.btnParseAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRunAction);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 29);
            this.panel1.TabIndex = 2;
            // 
            // btnRunAction
            // 
            this.btnRunAction.Location = new System.Drawing.Point(0, 6);
            this.btnRunAction.Name = "btnRunAction";
            this.btnRunAction.Size = new System.Drawing.Size(75, 23);
            this.btnRunAction.TabIndex = 0;
            this.btnRunAction.Text = "&Run Action";
            this.btnRunAction.UseVisualStyleBackColor = true;
            this.btnRunAction.Click += new System.EventHandler(this.btnRunAction_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // btnCopyAllHits
            // 
            this.btnCopyAllHits.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyAllHits.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopyAllHits.Location = new System.Drawing.Point(0, 159);
            this.btnCopyAllHits.Name = "btnCopyAllHits";
            this.btnCopyAllHits.Size = new System.Drawing.Size(124, 23);
            this.btnCopyAllHits.TabIndex = 8;
            this.btnCopyAllHits.Text = "Copy All to Clipboard";
            this.btnCopyAllHits.UseVisualStyleBackColor = true;
            this.btnCopyAllHits.Click += new System.EventHandler(this.btnCopyAllHits_Click);
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
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnParseAll;
        private System.Windows.Forms.Button btnRunAction;
        private System.Windows.Forms.TreeView trvActions;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox edtActionDescription;
        private System.Windows.Forms.Button btnCopyAllHits;
    }
}
