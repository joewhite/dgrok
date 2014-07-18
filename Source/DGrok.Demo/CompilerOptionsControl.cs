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
