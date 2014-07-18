// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
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