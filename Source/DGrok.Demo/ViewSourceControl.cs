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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DGrok.Demo
{
    public partial class ViewSourceControl : UserControl
    {
        private Regex _findRegex = null;

        public ViewSourceControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return textBox1.Text; }
            set
            {
                textBox1.Text = value;
                textBox1.SelectionStart = 0;
            }
        }

        private void FindNext(int offset)
        {
            if (_findRegex == null)
                return;
            Match match = _findRegex.Match(Text, offset);
            if (match.Success)
            {
                textBox1.SelectionStart = match.Index;
                textBox1.SelectionLength = match.Length;
                textBox1.ScrollToCaret();
            }
            else
                MessageBox.Show(this, "No matches found", "Find");
        }
        public void ScrollToOffset(int offset)
        {
            textBox1.SelectionStart = offset;
            textBox1.ScrollToCaret();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                e.Handled = true;
                Regex regex = FindDialog.Execute();
                if (regex != null)
                {
                    _findRegex = regex;
                    FindNext(0);
                }
            }
            else if (e.KeyCode == Keys.F3)
                FindNext(textBox1.SelectionStart + 1);
        }
    }
}
