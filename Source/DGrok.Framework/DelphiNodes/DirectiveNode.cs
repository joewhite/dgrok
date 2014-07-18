// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.Framework;

namespace DGrok.DelphiNodes
{
    public partial class DirectiveNode
    {
        public bool ForbidsBody
        {
            get
            {
                return
                    KeywordNode.Type == TokenType.ForwardSemikeyword ||
                    KeywordNode.Type == TokenType.ExternalSemikeyword;
            }
        }
    }
}
