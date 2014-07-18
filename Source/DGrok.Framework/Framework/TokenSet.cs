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
