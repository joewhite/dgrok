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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Framework
{
    public class TokenSet : ITokenSet, IEnumerable<TokenType>
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
