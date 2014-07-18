// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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

        public void AddHit(AstNode node, string description)
        {
            Hits.Add(new Hit(node.Location, description));
        }
        public IList<Hit> Execute(CodeBase codeBase)
        {
            Visit(codeBase);
            return Hits;
        }
        public void Visit(CodeBase codeBase)
        {
            foreach (NamedContent<AstNode> node in codeBase.ParsedFiles)
                VisitSourceFile(node.FileName, node.Content);
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
