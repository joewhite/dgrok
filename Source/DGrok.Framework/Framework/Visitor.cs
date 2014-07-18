// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
