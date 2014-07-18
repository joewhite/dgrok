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
    public class SingleTokenTokenSet : ITokenSet
    {
        private TokenType _tokenType;

        public SingleTokenTokenSet(TokenType tokenType)
        {
            _tokenType = tokenType;
        }

        public string Name
        {
            get { return _tokenType.ToString(); }
        }

        public bool Contains(TokenType value)
        {
            return value == _tokenType;
        }
        public bool LookAhead(Parser parser)
        {
            return parser.CanParseToken(this);
        }
    }
}
