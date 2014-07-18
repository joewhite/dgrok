// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;
using DGrok.DelphiNodes;
using DGrok.Framework;

namespace DGrok.Visitors
{
    [CodeBaseAction]
    public class FindWithStatements : Visitor
    {
        public override void VisitWithStatementNode(WithStatementNode node)
        {
            Hits.Add(new Hit(node.With.Location, AstNode.ToCode(node.With, node.ExpressionList)));
            base.VisitWithStatementNode(node);
        }
    }
}
