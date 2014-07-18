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
