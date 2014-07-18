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
    public class DelimitedItemNode<T> : NonterminalNode
        where T : AstNode
    {
        private Token _delimiterNode;
        private T _itemNode;

        public DelimitedItemNode(T itemNode, Token delimiterNode)
        {
            _itemNode = itemNode;
            _delimiterNode = delimiterNode;
        }

        public override IEnumerable<AstNode> ChildNodes
        {
            get
            {
                if (ItemNode != null)
                    yield return ItemNode;
                if (DelimiterNode != null)
                    yield return DelimiterNode;
            }
        }
        public Token DelimiterNode
        {
            get { return _delimiterNode; }
        }
        public T ItemNode
        {
            get { return _itemNode; }
        }
        public override IEnumerable<KeyValuePair<string, AstNode>> Properties
        {
            get
            {
                yield return new KeyValuePair<string, AstNode>("ItemNode", ItemNode);
                yield return new KeyValuePair<string, AstNode>("DelimiterNode", DelimiterNode);
            }
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitDelimitedItemNode(this, ItemNode, DelimiterNode);
        }
    }
}
