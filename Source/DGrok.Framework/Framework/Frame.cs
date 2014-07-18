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
    public class Frame : IFrame
    {
        private IFrame _next = null;
        private Token _token;

        public Frame(Token token)
        {
            _token = token;
        }

        public string DisplayName
        {
            get { return Token.Type.ToString(); }
        }
        public bool IsEof
        {
            get { return false; }
        }
        public Location Location
        {
            get { return Token.Location; }
        }
        public IFrame Next
        {
            get
            {
                if (_next == null)
                    _next = new EofFrame(Token.EndLocation);
                return _next;
            }
            set { _next = value; }
        }
        public Token Token
        {
            get { return _token; }
        }
        public TokenType TokenType
        {
            get { return _token.Type; }
        }

        public bool CanParseToken(ITokenSet tokenSet)
        {
            return tokenSet.Contains(Token.Type);
        }
        public Token ParseToken(ITokenSet tokenSet)
        {
            if (CanParseToken(tokenSet))
                return Token;
            throw new ParseException("Expected " + tokenSet.Name + " but found " + Token.Type, Location);
        }
    }
}
