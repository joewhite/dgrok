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
