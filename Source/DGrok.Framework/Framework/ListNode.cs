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
