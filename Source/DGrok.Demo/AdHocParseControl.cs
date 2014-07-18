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
    public partial class AdHocParseControl : UserControl
    {
        public AdHocParseControl()
        {
            InitializeComponent();
            lstRules.DataSource = Enum.GetValues(typeof(RuleType));
            lstRules.SelectedItem = RuleType.Goal;
        }

        private void lstRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            parseTextControl1.RuleType = (RuleType) lstRules.SelectedItem;
        }
    }
}
