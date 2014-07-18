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
    public class FindGlobalVariables : Visitor
    {
        public override void VisitMethodImplementationNode(MethodImplementationNode node)
        {
            // do not visit children
        }
        public override void VisitVarDeclNode(VarDeclNode node)
        {
            foreach (DelimitedItemNode<Token> delimitedItemNode in node.Names.Items)
            {
                Token nameToken = delimitedItemNode.Item;
                Hits.Add(new Hit(nameToken.Location, nameToken.Text));
            }
            base.VisitVarDeclNode(node);
        }
    }
}
