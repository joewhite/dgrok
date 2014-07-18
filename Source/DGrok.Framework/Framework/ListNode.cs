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
    public class ListNode<T> : NonterminalNode
        where T : AstNode
    {
        private List<T> _items;

        public ListNode(IEnumerable<T> items)
        {
            _items = new List<T>(items);
        }

        public override IEnumerable<AstNode> ChildNodes
        {
            get
            {
                foreach (T item in Items)
                    yield return item;
            }
        }
        public IList<T> Items
        {
            get { return _items; }
        }
        public IEnumerable<AstNode> ItemsAsBase
        {
            get
            {
                foreach (AstNode node in Items)
                    yield return node;
            }
        }
        public override IEnumerable<KeyValuePair<string, AstNode>> Properties
        {
            get
            {
                for (int i = 0; i < _items.Count; ++i)
                    yield return new KeyValuePair<string, AstNode>("Items[" + i + "]", Items[i]);
            }
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitListNode(this, ItemsAsBase);
        }
    }
}
