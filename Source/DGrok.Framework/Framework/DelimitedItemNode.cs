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
