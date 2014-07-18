// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DGrok.DelphiNodes;
using DGrok.Framework;

namespace DGrok.Visitors
{
    [CodeBaseAction(CategoryType.DotNetCompatibility)]
    [Description("x86 assembler is not allowed in managed code.")]
    public class FindAsmBlocks : Visitor
    {
        public override void VisitAssemblerStatementNode(AssemblerStatementNode node)
        {
            AddHit(node, node.ToCode());
            base.VisitAssemblerStatementNode(node);
        }
    }
}
