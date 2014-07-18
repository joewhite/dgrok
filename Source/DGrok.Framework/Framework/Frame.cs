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
        private IFrame _next = EofFrame.Instance;
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
        public IFrame Next
        {
            get { return _next; }
            set { _next = value; }
        }
        public int Offset
        {
            get { return _token.StartIndex; }
        }
        public Token Token
        {
            get { return _token; }
        }
        public TokenType TokenType
        {
            get { return _token.Type; }
        }

        public bool CanParseToken(TokenSet tokenSet)
        {
            return tokenSet.Contains(Token.Type);
        }
        public Token ParseToken(TokenSet tokenSet)
        {
            if (CanParseToken(tokenSet))
                return Token;
            throw new ParseException("Expected " + tokenSet.Name + " but found " + Token.Type, Offset);
        }
    }
}
