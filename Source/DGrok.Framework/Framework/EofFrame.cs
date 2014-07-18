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
