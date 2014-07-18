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
