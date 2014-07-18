// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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
            try
            {
                Parser parser = Parser.FromText(viewSourceControl1.Text, "input", CompilerDefines.CreateStandard(),
                    new MemoryFileLoader());
                AstNode tree = parser.ParseRule(_ruleType);
                edtResults.Text = tree.Inspect();
            }
            catch (DGrokException ex)
            {
                edtResults.Text = "Filename: " + ex.Location.FileName + Environment.NewLine +
                    "Offset: " + ex.Location.Offset + Environment.NewLine +
                    ex.Message;
                viewSourceControl1.Focus();
                viewSourceControl1.ScrollToOffset(ex.Location.Offset);
                return;
            }
        }
        public void ParseString(string s)
        {
            viewSourceControl1.Text = s;
            Parse();
        }
    }
}
