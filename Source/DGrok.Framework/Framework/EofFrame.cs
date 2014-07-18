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
        private static EofFrame _instance = new EofFrame();

        public EofFrame()
        {
        }

        public string DisplayName
        {
            get { return "end of file"; }
        }
        public static EofFrame Instance
        {
            get { return _instance; }
        }
        public bool IsEof
        {
            get { return true; }
        }
        public IFrame Next
        {
            get { throw new ParseException("Expected token but found end of file", Offset); }
            set { throw new InvalidOperationException("Cannot set Next on NullFrame"); }
        }
        public int Offset
        {
            get { return -1; }
        }
        public TokenType TokenType
        {
            get { return TokenType.EndOfFile; }
        }

        public bool CanParseToken(TokenSet tokenSet)
        {
            return false;
        }
        public Token ParseToken(TokenSet tokenSet)
        {
            throw new ParseException("Expected " + tokenSet.Name + " but found end of file", Offset);
        }
    }
}
