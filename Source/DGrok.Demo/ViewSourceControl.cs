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
