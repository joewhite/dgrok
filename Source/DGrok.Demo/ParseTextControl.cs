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
    public partial class ParseTextControl : UserControl
    {
        private RuleType _ruleType = RuleType.Goal;

        public ParseTextControl()
        {
            InitializeComponent();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RuleType RuleType
        {
            get { return _ruleType; }
            set { _ruleType = value; }
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Parse();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        public void Parse()
        {
            Parser parser = Parser.FromText(edtSource.Text, CompilerDefines.CreateStandard());
            AstNode tree;
            try
            {
                tree = parser.ParseRule(_ruleType);
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
                edtSource.ScrollToCaret();
                return;
            }
        }
        public void ParseString(string s)
        {
            edtSource.Text = s;
            Parse();
        }
    }
}
