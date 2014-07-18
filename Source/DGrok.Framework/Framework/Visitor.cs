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
    public partial class Visitor : ICodeBaseAction
    {
        private List<Hit> _hits = new List<Hit>();

        public IList<Hit> Hits
        {
            get { return _hits; }
        }

        public IList<Hit> Execute(CodeBase codeBase)
        {
            Visit(codeBase);
            return Hits;
        }
        public void Visit(CodeBase codeBase)
        {
            foreach (KeyValuePair<string, AstNode> pair in codeBase.ParsedFiles)
                VisitSourceFile(pair.Key, pair.Value);
        }
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
        public virtual void VisitSourceFile(string fileName, AstNode node)
        {
            Visit(node);
        }
        public virtual void VisitToken(Token token)
        {
        }
    }
}
