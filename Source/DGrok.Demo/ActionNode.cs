// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DGrok.Demo
{
    internal class ActionNode : TreeNode
    {
        private Block _block;
        private string _description;

        public ActionNode(string text, string description, Block block)
            : base(text)
        {
            _description = description;
            _block = block;
        }

        public string Description
        {
            get { return _description; }
        }

        public void Execute()
        {
            if (_block != null)
                _block();
        }
    }
}
