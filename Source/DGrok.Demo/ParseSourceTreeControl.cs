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
        public ParseSourceTreeControl()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgBrowse.SelectedPath = edtStartingDirectory.Text;
            if (dlgBrowse.ShowDialog() == DialogResult.OK)
                edtStartingDirectory.Text = dlgBrowse.SelectedPath;
        }
        private void btnTestParser_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            trvSummary.BeginUpdate();
            try
            {
                trvSummary.Nodes.Clear();
                List<string> passed = new List<string>();
                Dictionary<string, List<string>> failed = new Dictionary<string, List<string>>();
                string[] fileNames = Directory.GetFiles(edtStartingDirectory.Text,
                    "*.pas", SearchOption.AllDirectories);
                int failingCount = 0;
                foreach (string fileName in fileNames)
                {
                    try
                    {
                        string source = File.ReadAllText(fileName);
                        Parser parser = Parser.FromText(source, CompilerDefines.CreateStandard());
                        parser.ParseRule(RuleType.Goal);
                        passed.Add(fileName);
                    }
                    catch (Exception ex)
                    {
                        if (!failed.ContainsKey(ex.Message))
                            failed.Add(ex.Message, new List<string>());
                        failed[ex.Message].Add(fileName);
                        ++failingCount;
                    }
                }
                TreeNode passingNode = trvSummary.Nodes.Add(String.Format("Passing ({0})", passed.Count));
                foreach (string s in passed)
                    passingNode.Nodes.Add(CreateFileNode(s));
                List<string> messages = new List<string>(failed.Keys);
                messages.Sort(StringComparer.CurrentCultureIgnoreCase);
                TreeNode failingNode = trvSummary.Nodes.Add(String.Format("Failing ({0})", failingCount));
                foreach (string message in messages)
                {
                    TreeNode failureNode = failingNode.Nodes.Add(
                        String.Format("{0} ({1})", message, failed[message].Count));
                    foreach (string fileName in failed[message])
                        failureNode.Nodes.Add(CreateFileNode(fileName));
                }
            }
            finally
            {
                trvSummary.EndUpdate();
                Cursor.Current = Cursors.Default;
            }
        }
        private TreeNode CreateFileNode(string fileName)
        {
            TreeNode node = new TreeNode(fileName);
            node.Tag = fileName;
            return node;
        }
        private void ParseFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                ((Form) sender).Close();
        }
        private void ShowWindowForNode(TreeNode selectedNode)
        {
            if (selectedNode == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string fileName = selectedNode.Tag as string;
                if (fileName == null)
                    return;
                Form form = new Form();
                form.Size = ParentForm.Size;
                form.Text = fileName;
                form.KeyPreview = true;
                form.KeyDown += ParseFormKeyDown;
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
                ShowWindowForNode(trvSummary.SelectedNode);
            }
        }
        private void trvSummary_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            ShowWindowForNode(selectedNode);
        }
    }
}
