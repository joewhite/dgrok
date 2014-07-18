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
        private Token _delimiter;
        private T _item;

        public DelimitedItemNode(T item, Token delimiter)
        {
            _item = item;
            _delimiter = delimiter;
        }

        public Token Delimiter
        {
            get { return _delimiter; }
        }
        public T Item
        {
            get { return _item; }
        }

        public override IEnumerable<KeyValuePair<string, AstNode>> Properties
        {
            get
            {
                yield return new KeyValuePair<string, AstNode>("Item", Item);
                yield return new KeyValuePair<string, AstNode>("Delimiter", Delimiter);
            }
        }
    }
}
