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
