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
    [CodeBaseAction(CategoryType.BestPracticeViolations)]
    [Description("'With' statements make for confusing code. Avoid them.")]
    public class FindWithStatements : Visitor
    {
        public override void VisitWithStatementNode(WithStatementNode node)
        {
            AddHit(node.WithKeywordNode, AstNode.ToCode(node.WithKeywordNode, node.ExpressionListNode));
            base.VisitWithStatementNode(node);
        }
    }
}
