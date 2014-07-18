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
using System.Windows.Forms;
using DGrok.Framework;

namespace DGrok.Demo
{
    public partial class MainForm : Form
    {
        CodeBaseOptions _codeBaseOptions;

        public MainForm()
        {
            InitializeComponent();

            _codeBaseOptions = new CodeBaseOptions();
            _codeBaseOptions.LoadFromRegistry();
            parseSourceTreeControl1.CodeBaseOptions = _codeBaseOptions;
            compilerOptionsControl1.CodeBaseOptions = _codeBaseOptions;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _codeBaseOptions.SaveToRegistry();
        }
    }
}