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
