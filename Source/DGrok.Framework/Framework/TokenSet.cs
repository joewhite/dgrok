// DGrok Delphi parser
// Copyright (C) 2007 Joe White
// http://www.excastle.com/dgrok
//
// Licensed under the Open Software License version 3.0
// http://www.opensource.org/licenses/osl-3.0.php
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public class TokenSet : IEnumerable<TokenType>
    {
        private List<bool> _items = new List<bool>();
        private string _name;

        public TokenSet(string name)
        {
            _name = name;
        }
        public TokenSet(TokenType value)
        {
            _name = value.ToString();
            Add(value);
        }

        public string Name
        {
            get { return _name; }
        }

        public void Add(TokenType value)
        {
            EnsureContainsIndex(value);
            _items[(int) value] = true;
        }
        public void AddRange(IEnumerable<TokenType> values)
        {
            foreach (TokenType value in values)
                Add(value);
        }
        public bool Contains(TokenType value)
        {
            return ContainsIndex(value) && _items[(int) value];
        }
        private bool ContainsIndex(TokenType value)
        {
            return _items.Count > (int) value;
        }
        private void EnsureContainsIndex(TokenType value)
        {
            while (!ContainsIndex(value))
                _items.Add(false);
        }
        public IEnumerator<TokenType> GetEnumerator()
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                if (_items[i])
                    yield return (TokenType) i;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool LookAhead(Parser parser)
        {
            return parser.CanParseToken(this);
        }
    }
}
