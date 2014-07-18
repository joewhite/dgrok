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
