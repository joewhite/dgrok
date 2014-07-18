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
    public partial class CompilerOptionsControl : UserControl
    {
        private CodeBaseOptions _options;

        public CompilerOptionsControl()
        {
            InitializeComponent();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CodeBaseOptions CodeBaseOptions
        {
            get { return _options; }
            set
            {
                _options = value;
                edtCompilerOptionsSetOff.Text = _options.CompilerOptionsSetOff;
                edtCompilerOptionsSetOn.Text = _options.CompilerOptionsSetOn;
                edtCustomDefines.Text = _options.CustomDefines;
                edtDelphiVersionDefine.Text = _options.DelphiVersionDefine;
                edtFalseIfConditions.Text = _options.FalseIfConditions;
                edtTrueIfConditions.Text = _options.TrueIfConditions;
            }
        }

        private void edtCompilerOptionsSetOff_TextChanged(object sender, EventArgs e)
        {
            _options.CompilerOptionsSetOff = edtCompilerOptionsSetOff.Text;
        }
        private void edtCompilerOptionsSetOn_TextChanged(object sender, EventArgs e)
        {
            _options.CompilerOptionsSetOn = edtCompilerOptionsSetOn.Text;
        }
        private void edtCustomDefines_TextChanged(object sender, EventArgs e)
        {
            _options.CustomDefines = edtCustomDefines.Text;
        }
        private void edtDelphiVersionDefine_TextChanged(object sender, EventArgs e)
        {
            _options.DelphiVersionDefine = edtDelphiVersionDefine.Text;
        }
        private void edtFalseIfConditions_TextChanged(object sender, EventArgs e)
        {
            _options.FalseIfConditions = edtFalseIfConditions.Text;
        }
        private void edtTrueIfConditions_TextChanged(object sender, EventArgs e)
        {
            _options.TrueIfConditions = edtTrueIfConditions.Text;
        }
    }
}
