// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public partial class Visitor
    {
        public void Visit(AstNode node)
        {
            if (node != null)
                node.Accept(this);
        }
        public virtual void VisitDelimitedItemNode(AstNode node, AstNode item, Token delimiter)
        {
            Visit(item);
            Visit(delimiter);
        }
        public virtual void VisitListNode(AstNode node, IEnumerable<AstNode> items)
        {
            foreach (AstNode item in items)
                Visit(item);
        }
        public virtual void VisitToken(Token token)
        {
        }
    }
}
