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
using DGrok.Visitors;

namespace DGrok.Demo
{
    public partial class ParseSourceTreeControl : UserControl
    {
        private delegate void Block();
        private delegate object DoWorkDelegate();

        private Catalog _catalog;
        private CodeBase _codeBase;
        private DoWorkDelegate _doWork;

        public ParseSourceTreeControl()
        {
            InitializeComponent();
            LoadCatalog();
            ShowRunnerStatus();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _doWork();
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSingleTreeNode(e.UserState.ToString());
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CodeBase codeBase = e.Result as CodeBase;
            if (codeBase != null)
            {
                _codeBase = codeBase;
                ShowParseResultsInTree();
            }
            IEnumerable<Hit> hits = e.Result as IEnumerable<Hit>;
            if (hits != null)
            {
                trvSummary.Nodes.Clear();
                trvSummary.BeginUpdate();
                try
                {
                    List<Hit> sortedHits = new List<Hit>(hits);
                    sortedHits.Sort(delegate(Hit a, Hit b)
                    {
                        int result = String.Compare(
                            Path.GetFileName(a.Location.FileName),
                            Path.GetFileName(b.Location.FileName),
                            StringComparison.CurrentCultureIgnoreCase);
                        if (result != 0)
                            return result;
                        if (a.Location.Offset < b.Location.Offset)
                            return -1;
                        if (a.Location.Offset > b.Location.Offset)
                            return 1;
                        return 0;
                    });
                    foreach (Hit hit in sortedHits)
                    {
                        TreeNode treeNode = CreateNodeForLocation(hit.Location);
                        treeNode.Text = Path.GetFileName(hit.Location.FileName) +
                            ":" + hit.Location.Offset + ": " + hit.Description;
                        trvSummary.Nodes.Add(treeNode);
                    }
                }
                finally
                {
                    trvSummary.EndUpdate();
                }
            }
            ShowRunnerStatus();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
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
        private TreeNode CreateNodeForLocation(Location location)
        {
            TreeNode node = new TreeNode(location.FileName + ":" + location.Offset);
            node.Tag = new Block(delegate { ShowWindowForFailingFile(location); });
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
        private void lnkParseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CodeBaseOptions options = new CodeBaseOptions(edtStartingDirectory.Text,
                edtFileMasks.Text, CompilerDefines.CreateStandard());
            RunBackground(delegate
            {
                return CodeBaseWorker.Execute(options, backgroundWorker1);
            });
        }
        private void lnkShowParseResults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowParseResultsInTree();
        }
        private void LoadCatalog()
        {
            _catalog = Catalog.Load();

            int maxBottom = 0;
            foreach (Control control in pnlActions.Controls)
            {
                if (control.Bottom > maxBottom)
                    maxBottom = control.Bottom;
            }
            maxBottom += 10;

            foreach (CodeBaseActionProxy item in _catalog.Items)
            {
                LinkLabel link = new LinkLabel();
                link.Top = maxBottom;
                link.Left = lnkParseAll.Left;
                link.Height = lnkParseAll.Height;
                link.Text = item.Name;
                link.AutoSize = true;
                // Make sure we get the item for this iteration, not the variable
                // that changes throughout the loop
                CodeBaseActionProxy capturedItem = item;
                link.Click += delegate
                {
                    CodeBase codeBase = _codeBase;
                    RunBackground(delegate
                    {
                        return capturedItem.Execute(codeBase);
                    });
                };
                pnlActions.Controls.Add(link);
                maxBottom = link.Bottom;
            }
        }
        private void ParseFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                ((Form) sender).Close();
        }
        private void RunBackground(DoWorkDelegate doWork)
        {
            _doWork = doWork;
            backgroundWorker1.RunWorkerAsync();
            ShowRunnerStatus();
        }
        private void ShowParseResultsInTree()
        {
            trvSummary.BeginUpdate();
            try
            {
                trvSummary.Nodes.Clear();
                Dictionary<string, List<string>> failed = new Dictionary<string, List<string>>();
                int failingCount = 0;
                foreach (KeyValuePair<string, Exception> pair in _codeBase.Errors)
                {
                    if (!failed.ContainsKey(pair.Value.Message))
                        failed.Add(pair.Value.Message, new List<string>());
                    failed[pair.Value.Message].Add(pair.Key);
                    ++failingCount;
                }

                TreeNode passingNode = trvSummary.Nodes.Add(String.Format("Passing ({0})",
                    _codeBase.ParsedFileCount));
                foreach (string fileName in _codeBase.ParsedFileNames)
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
                        DGrokException ex = _codeBase.ErrorByFileName(fileName) as DGrokException;
                        Location errorLocation = ex != null ? ex.Location : new Location(fileName,
                            File.ReadAllText(fileName), 0);
                        failureNode.Nodes.Add(CreateNodeForLocation(errorLocation));
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
            pnlActions.Enabled = !backgroundWorker1.IsBusy;
            btnStop.Enabled = backgroundWorker1.IsBusy;
            foreach (Control control in pnlActions.Controls)
            {
                if (control != lnkParseAll)
                    control.Enabled = (_codeBase != null);
            }
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
