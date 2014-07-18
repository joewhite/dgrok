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
            try
            {
                trvSummary.Nodes.Clear();
                List<string> passed = new List<string>();
                Dictionary<string, List<string>> failed = new Dictionary<string, List<string>>();
                string[] fileNames = Directory.GetFiles(@"c:\program files\borland\bds\4.0\source\win32\rtl",
                    "*.pas", SearchOption.AllDirectories);
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
                    }
                }
                TreeNode passingNode = trvSummary.Nodes.Add(String.Format("Passing ({0})", passed.Count));
                foreach (string s in passed)
                    passingNode.Nodes.Add(s);
                List<string> messages = new List<string>(failed.Keys);
                messages.Sort(StringComparer.CurrentCultureIgnoreCase);
                foreach (string message in messages)
                {
                    TreeNode failureNode = trvSummary.Nodes.Add(
                        String.Format("{0} ({1})", message, failed[message].Count));
                    foreach (string fileName in failed[message])
                        failureNode.Nodes.Add(fileName);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
