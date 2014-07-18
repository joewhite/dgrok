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
    public class EofFrame : IFrame
    {
        private Location _location;

        public EofFrame(Location location)
        {
            _location = location;
        }

        public string DisplayName
        {
            get { return "end of file"; }
        }
        public bool IsEof
        {
            get { return true; }
        }
        public Location Location
        {
            get { return _location; }
        }
        public IFrame Next
        {
            get { throw new ParseException("Expected token but found end of file", Location); }
            set { throw new InvalidOperationException("Cannot set Next on NullFrame"); }
        }
        public TokenType TokenType
        {
            get { return TokenType.EndOfFile; }
        }

        public bool CanParseToken(ITokenSet tokenSet)
        {
            return false;
        }
        public Token ParseToken(ITokenSet tokenSet)
        {
            throw new ParseException("Expected " + tokenSet.Name + " but found end of file", Location);
        }
    }
}
