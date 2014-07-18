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
                edtParserThreadCount.Value = _options.ParserThreadCount;
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
        private void edtParserThreadCount_ValueChanged(object sender, EventArgs e)
        {
            _options.ParserThreadCount = (int) edtParserThreadCount.Value;
        }
        private void edtTrueIfConditions_TextChanged(object sender, EventArgs e)
        {
            _options.TrueIfConditions = edtTrueIfConditions.Text;
        }
    }
}
