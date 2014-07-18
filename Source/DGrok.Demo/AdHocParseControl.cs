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
using System.Text;
using System.Windows.Forms;
using DGrok.Framework;

namespace DGrok.Demo
{
    public partial class AdHocParseControl : UserControl
    {
        public AdHocParseControl()
        {
            InitializeComponent();
            lstRules.DataSource = Enum.GetValues(typeof(RuleType));
            lstRules.SelectedItem = RuleType.Goal;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Parser parser = Parser.FromText(edtSource.Text, CompilerDefines.CreateStandard());
                AstNode tree;
                try
                {
                    tree = parser.ParseRule((RuleType) lstRules.SelectedItem);
                    edtResults.Text = tree.Inspect();
                }
                catch (DGrokException ex)
                {
                    edtResults.Text = ex.Message;
                    if (ex.Offset >= 0)
                        edtSource.SelectionStart = ex.Offset;
                    else
                        edtSource.SelectionStart = edtSource.TextLength;
                    edtSource.Focus();
                    return;
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
