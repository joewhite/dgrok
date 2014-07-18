// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
        private delegate object DoWorkDelegate();
        private class ActionResults
        {
            private CodeBaseActionProxy _action;
            private IList<Hit> _hits;

            public ActionResults(CodeBaseActionProxy action, IList<Hit> hits)
            {
                _action = action;
                _hits = hits;
            }

            public CodeBaseActionProxy Action
            {
                get { return _action; }
            }
            public IList<Hit> Hits
            {
                get { return _hits; }
            }
        }

        private Catalog _catalog;
        private CodeBase _codeBase;
        private CodeBaseOptions _codeBaseOptions;

        public ParseSourceTreeControl()
        {
            InitializeComponent();
            CodeBaseOptions = new CodeBaseOptions();
            LoadCatalog();
            ShowRunnerStatus();
        }

        private bool CanRunAction
        {
            get { return !backgroundWorker1.IsBusy && (_codeBase != null); }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CodeBaseOptions CodeBaseOptions
        {
            get { return _codeBaseOptions; }
            set
            {
                _codeBaseOptions = value;
                edtStartingDirectory.Text = _codeBaseOptions.SearchPaths;
                edtFileMasks.Text = _codeBaseOptions.FileMasks;
            }
        }

        private void AppendNodeToStringBuilder(int indent, TreeNode node, StringBuilder sb)
        {
            sb.Append('\t', indent);
            sb.AppendLine(node.Text);
            foreach (TreeNode childNode in node.Nodes)
                AppendNodeToStringBuilder(indent + 1, childNode, sb);
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWorkDelegate doWork = (DoWorkDelegate) e.Argument;
            e.Result = doWork();
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSingleTreeNode(e.UserState.ToString());
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            trvSummary.Nodes.Clear();
            CodeBase codeBase = e.Result as CodeBase;
            if (codeBase != null)
            {
                _codeBase = codeBase;
                ShowParseResultsInTree();
            }
            ActionResults actionResults = e.Result as ActionResults;
            if (actionResults != null)
                ShowHitsInTree(actionResults);
            ShowRunnerStatus();
        }
        private void btnCopyAllHits_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TreeNode node in trvSummary.Nodes)
                AppendNodeToStringBuilder(0, node, sb);
            Clipboard.SetText(sb.ToString());
        }
        private void btnParseAll_Click(object sender, EventArgs e)
        {
            _codeBase = null;
            CodeBaseOptions options = _codeBaseOptions.Clone();
            RunBackground(delegate
            {
                return CodeBaseWorker.Execute(options, backgroundWorker1);
            });
        }
        private void btnRunAction_Click(object sender, EventArgs e)
        {
            RunSelectedAction();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        private ActionNode CreateActionNode(CodeBaseActionProxy action)
        {
            Block block = delegate {
                CodeBase codeBase = _codeBase;
                RunBackground(delegate
                {
                    IList<Hit> hits = action.Execute(codeBase);
                    return new ActionResults(action, hits);
                });
            };
            return new ActionNode(action.Name, action.Description, block);
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
        private void edtFileMasks_TextChanged(object sender, EventArgs e)
        {
            _codeBaseOptions.FileMasks = edtFileMasks.Text;
        }
        private void edtStartingDirectory_TextChanged(object sender, EventArgs e)
        {
            _codeBaseOptions.SearchPaths = edtStartingDirectory.Text;
        }
        private void ExecuteNodeAction(TreeNode node)
        {
            if (node == null)
                return;
            Block block = (Block) node.Tag;
            if (block != null)
                block();
        }
        private void LoadCatalog()
        {
            trvActions.Nodes.Clear();
            _catalog = Catalog.Load();

            ActionNode showParseResults = new ActionNode("Show parse results",
                "Re-displays the list of passing/failing files from the last 'Parse All'.",
                ShowParseResultsInTree);
            trvActions.Nodes.Add(showParseResults);

            foreach (Category category in _catalog.Categories)
            {
                ActionNode categoryNode = new ActionNode(category.CategoryType.ToString(),
                    category.Description, null);
                trvActions.Nodes.Add(categoryNode);
                foreach (CodeBaseActionProxy item in category.Items)
                {
                    ActionNode itemNode = CreateActionNode(item);
                    categoryNode.Nodes.Add(itemNode);
                }
            }

            trvActions.SelectedNode = trvActions.Nodes[0];
        }
        private void ParseFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                ((Form) sender).Close();
        }
        private void RunBackground(DoWorkDelegate doWork)
        {
            backgroundWorker1.RunWorkerAsync(doWork);
            ShowRunnerStatus();
        }
        private void RunSelectedAction()
        {
            if (!CanRunAction)
                return;
            ActionNode node = trvActions.SelectedNode as ActionNode;
            if (node == null)
                return;
            node.Execute();
        }
        private void ShowHitsInTree(ActionResults actionResults)
        {
            trvSummary.BeginUpdate();
            try
            {
                string summaryText = actionResults.Action.Name + ": " + actionResults.Hits.Count + " hit(s)";
                TreeNode summaryNode = new TreeNode(summaryText);
                trvSummary.Nodes.Add(summaryNode);

                List<Hit> sortedHits = SortHits(actionResults.Hits);
                foreach (Hit hit in sortedHits)
                {
                    TreeNode treeNode = CreateNodeForLocation(hit.Location);
                    treeNode.Text = Path.GetFileName(hit.Location.FileName) +
                        ":" + hit.Location.Offset + ": " + hit.Description;
                    summaryNode.Nodes.Add(treeNode);
                }
                trvSummary.ExpandAll();
            }
            finally
            {
                trvSummary.EndUpdate();
            }
        }
        private void ShowParseResultsInTree()
        {
            trvSummary.BeginUpdate();
            try
            {
                trvSummary.Nodes.Clear();
                trvSummary.Nodes.Add("Elapsed time: " + _codeBase.ParseDuration);
                Dictionary<string, List<string>> failed = new Dictionary<string, List<string>>();
                int failingCount = 0;
                foreach (NamedContent<Exception> error in _codeBase.Errors)
                {
                    if (!failed.ContainsKey(error.Content.Message))
                        failed.Add(error.Content.Message, new List<string>());
                    failed[error.Content.Message].Add(error.FileName);
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
                failingNode.Expand();
            }
            finally
            {
                trvSummary.EndUpdate();
            }
        }
        private void ShowRunnerStatus()
        {
            btnParseAll.Enabled = !backgroundWorker1.IsBusy;
            if (CanRunAction)
            {
                trvActions.BackColor = SystemColors.Window;
                edtActionDescription.BackColor = SystemColors.Info;
                edtActionDescription.ForeColor = SystemColors.InfoText;
            }
            else
            {
                trvActions.BackColor = SystemColors.Control;
                edtActionDescription.BackColor = SystemColors.Control;
                edtActionDescription.ForeColor = SystemColors.GrayText;
            }
            btnRunAction.Enabled = CanRunAction;
            btnStop.Enabled = backgroundWorker1.IsBusy;
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
                ViewSourceControl viewSource = new ViewSourceControl();
                viewSource.Text = errorLocation.FileSource;
                viewSource.Dock = DockStyle.Fill;
                form.Controls.Add(viewSource);
                form.Show();
                viewSource.ScrollToOffset(errorLocation.Offset);
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
        private static List<Hit> SortHits(IEnumerable<Hit> hits)
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
            return sortedHits;
        }
        private void trvActions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ActionNode node = e.Node as ActionNode;
            if (node == null)
                edtActionDescription.Text = "";
            else
                edtActionDescription.Text = node.Description;
        }
        private void trvActions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                RunSelectedAction();
            }
        }
        private void trvActions_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            RunSelectedAction();
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
