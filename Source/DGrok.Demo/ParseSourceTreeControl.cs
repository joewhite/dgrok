// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DGrok.Framework;

namespace DGrok.Demo
{
    public partial class ParseSourceTreeControl : UserControl
    {
        private delegate void Block();

        private ParseSourceTreeRunner _runner = new ParseSourceTreeRunner();

        public ParseSourceTreeControl()
        {
            InitializeComponent();
            ShowRunnerStatus();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgBrowse.SelectedPath = edtStartingDirectory.Text;
            if (dlgBrowse.ShowDialog() == DialogResult.OK)
                edtStartingDirectory.Text = dlgBrowse.SelectedPath;
        }
        private void btnParseAll_Click(object sender, EventArgs e)
        {
            _runner.BeginExecute(edtStartingDirectory.Text, edtFileMasks.Text);
            ShowRunnerStatus();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            _runner.Canceled = true;
        }
        private TreeNode CreateNodeForFailingFile(Location errorLocation)
        {
            TreeNode node = new TreeNode(errorLocation.FileName + ":" + errorLocation.Offset);
            node.Tag = new Block(delegate { ShowWindowForFailingFile(errorLocation); });
            return node;
        }
        private TreeNode CreateNodeForPassingFile(string fileName)
        {
            TreeNode node = new TreeNode(fileName);
            node.Tag = new Block(delegate { ShowWindowForPassingFile(fileName); });
            return node;
        }
        private void ExecuteNodeAction(TreeNode node)
        {
            if (node == null)
                return;
            Block block = (Block) node.Tag;
            if (block != null)
                block();
        }
        private void ParseFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                ((Form) sender).Close();
        }
        private void ShowResultsInTree(IDictionary<string, Exception> results)
        {
            trvSummary.BeginUpdate();
            try
            {
                trvSummary.Nodes.Clear();
                List<string> passed = new List<string>();
                Dictionary<string, List<string>> failed = new Dictionary<string, List<string>>();
                int failingCount = 0;
                foreach (KeyValuePair<string, Exception> pair in results)
                {
                    if (pair.Value == null)
                        passed.Add(pair.Key);
                    else
                    {
                        if (!failed.ContainsKey(pair.Value.Message))
                            failed.Add(pair.Value.Message, new List<string>());
                        failed[pair.Value.Message].Add(pair.Key);
                        ++failingCount;
                    }
                }

                TreeNode passingNode = trvSummary.Nodes.Add(String.Format("Passing ({0})", passed.Count));
                passed.Sort(StringComparer.CurrentCultureIgnoreCase);
                foreach (string fileName in passed)
                    passingNode.Nodes.Add(CreateNodeForPassingFile(fileName));
                List<string> messages = new List<string>(failed.Keys);
                messages.Sort(StringComparer.CurrentCultureIgnoreCase);
                TreeNode failingNode = trvSummary.Nodes.Add(String.Format("Failing ({0})", failingCount));
                foreach (string message in messages)
                {
                    TreeNode failureNode = failingNode.Nodes.Add(
                        String.Format("{0} ({1})", message, failed[message].Count));
                    foreach (string fileName in failed[message])
                    {
                        DGrokException ex = results[fileName] as DGrokException;
                        Location errorLocation = ex != null ? ex.Location : new Location(fileName, 0);
                        failureNode.Nodes.Add(CreateNodeForFailingFile(errorLocation));
                    }
                }
            }
            finally
            {
                trvSummary.EndUpdate();
            }
        }
        private void ShowRunnerStatus()
        {
            ParseSourceTreeStatus status = _runner.Status;
            btnParseAll.Enabled = !status.IsRunning;
            btnStop.Enabled = status.IsRunning;
            tmrRunner.Enabled = status.IsRunning;
            if (status.Error != null)
                ShowSingleTreeNode(status.Error.Message);
            else if (status.Progress != null)
                ShowSingleTreeNode(status.Progress);
            else
                ShowResultsInTree(status.Results);
        }
        private void ShowSingleTreeNode(string text)
        {
            if (trvSummary.Nodes.Count != 1 || trvSummary.Nodes[0].Nodes.Count != 0)
            {
                trvSummary.Nodes.Clear();
                trvSummary.Nodes.Add("");
            }
            trvSummary.Nodes[0].Text = text;
        }
        private Form CreateEmptyWindowForFile(string fileName)
        {
            Form form = new Form();
            form.Size = ParentForm.Size;
            form.Text = fileName;
            form.KeyPreview = true;
            form.KeyDown += ParseFormKeyDown;
            return form;
        }
        private void ShowWindowForFailingFile(Location errorLocation)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Form form = CreateEmptyWindowForFile(errorLocation.FileName);
                TextBox textBox = new TextBox();
                textBox.Multiline = true;
                textBox.WordWrap = false;
                textBox.ScrollBars = ScrollBars.Both;
                textBox.Dock = DockStyle.Fill;
                textBox.Text = File.ReadAllText(errorLocation.FileName);
                textBox.SelectionStart = errorLocation.Offset;
                form.Controls.Add(textBox);
                form.Show();
                textBox.ScrollToCaret();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void ShowWindowForPassingFile(string fileName)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (fileName == null)
                    return;
                Form form = CreateEmptyWindowForFile(fileName);
                ParseTextControl control = new ParseTextControl();
                control.RuleType = RuleType.Goal;
                control.Dock = DockStyle.Fill;
                form.Controls.Add(control);
                form.Show();
                control.ParseString(File.ReadAllText(fileName));
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void tmrRunner_Tick(object sender, EventArgs e)
        {
            ShowRunnerStatus();
        }
        private void trvSummary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                ExecuteNodeAction(trvSummary.SelectedNode);
            }
        }
        private void trvSummary_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ExecuteNodeAction(e.Node);
        }
    }
}
