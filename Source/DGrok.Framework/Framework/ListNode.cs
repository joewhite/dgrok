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
    public class ListNode : NonterminalNode
    {
        private List<AstNode> _items;

        public ListNode(IEnumerable<AstNode> items)
        {
            _items = new List<AstNode>(items);
        }

        public IList<AstNode> Items
        {
            get { return _items; }
        }
        public override IEnumerable<KeyValuePair<string, AstNode>> Properties
        {
            get
            {
                for (int i = 0; i < _items.Count; ++i)
                    yield return new KeyValuePair<string, AstNode>("Items[" + i + "]", Items[i]);
            }
        }
    }
}
