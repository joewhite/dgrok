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
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DGrok.Demo
{
    public partial class FindDialog : Form
    {
        public FindDialog()
        {
            InitializeComponent();
        }

        public Regex Regex
        {
            get
            {
                string pattern = Regex.Escape(edtSearchText.Text);
                if (chkWholeWords.Checked)
                    pattern = @"\b" + pattern + @"\b";

                RegexOptions options = 0;
                if (!chkCaseSensitive.Checked)
                    options |= RegexOptions.IgnoreCase;

                return new Regex(pattern, options);
            }
        }

        public static Regex Execute()
        {
            using (FindDialog dialog = new FindDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    return dialog.Regex;
                return null;
            }
        }
    }
}